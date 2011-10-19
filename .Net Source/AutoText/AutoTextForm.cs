using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Resources;
using TSDatabase;

namespace AutoText
{
    public partial class AutoTextForm : Form
    {
        #region Constructors/Destructors

        public AutoTextForm()
        {
            InitializeComponent();

            PositionControls();
            FillCompaniesCombo(); //events fired will fill the other combos
        }

        #endregion

        #region Event Handlers

        private void AutoText_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, 0);
        }

        private void AutoTextForm_ResizeEnd(object sender, EventArgs e)
        {
            PositionControls();
        }  

        private void companiesCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCompanyId = (int)((ComboItem)companiesCombo.SelectedItem).Value;
            FillCountriesCombo();
        }

        private void countriesCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCountryId = (int)((ComboItem)countriesCombo.SelectedItem).Value;
            FillAutoTextCombo();
        }

        private void autotextCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedAutotextId = (int)((ComboItem)autotextCombo.SelectedItem).Value;
            if (AutoTextExists())
            {
                DisplayAutoText();
            }
        }

        private void pasteButton_click(object sender, EventArgs e)
        {
            PasteText();
        }

        private void toggleButton_Click(object sender, EventArgs e)
        {
            this.expanded = !this.expanded;
            PositionControls();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if (AutoTextExists())
            {
                SaveAutoText();
            }
        }

        private void reloadToolStripButton_Click(object sender, EventArgs e)
        {
            if (AutoTextExists())
            {
                DisplayAutoText();
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            DisplayEditForm(-1);
        }

        private void editToolStripButton_Click(object sender, EventArgs e)
        {
            if (AutoTextExists())
            {
                DisplayEditForm(selectedAutotextId);
            }
        }

        private void deleteToolStripButton_Click(object sender, EventArgs e)
        {
            if (AutoTextExists())
            {
                DeleteAutoText();
            }
        }     

        private void topmostDetectorTimer_Tick(object sender, EventArgs e)
        {
            DisplayCurrentPasteWindow();
        }

        private void autotextTextbox_TextChanged(object sender, EventArgs e)
        {
            SetChangedFlag();
        }

        #endregion

        #region Data Methods

        private void PositionControls()
        {
            try
            {
                // top bar
                companiesCombo.Left = 0;
                companiesCombo.Top = 0;
                companiesCombo.Width = 137;

                countriesCombo.Left = companiesCombo.Right;
                countriesCombo.Top = 0;
                countriesCombo.Width = 137;

                toggleButton.Left = this.ClientRectangle.Width - toggleButton.Width;
                toggleButton.Top = 0;
                toggleButton.Width = 30;

                pasteButton.Left = toggleButton.Left - pasteButton.Width;
                pasteButton.Top = 0;
                pasteButton.Width = 30;

                autotextCombo.Left = countriesCombo.Right;
                autotextCombo.Top = 0;
                autotextCombo.Width = pasteButton.Left - countriesCombo.Right; // fill the remaining horizontal space

                if (expanded)
                {
                    this.Height = 300;

                    // bottom toolbar
                    buttonToolstrip.Visible = true;

                    // status bar 
                    // nothing to do at the mo

                    // textbox
                    autotextTextbox.Left = 0;
                    autotextTextbox.Top = companiesCombo.Bottom;
                    autotextTextbox.Width = this.ClientRectangle.Width;
                    autotextTextbox.Height = buttonToolstrip.Top - companiesCombo.Bottom; // fill the remaining vertical space

                    toggleButton.Image = Properties.Resources.arrow_up;
                }
                else
                {
                    this.Height = companiesCombo.Height + (this.Height - this.ClientRectangle.Height);
                    toggleButton.Image = Properties.Resources.arrow_down;
                    buttonToolstrip.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ReportError("Unable to position controls", ex);
            }
        }

        private void FillCompaniesCombo()
        {
            try
            {
                companiesCombo.BeginUpdate();

                companiesCombo.Items.Clear();
                companiesCombo.Items.Add(new ComboItem("All Companies", -1));

                using (Database database = DatabaseFactory.Connect(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"]))
                {
                    using (IDataReader reader = database.QuerySQL(SqlText.Companies_GetAllUsed))
                    {
                        while (reader.Read())
                        {
                            companiesCombo.Items.Add(new ComboItem((string)reader["name"], (int)reader["id"]));
                        }
                        reader.Close();
                    }
                }

                companiesCombo.EndUpdate();

                if(initialiseComboIndex) companiesCombo.SelectedIndex = 0;
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
                countriesCombo.BeginUpdate();

                countriesCombo.Items.Clear();
                countriesCombo.Items.Add(new ComboItem("All Countries", -1));

                using (Database database = DatabaseFactory.Connect(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"]))
                {
                    IDbDataParameter[] parameter = new IDbDataParameter[1];
                    parameter[0] = database.CreateParameter("@company_id", selectedCompanyId, ParameterDirection.Input);
                    using (IDataReader reader = database.QuerySQL(SqlText.Countries_GetAllUsed, ref parameter))
                    {
                        while (reader.Read())
                        {
                            countriesCombo.Items.Add(new ComboItem((string)reader["name"], (int)reader["id"]));
                        }
                        reader.Close();
                    }
                }

                countriesCombo.EndUpdate();

                if (initialiseComboIndex) countriesCombo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ErrorHandler.ReportError("Unable to fill country combo.", ex);
            }
        }

        private void FillAutoTextCombo()
        {
            try
            {
                autotextCombo.BeginUpdate();
                autotextCombo.Items.Clear();

                using (Database database = DatabaseFactory.Connect(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"]))
                {
                    IDbDataParameter[] parameter = new IDbDataParameter[2];
                    parameter[0] = database.CreateParameter("@company_id", selectedCompanyId, ParameterDirection.Input);
                    parameter[1] = database.CreateParameter("@country_id", selectedCountryId, ParameterDirection.Input);
                    using (IDataReader reader = database.QuerySQL(SqlText.Autotext_GetFiltered, ref parameter))
                    {
                        while (reader.Read())
                        {
                            autotextCombo.Items.Add(new ComboItem((string)reader["name"], (int)reader["id"]));
                        }
                        reader.Close();
                    }
                }

                autotextCombo.EndUpdate();

                if (initialiseComboIndex)
                {
                    if (autotextCombo.Items.Count > 0)
                    {
                        autotextCombo.SelectedIndex = 0;
                    }
                    else
                    {
                        ReloadWithMessage();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ReportError("Unable to fill autotext combo.", ex);
            }
        }

        private void DisplayAutoText()
        {
            try
            {
                using (Database database = DatabaseFactory.Connect(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"]))
                {
                    IDbDataParameter[] parameter = new IDbDataParameter[1];
                    parameter[0] = database.CreateParameter("@id", selectedAutotextId, ParameterDirection.Input);
                    using (IDataReader reader = database.QuerySQL(SqlText.Autotext_Get, ref parameter))
                    {
                        if (reader.Read())
                        {
                            autotextTextbox.Text = (string)reader["AutoText"];
                            ClearChangedFlag();
                        }
                        reader.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.ReportError("Unable to display autotext.", ex);
            }

        }

        private void LoadAutoText(int autotextId)
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
                            ComboItem.SetComboValue(autotextCombo, autotextId);
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ReportError("Unable to load autotext.", ex);
            }
        }

        private void DisplayEditForm(int autotextId)
        {
            try
            {
                using (EditAutoTextForm editAutoTextForm = new EditAutoTextForm())
                {
                    editFormHandle = editAutoTextForm.Handle;
                    editAutoTextForm.CountryId = selectedCountryId; //for add only
                    editAutoTextForm.CompanyId = selectedCompanyId; //for add only
                    editAutoTextForm.AutoTextId = autotextId;       //for edit only
                    editAutoTextForm.Location = new Point(this.Left + 30, this.Top + 30);
                    if (editAutoTextForm.ShowDialog(this) == DialogResult.OK)
                    {
                        //refresh all the drop downs
                        initialiseComboIndex = false;
                        FillCompaniesCombo();
                        initialiseComboIndex = true;

                        //reload the form with the latest saved data
                        selectedAutotextId = editAutoTextForm.AutoTextId;
                        if (AutoTextExists()) //element may have been deleted while it was being edited
                        {
                            LoadAutoText(editAutoTextForm.AutoTextId); // still have to keep track of the new Id as the class-scope selectedAutotext may get overwritten as the combos fill
                        }
                    }
                    editAutoTextForm.Close();
                }
            }
            catch(Exception ex)
            {
                ErrorHandler.ReportError("Unable to display edit form.", ex);
            }
            finally
            {
                editFormHandle = IntPtr.Zero;
            }
        }

        private bool SaveAutoText()
        {
            bool success = false;
            try
            {
                string errorMessage = String.Empty;

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
                        IDbDataParameter[] parameter = new IDbDataParameter[3];
                        parameter[0] = database.CreateParameter("@id",selectedAutotextId, ParameterDirection.Input);
                        parameter[1] = database.CreateParameter("@autotext", autotext, ParameterDirection.Input);
                        parameter[2] = database.CreateParameter("@user", Environment.UserName, ParameterDirection.Input);

                        database.ExecuteSQL(SqlText.Autotext_UpdateText, ref parameter);

                        ClearChangedFlag(); //to update the controls
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.ReportError("Unable to save autotext text.", ex);
            }

            return success;

        }

        private void DeleteAutoText()
        {
            try
            {
                if (MessageBox.Show ("Are you sure you want to delete this AutoText element?"
                                        , "AutoText Delete"
                                        , MessageBoxButtons.YesNo
                                        , MessageBoxIcon.Question)== DialogResult.Yes) 
                {

                    using (Database database = DatabaseFactory.Connect(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"]))
                    {
                        IDbDataParameter[] parameter = new IDbDataParameter[1];
                        parameter[0] = database.CreateParameter("@id", selectedAutotextId, ParameterDirection.Input);
                        database.ExecuteSQL(SqlText.Autotext_Delete, ref parameter);

                        //refresh all the drop downs
                        FillCompaniesCombo();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ReportError("Unable to delete autotext.", ex);
            }
           
        }

        private void SetChangedFlag()
        {
            //autotextChanged = true;
            saveToolStripButton.ForeColor = Color.Red;
            saveToolStripButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void ClearChangedFlag()
        {
            //autotextChanged = false;
            saveToolStripButton.ForeColor = Color.Black;
            saveToolStripButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void ReloadWithMessage()
        {
            MessageBox.Show("Selected element has been deleted by another user.\n\nAutoText will be refreshed."
                                        , "AutoText Deleted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            FillCompaniesCombo();
        }

        private bool AutoTextExists()
        {
            bool exists = false;
            try
            {

                using (Database database = DatabaseFactory.Connect(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"]))
                {
                    IDbDataParameter[] parameter = new IDbDataParameter[1];
                    parameter[0] = database.CreateParameter("@id", selectedAutotextId, ParameterDirection.Input);
                    if (database.CountSQL(SqlText.Autotext_Exists, ref parameter) > 0)
                    {
                        exists = true;
                    }
                }

                if (!exists)
                {
                    ReloadWithMessage();
                }
            }
            catch(Exception ex)
            {
                ErrorHandler.ReportError("Unable to check if AutoText exists.", ex);
            }

            return exists;

        }

        #endregion

        #region Window Manipulation Methods

        private string GetWindowName(IntPtr windowHandle)
        {
            string windowName = string.Empty;
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            if (WinAPI.GetWindowText(windowHandle, Buff, nChars) > 0)
            {
                windowName = Buff.ToString();
            }

            return windowName;
        }

        private string GetWindowClassName(IntPtr windowHandle)
        {
            string className = string.Empty;
            int nRet;
            StringBuilder classNameBuilder = new StringBuilder(100);
            nRet = WinAPI.GetClassName(windowHandle, classNameBuilder, classNameBuilder.Capacity);
            if (nRet != 0)
            {
                className = classNameBuilder.ToString();
            }

            return className;
        }

        private IntPtr GetTopWindow()
        {
            IntPtr topWindowHandle = IntPtr.Zero;
            IntPtr nextWindowHandle = WinAPI.GetTopWindow((IntPtr)null);
            while (nextWindowHandle != IntPtr.Zero)
            {
                if (WinAPI.IsWindowVisible(nextWindowHandle) != 0) //we are only interested in open windows
                {
                    string className = GetWindowClassName(nextWindowHandle);
                    string windowName = GetWindowName(nextWindowHandle);

                    if (nextWindowHandle != this.Handle //dont want to paste to AutoText window
                        && nextWindowHandle != editFormHandle //nor the edit form
                        && windowName.Length > 0 //any valid window will have a title
                        && className != "Button") //Windows 7 (probably Vista too) alwats puts this window at the top of the z-order
                    {
                        Console.WriteLine(GetWindowName(nextWindowHandle));
                        topWindowHandle = nextWindowHandle;
                        break;
                    }
                }
                nextWindowHandle = WinAPI.GetWindow(nextWindowHandle, WinAPI.GW_HWNDNEXT);
            }

            return topWindowHandle;

        }

        private void DisplayCurrentPasteWindow()
        {
            try
            {
                IntPtr topWindowHandle = GetTopWindow();
                if (topWindowHandle != IntPtr.Zero)
                {
                    string windowName = GetWindowName(topWindowHandle);
                    this.Text = "AUTOTEXT : Pasting to " + windowName;
                }
            }
            catch
            {
                this.Text = "AUTOTEXT : ** error getting paste window **";
                // dont throw error messages on a timer event handler ...
            }
        }

        private void PasteText()
        {
            try
            {
                IntPtr topWindowHandle = GetTopWindow();
                if (topWindowHandle != IntPtr.Zero)
                {
                    WinAPI.SetForegroundWindow(topWindowHandle);

                    Clipboard.SetDataObject(autotextTextbox.Text, true);
                    SendKeys.Send("^v");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ReportError("Unable to paste text.", ex);
            }
        }

        #endregion

        #region Fields

        private int selectedAutotextId = -1;            //keep these variables in case the combos themselves stop being set
        private int selectedCompanyId = -1;
        private int selectedCountryId = -1;

        private bool expanded = false;                  // autext and buttons visisble
        private bool initialiseComboIndex = true;       // stops the index being set at the top (which is slow when getting all Autotext Elements)
        //private bool autotextChanged = false;           // dirty flag **not used**

        private IntPtr editFormHandle = IntPtr.Zero;

        #endregion

    }
}
