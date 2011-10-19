using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace AutoText
{
    public class WinAPI
    {
        [DllImport("user32.dll")]
        public static extern int FindWindow(
            string lpClassName // class name 
            ,string lpWindowName // window name 
        );

        [DllImport("user32.dll")]
        public static extern int SendMessage(
            IntPtr hWnd // handle to destination window 
            ,uint Msg // message 
            ,int wParam // first message parameter 
            ,int lParam // second message parameter 
        );

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(
            IntPtr hWnd // handle to window
        );

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(
            IntPtr hWnd
            , uint uCmd
        );

        [DllImport("user32.dll")]
        public static extern IntPtr GetTopWindow(
            IntPtr hWnd
        );

        [DllImport("user32.dll")]
        public static extern int GetWindowText(
            IntPtr hWnd
            , StringBuilder text
            , int count
        );

        [DllImport("user32.dll")]
        public static extern int GetClassName(
            IntPtr hWnd
            , StringBuilder lpClassName
            , int nMaxCount
        );

        [DllImport("user32")]
        public static extern int EnumWindows(
            EnumWindowsProcDelegate lpEnumFunc
            , int lParam
        );

        [DllImport("user32", EntryPoint = "GetWindowLongA")]
        public static extern int GetWindowLongPtr(
            IntPtr hwnd
            , int nIndex
        );

        [DllImport("user32")]
        public static extern int GetParent(
            IntPtr hwnd
        );

        [DllImport("user32")]
        public static extern int IsWindowVisible(
            IntPtr hwnd
        );

        [DllImport("user32")]
        public static extern int GetDesktopWindow();

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;

        private const int GWL_EXSTYLE = (-20);
        private const int WS_EX_TOOLWINDOW = 0x80;
        private const int WS_EX_APPWINDOW = 0x40000;

        public const int WM_WINDOWPOSCHANGING = 0x0046;

        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;

        public delegate int EnumWindowsProcDelegate(int hWnd, int lParam);


    }
}
