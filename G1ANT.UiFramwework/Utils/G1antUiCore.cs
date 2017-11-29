using G1ANT.UiFramework.WindowsApi;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Automation;
using System.Windows.Forms;

namespace G1ANT.UiFramework.Utils
{/// <summary>
/// Core Functions Class
/// </summary>
    public static class G1antUiCore
    {   /// <summary>
    /// Function used to find main window element based on  proces name and window title
    /// </summary>
    /// <param name="procesname">Name of process to look for main window</param>
    /// <param name="windowname">Title of window to find</param>
    /// <returns></returns>
        public static AutomationElement FindMain(string procesname, string windowname)
        {
            AutomationElement main = null;
            List<PropertyCondition> conditions = new List<PropertyCondition>();
            var processss = Process.GetProcessesByName(procesname);
            PropertyCondition condition4 = new PropertyCondition(AutomationElement.NameProperty, windowname);
            conditions.Add(condition4);
            PropertyCondition condition5 = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);
            conditions.Add(condition5);

            TreeWalker walkerek = new TreeWalker(new AndCondition(conditions.ToArray()));

            for (int i = 0; i < processss.Count(); i++)
            {

                Process proces = Process.GetProcessesByName(procesname)[i];
                AutomationElement nowe = AutomationElement.FromHandle(proces.MainWindowHandle);

                main = walkerek.Normalize(nowe);
                if (main != null)
                {
                    break;
                }
            }
            return main;
        }
        /// <summary>
        /// Function used to find main window element based on window title 
        /// </summary>
        /// <param name="windowname">Title of window to find</param>
        /// <returns></returns>
        public static AutomationElement FindMain(string windowname)
        {
            AutomationElement main = null;
            List<PropertyCondition> conditions = new List<PropertyCondition>();
            var processss = Process.GetProcesses();
            PropertyCondition condition4 = new PropertyCondition(AutomationElement.NameProperty, windowname);
            conditions.Add(condition4);
            PropertyCondition condition5 = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);
            conditions.Add(condition5);

            TreeWalker walkerek = new TreeWalker(new AndCondition(conditions.ToArray()));

