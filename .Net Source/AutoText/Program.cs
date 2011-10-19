using System;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Configuration;
using TSDatabase;

namespace AutoText
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (RunningInstance() != null)
            {
                MessageBox.Show("AutoText is already running.", "AutoText Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (TestDatabaseConnection())
                {
                    Application.Run(new AutoTextForm());
                }
                else
                {
                    MessageBox.Show("AutoText cannot connect to the database.\n\nPlease check with the system administrator that this computer is connected to the network and that the connection string in AutoText.exe.config is correct.", "AutoText Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// To check if other instances are already running
        /// </summary>
        static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            //Loop through the running processes in with the same name 
            foreach (Process process in processes)
            {
                //Ignore the current process 
                if (process.Id != current.Id)
                {
                    //Make sure that the process is running from the exe file. 
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        //Return the other process instance.  
                        return process;
                    }
                }
            }
            //No other instance was found, return null.  
            return null;
        }

        static bool TestDatabaseConnection()
        {
            bool databaseConnectionOK = false;

            try
            {
                using (Database database = DatabaseFactory.Connect(ConfigurationManager.ConnectionStrings["DatabaseConnectionString"]))
                {
                    if (database != null)
                    {
                        databaseConnectionOK = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to database.\n\nError: " + ex.Message, "AutoText Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return databaseConnectionOK;
        }
    }
}
