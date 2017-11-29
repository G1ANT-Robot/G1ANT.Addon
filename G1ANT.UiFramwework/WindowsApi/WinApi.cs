using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace G1ANT.UiFramework.WindowsApi
{
    public static class WinApi
    {
        #region delegates

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);
        public delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        #endregion
        #region Structures
        
        [StructLayout(LayoutKind.Sequential)]
        public struct COLORREF
        {
            public byte R;
            public byte G;
            public byte B;

            public override string ToString()
            {
                return string.Format($"R={R},G={G},B={B}");
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        #endregion
        #region Flags & Enums

        [Flags()]
        public enum RedrawWindowFlags : uint
        {
            /// <summary>
            /// Invalidates the rectangle or region that you specify in lprcUpdate or hrgnUpdate.
            /// You can set only one of these parameters to a non-NULL value. If both are NULL, RDW_INVALIDATE invalidates the entire window.
            /// </summary>
            Invalidate = 0x1,

            /// <summary>Causes the OS to post a WM_PAINT message to the window regardless of whether a portion of the window is invalid.</summary>
            InternalPaint = 0x2,

            /// <summary>
            /// Causes the window to receive a WM_ERASEBKGND message when the window is repainted.
            /// Specify this value in combination with the RDW_INVALIDATE value; otherwise, RDW_ERASE has no effect.
            /// </summary>
            Erase = 0x4,

            /// <summary>
            /// Validates the rectangle or region that you specify in lprcUpdate or hrgnUpdate.
            /// You can set only one of these parameters to a non-NULL value. If both are NULL, RDW_VALIDATE validates the entire window.
            /// This value does not affect internal WM_PAINT messages.
            /// </summary>
            Validate = 0x8,

            NoInternalPaint = 0x10,

            /// <summary>Suppresses any pending WM_ERASEBKGND messages.</summary>
            NoErase = 0x20,

            /// <summary>Excludes child windows, if any, from the repainting operation.</summary>
            NoChildren = 0x40,

            /// <summary>Includes child windows, if any, in the repainting operation.</summary>
            AllChildren = 0x80,

            /// <summary>Causes the affected windows, which you specify by setting the RDW_ALLCHILDREN and RDW_NOCHILDREN values, to receive WM_ERASEBKGND and WM_PAINT messages before the RedrawWindow returns, if necessary.</summary>
            UpdateNow = 0x100,

            /// <summary>
            /// Causes the affected windows, which you specify by setting the RDW_ALLCHILDREN and RDW_NOCHILDREN values, to receive WM_ERASEBKGND messages before RedrawWindow returns, if necessary.
            /// The affected windows receive WM_PAINT messages at the ordinary time.
            /// </summary>
            EraseNow = 0x200,

            Frame = 0x400,

            NoFrame = 0x800
        }
        public enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };
        #endregion
        #region Imports User32
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        public static string GetWindowText(IntPtr h)
        {
            StringBuilder str = new StringBuilder(4096);
            GetWindowText(h, str, 4096);
            return str.ToString();
        }

        [DllImport("user32.dll")]
        public static extern int SendMessages(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        [DllImport("user32.dll")]
        public static extern bool LockWindowUpdate(IntPtr hWndLock);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowEnabled(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr LoadCursorFromFile(string path);

        [DllImport("user32.dll")]
        public static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, RedrawWindowFlags flags);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, UInt32 dwData, IntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(int xPoint, int yPoint);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, StringBuilder lParam);

        [DllImport("User32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindows);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);

        [DllImport("User32.dll")]
        public static extern IntPtr GetWindow(IntPtr hwnd, uint uCmd);

        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern long GetClassName(IntPtr hwnd, StringBuilder lpClassName, long nMaxCount);

        [DllImport("User32.dll")]
        static extern IntPtr GetParent(IntPtr hwnd);

        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern long GetWindowText(IntPtr hwnd, StringBuilder lpString, long cch);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        public static extern int FindWindow(string strClassName, string strWindowName);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(UInt16 virtualKeyCode);

        [DllImport("USER32.dll")]
        public static extern short GetKeyState(int nVirtKey);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        public static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        [DllImport("USER32.DLL")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("USER32.DLL")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("USER32.DLL")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr GetMenu(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll")]
        public static extern bool DrawMenuBar(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cX, int cY, uint uFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

        [DllImport("user32.dll", EntryPoint = "FrameRect")]
        public static extern int FrameRect(IntPtr hdc, ref RECT rect, IntPtr hbrush);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hwndAfter, int x, int y, int width, int height, int flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        #endregion
    }
}