            for (int i = 0; i < processss.Count(); i++)
            {
                Process proces = processss[i];
                if (proces.MainWindowHandle == IntPtr.Zero)
                {
                    continue;
                }
                AutomationElement nowe = AutomationElement.FromHandle(proces.MainWindowHandle);

                main = walkerek.Normalize(nowe);
                if (main != null)
                {
                    break;
                }
            }
            return main;
        }
        /// <summary>
        /// Method used to capture visible part of the screen
        /// </summary>
        /// <param name="windowHandle">Pointer to control handler which schould be captured</param>
        /// <returns></returns>
        public static Bitmap GetVisibleImage(IntPtr windowHandle)
        {
            var compatibleDeviceContext = IntPtr.Zero;
            var deviceContext = IntPtr.Zero;
            IntPtr bitmap = IntPtr.Zero;
            Image img;
            try
            {
                deviceContext = WinApi.GetWindowDC(windowHandle);
                var rect = new WinApi.RECT();
                WinApi.GetWindowRect(windowHandle, ref rect);
                int width = rect.Right - rect.Left;
                int height = rect.Bottom - rect.Top;
                compatibleDeviceContext = Gdi.CreateCompatibleDC(deviceContext);
                bitmap = Gdi.CreateCompatibleBitmap(deviceContext, width, height);
                IntPtr @object = Gdi.SelectObject(compatibleDeviceContext, bitmap);
                Gdi.BitBlt(compatibleDeviceContext, 0, 0, width, height, deviceContext, 0, 0, WindowsConstants.srccopy);
                Gdi.SelectObject(compatibleDeviceContext, @object);
            }
            finally
            {
                Gdi.DeleteDC(compatibleDeviceContext);
                WinApi.ReleaseDC(windowHandle, deviceContext);
                img = Image.FromHbitmap(bitmap);
                Gdi.DeleteObject(bitmap);
            }
            using (img) return new Bitmap(img);
        }
        /// <summary>
        /// Gets the color of backgrount in specified handler
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static uint BackgroundColor(IntPtr handle)
        {
            return Gdi.GetBkColor(WinApi.GetDC(handle));
        }
        /// <summary>
        /// Gets the color of text in specified handle
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static uint TextColor(IntPtr handle)
        {
            return Gdi.GetTextColor(WinApi.GetDC(handle));
        }
        /// <summary>
        /// Method used to close the window
        /// </summary>
        /// <param name="handle">Pionter to widow handler to close</param>
        public static void PostCloseMessage(IntPtr handle)
        {
            WinApi.PostMessage(handle, WindowsConstants.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }
        /// <summary>
        /// Method allows to draw rectangle on specified window handler 
        /// </summary>
        /// <param name="hWnd">Handrler to draw on</param>
        /// <param name="penWidth">Pen Width</param>
        /// /// <param name="color">Color of pen</param>
        public static void DrawRect(IntPtr hWnd, float penWidth, Color color)
        {
            WinApi.RECT rc = new WinApi.RECT();
            WinApi.GetWindowRect(hWnd, ref rc);

            IntPtr hDC = WinApi.GetWindowDC(hWnd);
            if (hDC != IntPtr.Zero)
            {
                using (Pen pen = new Pen(color, penWidth))
                {
                    using (Graphics g = Graphics.FromHdc(hDC))
                    {
                        Rectangle rect = new
                            Rectangle(0, 0, rc.Right - rc.Left - (int)penWidth, rc.Bottom - rc.Top - (int)penWidth);
                        g.DrawRectangle(pen, rect);
                    }
                }
            }
            WinApi.ReleaseDC(hWnd, hDC);
        }
        /// <summary>
        /// Method used to get text from control
        /// </summary>
        /// <param name="handle">Pointer to control handler</param>
        /// <returns>String</returns>
        public static string GetTextFromField(IntPtr handle)
        {
            return GetTextFromHandler(handle);
        }
        /// <summary>
        /// Method to get main Window handler from process id
        /// </summary>
        /// <param name="id">Process ID</param>
        /// <returns></returns>
        public static IntPtr GetProcessMainWindowHandler(int id)
        {
            return GetProcessMainWindow(id);
        }
        /// <summary>
        /// Method used to get cursor position on screen
        /// </summary>
        /// <returns>Point of cursor</returns>
        public static Point GetCursorPosition()
        {
            WinApi.POINT lpPoint;
            WinApi.GetCursorPos(out lpPoint);
            return lpPoint;
        }
        /// <summary>
        /// Method used to click on control
        /// </summary>
        /// <param name="handlername">Pointer to control handler</param>
        public static void Click(IntPtr handlername)
        {
            WinApi.PostMessage(handlername, WindowsConstants.BM_CLICK, IntPtr.Zero, IntPtr.Zero);
        }
        /// <summary>
        /// Method used to instert string into control
        /// </summary>
        /// <param name="handlername">Pointer to control handler</param>
        /// <param name="message">String to insert</param>
        public static void InsertText(IntPtr handlername, string message)
        {
            InsertTextIntoHandler(handlername, message);
        }
        /// <summary>
        /// Method used to change cursor on custom cursor from file
        /// </summary>
        /// <param name="path">Path to file with cursor</param>
        /// <returns></returns>
        public static Cursor LoadCustomCursor(string path)
        {
            IntPtr hCurs = WinApi.LoadCursorFromFile(path);
            if (hCurs == IntPtr.Zero) throw new Win32Exception();
            var curs = new Cursor(hCurs);
            var fi = typeof(Cursor).GetField("ownHandle", BindingFlags.NonPublic | BindingFlags.Instance);
            fi.SetValue(curs, true);
            return curs;
        }
        /// <summary>
        /// Method to redraw window
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lprcUpdate"></param>
        /// <param name="hrgnUpdate"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, WinApi.RedrawWindowFlags flags)
        {
            return WinApi.RedrawWindow(hWnd, lprcUpdate, hrgnUpdate, flags);
        }
        /// <summary>
        /// Method used to get window handler of control visible on specified cordinates
        /// </summary>
        /// <param name="xPoint">x position of point</param>
        /// <param name="yPoint">y position of point</param>
        /// <returns></returns>
        public static IntPtr WindowFromPoint(int xPoint, int yPoint)
        {
            return WinApi.WindowFromPoint(xPoint, yPoint);
        }
        /// <summary>
        /// Method allows to get all windows from process
        /// </summary>
        /// <param name="processId">Process ID</param>
        /// <returns></returns>
        public static List<IntPtr> ReadAllWindowsFromProcessID(int processId)
        {
            return (List<IntPtr>)EnumerateProcessWindowHandles(processId);
        }
        /// <summary>
        /// Gets all children form pointer
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static List<IntPtr> GetAllChildElements(IntPtr parent)
        {
            return (List<IntPtr>)GetChildWindows(parent);
        }

        private static IEnumerable<IntPtr> GetChildWindows(IntPtr parent)
        {
            var result = new List<IntPtr>();
            var listHandle = GCHandle.Alloc(result);
            try
            {
                WinApi.EnumChildWindows(parent, EnumChildWindowsCallback, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }

        private static string GetTextFromHandler(IntPtr handle)
        {
            var length = WinApi.SendMessage(handle, WindowsConstants.WM_GETTEXTLENGTH, IntPtr.Zero, null);
            var sb = new StringBuilder(length + 1);
            WinApi.SendMessage(handle, WindowsConstants.WM_GETTEXT, (IntPtr)sb.Capacity, sb);
            return sb.ToString();
        }

        private static bool EnumChildWindowsCallback(IntPtr handle, IntPtr pointer)
        {
            var gcHandle = GCHandle.FromIntPtr(pointer);

            var list = gcHandle.Target as List<IntPtr>;

            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }
            list.Add(handle);

            return true;
        }

        private static IEnumerable<IntPtr> EnumerateProcessWindowHandles(int processId)
        {
            var handles = new List<IntPtr>();
            foreach (ProcessThread thread in Process.GetProcessById(processId).Threads)
                WinApi.EnumThreadWindows(thread.Id,
                    (hWnd, lParam) => { handles.Add(hWnd); return true; }, IntPtr.Zero);
            return handles;
        }

        private static void InsertTextIntoHandler(IntPtr handlername, string message)
        {
            WinApi.SendMessage(handlername, WindowsConstants.WM_SETTEXT, IntPtr.Zero, new StringBuilder(message));
        }

        private static IntPtr GetProcessMainWindow(int id)
        {
            return Process.GetProcessById(id).MainWindowHandle;
        }
    }
}

