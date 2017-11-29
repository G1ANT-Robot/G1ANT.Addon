namespace G1ANT.UiFramework.WindowsApi
{
    public class WindowsConstants
    {
        public const uint SW_HIDE = 0;
        public const uint SW_SHOWNORMAL = 1;
        public const uint SW_NORMAL = 1;
        public const uint SW_SHOWMINIMIZED = 2;
        public const uint SW_SHOWMAXIMIZED = 3;
        public const uint SW_MAXIMIZE = 3;
        public const uint SW_SHOWNOACTIVATE = 4;
        public const uint SW_SHOW = 5;
        public const uint SW_MINIMIZE = 6;
        public const uint SW_SHOWMINNOACTIVE = 7;
        public const uint SW_SHOWNA = 8;
        public const uint SW_RESTORE = 9;
        public const uint SW_SHOWDEFAULT = 10;
        public const uint SW_FORCEMINIMIZE = 11;
        public const uint SW_MAX = 11;
      
        public const long WS_CAPTION = 0x00C00000L;
        public const long WS_DISABLED = 0x08000000L;
        public const long WS_VSCROLL = 0x00200000L;
        public const long WS_HSCROLL = 0x00100000L;
        public const long WS_MINIMIZEBOX = 0x00020000L;
        public const long WS_MAXIMIZEBOX = 0x00010000L;
        public const long WS_POPUP = 0x80000000L;
        public const long WS_SYSMENU = 0x00080000L;
        public const long WS_TABSTOP = 0x00010000L;
        public const long WS_VISIBLE = 0x10000000L;

        public static uint WPF_RESTORETOMAXIMIZED = 2;

        public const int INPUT_MOUSE = 0;
        public const int INPUT_KEYBOARD = 1;

        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int MK_LBUTTON = 0x0001;

        public const int MOUSEEVENTF_MOVE = 0x0001;
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        public const int MOUSEEVENTF_XDOWN = 0x0080;
        public const int MOUSEEVENTF_XUP = 0x0100;
        public const int MOUSEEVENTF_WHEEL = 0x0800;
        public const int MOUSEEVENTF_VIRTUALDESK = 0x4000;
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        public const int HourGlassValue = 65557;

        public const int srccopy = 0x00CC0020;
        public const uint GW_CHILD = 5;
        public const uint GW_HWNDFIRST = 0;
        public const uint GW_HWNDNEXT = 2;
        public const int WM_SETTEXT = 0x000C;
        public const uint WM_GETTEXTLENGTH = 0x000E;
        public const uint WM_GETTEXT = 0x000D;
        public const int BM_CLICK = 0x00F5;
        public static uint MF_BYPOSITION = 0x400;
        public static uint MF_REMOVE = 0x1000;
        public static int GWL_STYLE = -16;
        public static int WS_CHILD = 0x40000000;
        public static int WS_BORDER = 0x00800000;
        public static int WS_DLGFRAME = 0x00400000;
        public const int WM_SETREDRAW2 = 11;
        public const uint WS_THICKFRAME = 0x00040000;
        public const int WM_SETREDRAW = 11;
        public const uint WS_MINIMIZE = 0x20000000;
        public const uint WS_MAXIMIZE = 0x01000000;
        public const int WmPaint = 0x000F;
       
        public const uint WM_CLOSE = 0x0010;
    }
}