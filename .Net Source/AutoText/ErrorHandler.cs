using System;
using System.Windows.Forms;

namespace AutoText
{
    public static class ErrorHandler
    {
        public static void ReportError(string message, Exception ex)
        {
            MessageBox.Show(message + "\n\nError: " + ex.ToString()
                , "AutoText Error"
                , MessageBoxButtons.OK
                , MessageBoxIcon.Error);
        }

    }
}
