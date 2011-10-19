using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Linq;
using TSDatabase;

namespace AutoTextDataMigrator
{
    public class RTFDataConverter : BackgroundWorker
    {
        public RTFDataConverter(string oldDBConnectionString)
        {
            this.oldDBConnection = new ConnectionStringSettings("OldDBConnection", oldDBConnectionString, "System.Data.SqlClient");

            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
        }

        #region Properties

        public int ConversionProgress
        {
            get
            {
                int progressPercentage = 0;
                if (totalRecords > 0)
                {
                    progressPercentage = (int)((double)totalProcessed * 100.00 / (double)totalRecords);
                }
                return progressPercentage;
            }
        }

        #endregion

        #region Methods

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            ConvertToPlainText(e);
        }

        private string ConvertElement(string rtfText)
        {
            RichTextBox rtBox = new RichTextBox();

            // Convert the RTF to plain text.
            rtBox.Rtf = rtfText;
            string plainText = rtBox.Text;

            // Make sure the new lines are valid 
            plainText = plainText.Replace("\n", Environment.NewLine);

            //convert the bullet points
            char tab = '\u0009';
            plainText = plainText.Replace("·" + tab.ToString(), "- ");
            
            // Trim
            plainText = plainText.TrimStart();

            // Turn into a paragraph - ***perhaps controversial***
            // plainText += Environment.NewLine + Environment.NewLine;

            return plainText;

        }

        private void ConvertToPlainText(DoWorkEventArgs e)
        {
            try
            {
                int totalConverted = 0;

                using (Database database = DatabaseFactory.Connect(oldDBConnection))
                {

                    // create the plain text field if it doesnt exist
                    try
                    {
                        database.ExecuteSQL(SqlText.AutoText_BuildPlainTextField);
                    }
                    catch (SqlException sqlEx)
                    {
                        if (!sqlEx.Message.StartsWith("Column names in each table must be unique.")) // safer to catch error rather than check existance of column as latter requires sql user permissions on master and that might not be the case
                        {
                            throw;
                        }
                    }
                    
                    totalProcessed = 0;
                    totalRecords = database.CountSQL(SqlText.AutoText_TotalRecords);
                    
                    using (IDataReader reader = database.QuerySQL(SqlText.AutoText_GetAll))
                    {
                        if (CancellationPending) e.Cancel = true;

                        while (reader.Read() && !e.Cancel)
                        {
                            // for each table
                            int autotextId = (int)reader["AutoTextId"];
                            string autotextName = (string)reader["AutoTextName"];
                            string autotextRTF = (string)reader["AutoText"];

                            try
                            {
                                string autotextPlainText = ConvertElement(autotextRTF);
                            
                                using (Database database2 = DatabaseFactory.Connect(oldDBConnection))
                                {
                                    IDbDataParameter[] parameter = new IDbDataParameter[2];
                                    parameter[0] = database.CreateParameter("@AutoTextId", autotextId, ParameterDirection.Input);
                                    parameter[1] = database.CreateParameter("@AutoTextPlainText", autotextPlainText, ParameterDirection.Input);
                                    database2.ExecuteSQL(SqlText.AutoText_UpdatePlainText, ref parameter);
                                }

                                totalConverted++;
                                //ReportProgress(ConversionProgress , "  Converted element '" + autotextName + "'");
                                ReportProgress(ConversionProgress, string.Empty);

                            }
                            catch (Exception conversionEx)
                            {
                                ReportProgress(ConversionProgress, "Failed to convert RTF for element " + autotextId + " '" + autotextName + "':");
                                foreach (string errorLine in Tools.Split(conversionEx.ToString(), 80))
                                {
                                    ReportProgress(ConversionProgress, "    " + errorLine);
                                }
                            }

                            totalProcessed++;

                            if (CancellationPending) 
                            {    
                                e.Cancel = true;
                            }
                        }
                        reader.Close();
                    }
                    
                }

                ReportProgress(ConversionProgress, "Converted " + totalConverted + " out of " + totalRecords + " AutoText elements");

            }
            catch (Exception ex)
            {
                ReportProgress(ConversionProgress, "Unable to convert RTF text to plain text.");
                foreach (string errorLine in Tools.Split(ex.ToString(), 100))
                {
                    ReportProgress(ConversionProgress, errorLine);
                }
            }
        }

        #endregion

        #region Fields

        private ConnectionStringSettings oldDBConnection = null;
        private int totalProcessed = 0;
        private int totalRecords = 0;

        #endregion

    }
}
