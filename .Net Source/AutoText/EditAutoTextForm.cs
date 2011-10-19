using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using TSDatabase;


namespace AutoText
{
    public partial class EditAutoTextForm : Form
    {
        public EditAutoTextForm()
        {
            InitializeComponent();

            FillCompaniesCombo();
            FillCountriesCombo();
        }

        #region Event Handlers

        private void EditAutoTextForm_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (SaveAutoText())
            {
                DialogResult = DialogResult.OK;
                this.Hide();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Hide();
        }


        #endregion

        #region Properties

        public int AutoTextId 
        {
            get
            {
                return autotextId;
            }
            set
            {
                autotextId = value;
                if (autotextId > -1)
                {
                    LoadAutoText();
                    this.Text = "EDIT AUTOTEXT";
                }
                else
                {
                    this.Text = "NEW AUTOTEXT";
                    nameTextbox.Text = string.Empty;
                    autotextTextbox.Text = string.Empty;
                }
            }
        }

        public int CompanyId
        {
            set
            {
                ComboItem.SetComboValue(companiesCombo, value);
            }
        }

        public int CountryId
        {
            set
            {
                ComboItem.SetComboValue(countriesCombo, value);
            }
        }

        #endregion

        #region Methods

        private void FillCompaniesCombo()
        {
            try
            {
                companiesCombo.Items.Clear();

                using (Database database = DatabaseFactory.Connect(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"]))
                {
                    using (IDataReader reader = database.QuerySQL(SqlText.Companies_GetAll))
                    {
                        while (reader.Read())
                        {
                            companiesCombo.Items.Add(new ComboItem((string)reader["name"], (int)reader["id"]));
                        }
                        reader.Close();
                    }
                }

                companiesCombo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ErrorHandler.ReportError("Unable to fill company combo.", ex);
            }

        }

        private void FillCountriesCombo()
        {
            try
            {
                countriesCombo.Items.Clear();

                int selectedCompany = (int)((ComboItem)companiesCombo.SelectedItem).Value;

                using (Database database = DatabaseFactory.Connect(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"]))
                {
                    IDbDataParameter[] parameter = new IDbDataParameter[1];
                    parameter[0] = database.CreateParameter("@company_id", selectedCompany, ParameterDirection.Input);
                    using (IDataReader reader = database.QuerySQL(SqlText.Countries_GetAll, ref parameter))
                    {
                        while (reader.Read())
                        {
                            countriesCombo.Items.Add(new ComboItem((string)reader["name"], (int)reader["id"]));
                        }
                        reader.Close();
                    }
                }

                countriesCombo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ErrorHandler.ReportError("Unable to fill country combo.", ex);
            }

        }

        private void LoadAutoText()
        {
            try
            {
                using (Database database = DatabaseFactory.Connect(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"]))
                {
                    IDbDataParameter[] parameter = new IDbDataParameter[1];
                    parameter[0] = database.CreateParameter("@id", autotextId, ParameterDirection.Input);
                    using (IDataReader reader = database.QuerySQL(SqlText.Autotext_Get, ref parameter))
                    {
                        if (reader.Read())
                        {
                            ComboItem.SetComboValue(companiesCombo, (int)reader["company_id"]);
                            ComboItem.SetComboValue(countriesCombo, (int)reader["country_id"]);
                            nameTextbox.Text = (string)reader["name"];
                            autotextTextbox.Text = (string)reader["autotext"];
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ReportError("Unable to load autotext.",ex);
            }
        }

        private bool SaveAutoText()
        {
            bool saved = false;
            try
            {
                string errorMessage = String.Empty;

                //company
                int companyId = (int)((ComboItem)companiesCombo.SelectedItem).Value;

                //country
                int countryId = (int)((ComboItem)countriesCombo.SelectedItem).Value;

                //name
                string name = nameTextbox.Text.Trim();
                if (string.IsNullOrEmpty(name))
                {
                    errorMessage += " - you must provide a Name for the AutoText element\n\r";
                }

                //autotext
                string autotext = autotextTextbox.Text;
                if (string.IsNullOrEmpty(autotext))
                {
                    errorMessage += " - you must provide AutoText\n\r";
                }

                if (errorMessage != String.Empty)
                {
                    errorMessage = "There are problems with the AutoText data: \n\r" + errorMessage;
                    MessageBox.Show(errorMessage, "AutoText Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    using (Database database = DatabaseFactory.Connect(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"]))
                    {
                        IDbDataParameter[] parameter = new IDbDataParameter[7];
                        parameter[0] = database.CreateParameter("@id", autotextId, ParameterDirection.Input);
                        parameter[1] = database.CreateParameter("@country_id", countryId, ParameterDirection.Input);
                        parameter[2] = database.CreateParameter("@company_id", companyId, ParameterDirection.Input);
                        parameter[3] = database.CreateParameter("@name", name, ParameterDirection.Input);
                        parameter[4] = database.CreateParameter("@autotext", autotext, ParameterDirection.Input);
                        parameter[5] = database.CreateParameter("@user", Environment.UserName, ParameterDirection.Input);
                        parameter[6] = database.CreateParameter("@newid", -1, ParameterDirection.Output);

                        database.ExecuteSQL(SqlText.Autotext_Update, ref parameter);

                        autotextId = (int)parameter[6].Value;

                        saved = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ReportError("Unable to save autotext.", ex);
            }

            return saved;

        }

        #endregion

        #region Fields

        private int autotextId = -1;

        #endregion

    }
}
