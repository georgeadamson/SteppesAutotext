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
    public class DataMigrator : BackgroundWorker
    {
        public DataMigrator(string oldDBConnectionString, string newDBConnectionString, bool doNotImportNameMissing, bool doNotImportTextMissing)
        {
            this.oldDBConnection = new ConnectionStringSettings("OldDBConnection", oldDBConnectionString, "System.Data.SqlClient");
            this.newDBConnection = new ConnectionStringSettings("NewDBConnection", newDBConnectionString, "System.Data.SqlClient");
            this.doNotImportNameMissing = doNotImportNameMissing;
            this.doNotImportTextMissing = doNotImportTextMissing;

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
           Migrate(e);
        }

        private void Migrate(DoWorkEventArgs e)
        {
            try
            {
                int totalMigrated = 0;

                using (Database newDatabase = DatabaseFactory.Connect(newDBConnection))
                {

                    using (Database oldDatabase = DatabaseFactory.Connect(oldDBConnection))
                    {
                        // create the AutoText table if it doesnt exist
                        try
                        {
                            newDatabase.ExecuteSQL(SqlText.AutoText_CreateTable);
                        }
                        catch (SqlException sqlEx)
                        {
                            if (!sqlEx.Message.StartsWith("There is already an object named")) // safer to catch error rather than check existance of column as latter requires sql user permissions on master and that might not be the case
                            {
                                throw;
                            }
                        }

                        // make sure the autotext table is empty
                        newDatabase.ExecuteSQL(SqlText.AutoText_DeleteAll);

                        // turn on autoinc field insert
                        newDatabase.ExecuteSQL(SqlText.AutoText_SetIdentityInsertOn);

                        totalProcessed = 0;
                        totalRecords = oldDatabase.CountSQL(SqlText.AutoText_TotalRecords);

                        using (IDataReader reader = oldDatabase.QuerySQL(SqlText.AutoText_GetAllForMigration))
                        {
                            if (CancellationPending) e.Cancel = true;

                            while (reader.Read() && !e.Cancel)
                            {
                                // for each table
                                int autotextId = (int)reader["AutoTextId"];
                                string autotextName = (string)reader["AutoTextName"];
                                string autotextText = (string)reader["PlainText"];
                                int countryId = (int)reader["CountryId"];
                                int companyId = (int)reader["CompanyId"];

                                bool migrate = true;

                                if (string.IsNullOrEmpty(autotextName))
                                {
                                    string reportNoName = "Element " + autotextId + " has no name.";
                                    if (doNotImportNameMissing)
                                    {
                                        migrate = false;
                                        reportNoName += " Not migrating.";
                                    }
                                    else
                                    {
                                        reportNoName += " Attempting migration anyway ...";
                                    }

                                    ReportProgress(ConversionProgress, reportNoName);
                                }

                                if (string.IsNullOrEmpty(autotextText))
                                {
                                    string reportNoText = "Element " + autotextId + " has no text.";
                                    if (doNotImportTextMissing)
                                    {
                                        migrate = false;
                                        reportNoText += " Not migrating.";
                                    }
                                    else
                                    {
                                        reportNoText += " Attempting migration anyway ...";
                                    }

                                    ReportProgress(ConversionProgress, reportNoText);
                                }

                                if (migrate)
                                {
                                    try
                                    {
                                        IDbDataParameter[] parameter = new IDbDataParameter[5];
                                        parameter[0] = newDatabase.CreateParameter("@id", autotextId, ParameterDirection.Input);
                                        parameter[1] = newDatabase.CreateParameter("@name", autotextName, ParameterDirection.Input);
                                        parameter[2] = newDatabase.CreateParameter("@autotext", autotextText, ParameterDirection.Input);
                                        parameter[3] = newDatabase.CreateParameter("@country_id", countryId, ParameterDirection.Input);
                                        parameter[4] = newDatabase.CreateParameter("@company_id", companyId, ParameterDirection.Input);
                                        newDatabase.ExecuteSQL(SqlText.AutoText_InsertAutotextElement, ref parameter);

                                        totalMigrated++;
                                        ReportProgress(ConversionProgress, string.Empty);
                                    }
                                    catch (Exception conversionEx)
                                    {
                                        ReportProgress(ConversionProgress, "Failed to migrate data for element " + autotextId + " '" + autotextName + "':");
                                        foreach (string errorLine in Tools.Split(conversionEx.ToString(), 80))
                                        {
                                            ReportProgress(ConversionProgress, "    " + errorLine);
                                        }
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

                        newDatabase.ExecuteSQL(SqlText.AutoText_SetIdentityInsertOff);
                    }
                }

                ReportProgress(ConversionProgress, "Migrated " + totalMigrated + " out of " + totalRecords + " AutoText elements");

            }
            catch (Exception ex)
            {
                ReportProgress(ConversionProgress, "Unable to migrate data to new database.");
                foreach (string errorLine in Tools.Split(ex.ToString(), 100))
                {
                    ReportProgress(ConversionProgress, errorLine);
                }
            }
        }

        #endregion

        #region Fields

        private ConnectionStringSettings oldDBConnection = null;
        private ConnectionStringSettings newDBConnection = null;
        private bool doNotImportNameMissing = false;
        private bool doNotImportTextMissing = false;
        private int totalProcessed = 0;
        private int totalRecords = 0;

        #endregion

    }
}
