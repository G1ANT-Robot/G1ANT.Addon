using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using G1ANT.Language;

namespace G1ANT.Addon.Ui
{
    public partial class WindowsForm : Form
    {
        List<String> names = new List<string>();
        List<IntPtr> pointers = new List<IntPtr>();
        private UiWizard wiz;
        public WindowsForm(UiWizard wiza)
        {
            InitializeComponent();
            try
            {
                wiz = wiza;
                
                IntPtr nDeshWndHandle = RobotWin32.GetDesktopWindow();
                IntPtr nChildHandle = RobotWin32.GetWindow(nDeshWndHandle, RobotWin32.GW_CHILD);
                while (nChildHandle != IntPtr.Zero)
                {
                    if (nChildHandle == Handle)
                        nChildHandle = RobotWin32.GetWindow(nChildHandle, RobotWin32.GW_HWNDNEXT);

                    if (RobotWin32.IsWindowVisible(nChildHandle) != IntPtr.Zero)
                    {
                        StringBuilder sbTitle = new StringBuilder(1024);
                        RobotWin32.GetWindowText(nChildHandle, sbTitle, sbTitle.Capacity);
                        String sWinTitle = sbTitle.ToString();
                        if (sWinTitle.Length > 0)
                        {
                            names.Add(sWinTitle);
                            pointers.Add(nChildHandle);
                        }
                           
                    }
                    nChildHandle = RobotWin32.GetWindow(nChildHandle, RobotWin32.GW_HWNDNEXT);
                }
                    Text = "All windows";
                listBox1.DataSource = names;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wiz.curent = pointers[listBox1.SelectedIndex];
            wiz.textBoxWindowName.Text = listBox1.SelectedItem.ToString();
            Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            button1_Click(sender, e);
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}
