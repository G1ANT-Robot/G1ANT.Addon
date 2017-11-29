using System;
using System.Runtime.InteropServices;

namespace G1ANT.UiFramework.WindowsApi
{
    public static class Gdi
    {
        #region Imports Gdi32

        [DllImport("GDI32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int nYSrc, int dwRop);

        [DllImport("GDI32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

        [DllImport("GDI32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("GDI32.dll")]
        public static extern bool DeleteDC(IntPtr hDC);

        [DllImport("GDI32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("GDI32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern uint GetBkColor(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern uint GetTextColor(IntPtr hdc);
        #endregion
    }
}
