using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoTextDataMigrator
{
    public partial class AutoTextDataMigrator : Form
    {
        #region Constructors

        public AutoTextDataMigrator()
        {
            InitializeComponent();
        }

        #endregion 

        #region Event Handlers

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void convertRTFButton_Click(object sender, EventArgs e)
        {
            ConvertRTFToPlainText();
        }

        private void rtfDataConverter_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                AddProgressMessage("RTF conversion error");
            }
            else if (e.Cancelled)
            {
                AddProgressMessage("RTF conversion cancelled");
            }
            else
            {
                SetProgressBar(100);
                AddProgressMessage("Completed converting RTF text to plain text");
            }

            ResetButtons();
        }

        private void migrateDataButton_Click(object sender, EventArgs e)
        {
            MigrateData();
        }

        private void dataMigrator_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                AddProgressMessage("Data migration error");
            }
            else if (e.Cancelled)
            {
                AddProgressMessage("Data migration cancelled");
            }
            else
            {
                SetProgressBar(100);
                AddProgressMessage("Completed data migration");
            }

            ResetButtons();
        }

        private void process_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SetProgressBar(e.ProgressPercentage);

            if (!string.IsNullOrEmpty((string)e.UserState))
            {
                AddProgressMessage((string)e.UserState);
            }
        }

        #endregion

        #region Methods

        private void SetProgressBar(int percentage)
        {
            conversionProgressBar.Value = percentage;
            conversionProgressBar.CreateGraphics().DrawString(percentage.ToString() + "%", new Font("Arial", (float)9, FontStyle.Regular), Brushes.Black, new PointF(conversionProgressBar.Width / 2 - 10, conversionProgressBar.Height / 2 - 7));
        }

        private void AddProgressMessage(string message)
        {
            progressListbox.Items.Add(message);
            progressListbox.SelectedIndex = progressListbox.Items.Count - 1;
            progressListbox.SelectedIndex = -1;
        }

        private void ResetButtons()
        {
            convertRTFButton.Text = "Convert RTF";
            migrateDataButton.Text = "Migrate Data";
            convertRTFButton.Enabled = true;
            migrateDataButton.Enabled = true;
            closeButton.Enabled = true;
        }

        private void ConvertRTFToPlainText()
        {

            if (convertRTFButton.Text == "Cancel")
            {
                rtfDataConverter.CancelAsync();
            }
            else
            {
                convertRTFButton.Text = "Cancel";
                migrateDataButton.Enabled = false;
                closeButton.Enabled = false;

                progressListbox.Items.Clear();
                AddProgressMessage("Converting RTF text ...");
                SetProgressBar(0);

                rtfDataConverter = new RTFDataConverter(this.oldDatabaseConnectionStringTextbox.Text);
                rtfDataConverter.ProgressChanged += new ProgressChangedEventHandler(process_ProgressChanged);
                rtfDataConverter.RunWorkerCompleted += new RunWorkerCompletedEventHandler(rtfDataConverter_Completed);
                rtfDataConverter.RunWorkerAsync();
            }
        }

        private void MigrateData()
        {
            if (migrateDataButton.Text == "Cancel")
            {
                dataMigrator.CancelAsync();
            }
            else
            {
                migrateDataButton.Text = "Cancel";
                convertRTFButton.Enabled = false;
                closeButton.Enabled = false;

                progressListbox.Items.Clear();
                AddProgressMessage("Migrating data ...");
                SetProgressBar(0);

                dataMigrator = new DataMigrator(this.oldDatabaseConnectionStringTextbox.Text, this.steppes2ConnectionStringTextbox.Text, this.doNotMigrateMissingNameCheckbox.Checked, this.doNotMigrateMissingTextCheckbox.Checked);
                dataMigrator.ProgressChanged += new ProgressChangedEventHandler(process_ProgressChanged);
                dataMigrator.RunWorkerCompleted += new RunWorkerCompletedEventHandler(dataMigrator_Completed);
                dataMigrator.RunWorkerAsync();
            }
        }

        #endregion

        #region Fields

        private RTFDataConverter rtfDataConverter = null;
        private DataMigrator dataMigrator = null;

        #endregion

    }
}
