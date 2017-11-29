using System;
using System.Drawing;
using System.Windows.Forms;

using G1ANT.UiFramework.WindowsApi;
using System.Windows.Automation;

namespace G1ANT.UiFramework.Utils
{/// <summary>
/// Form used to draw real time overlay on app to show chosen control
/// </summary>
    public partial class OverlayForm : Form
    {
        public WinApi.RECT rect;
        public IntPtr hand;
        public OverlayForm(IntPtr hndIntPtr)
        {
            hand = hndIntPtr;
            InitializeComponent();
        }

        private void OverlayForm_Load(object sender, EventArgs e)
        {
            this.Name = "G1ANT UiWizard";
            this.Text = "G1ANT UiWizard";
            this.ShowIcon = false;
            this.BackColor = Color.Red;
            this.TransparencyKey = Color.Red;
            this.TopMost = true;
            int initialStyle = WinApi.GetWindowLong(this.Handle, -20);
            WinApi.SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);
            WinApi.GetWindowRect(hand, ref rect);
            this.Size = new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);
            this.Left = rect.Left;
            this.Top = rect.Top;

            WinApi.SetWindowLong(this.Handle, -20, initialStyle | (int)0x80000 | (int)0x20);
            EventMonitor.Monitorsize(AutomationElement.FromHandle(hand), UpdateSize);
        }
        public void UpdateSize(Object o, AutomationEventArgs args)
        {
            this.Invoke(new Action(delegate ()
            {
                this.Refresh();
                WinApi.GetWindowRect(hand, ref rect);
                this.Size = new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);
                this.Left = rect.Left;
                this.Top = rect.Top;
            }));

        }

    }
}
