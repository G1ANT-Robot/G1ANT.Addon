using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G1ANT.Addon.Mscrm
{
    public partial class MsCrmSetValueForm : Form
    {
        public string ResultValue { get; set; } = string.Empty;
        public ResultType ResultType { get; set; }
        public string ValidationErrorMessage { get; set; }

        private static bool positionSet = false;
        private static int xPosition;
        private static int yPosition;

        public MsCrmSetValueForm(string fieldSearchPhrase, List<string> availableOptions)
        {
            InitializeComponent();
            FieldSearchingPhrase.Text = $"Field search phrase: {fieldSearchPhrase}";
            if (ActionComboBox.Items.Count > 0)
                ActionComboBox.SelectedIndex = 0;
            if (availableOptions?.Count > 0)
            {
                ValueTextBox.Visible = false;
                ValueComboBox.Visible = true;
                ValueComboBox.DataSource = availableOptions;
                ValueComboBox.SelectedIndex = 0;
            }
        }

        private void MsCrmSetValueForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
                DialogResult = DialogResult.Cancel;
        }

        private void AddCommandButton_Click(object sender, EventArgs e)
        {
            ResultValue = ValueComboBox.SelectedItem?.ToString() ?? ValueTextBox.Text ?? string.Empty;
            switch (ActionComboBox.SelectedItem?.ToString().ToLower())
            {
                case "click":
                    ResultType = ResultType.Click;
                    break;
                case "setvalue":
                    ResultType = ResultType.SetValue;
                    if (ValidationErrorCheckBox.Checked)
                    {
                        ValidationErrorMessage = ValidationErrorMsgTextBox.Text;
                    }
                    break;
            }
            DialogResult = DialogResult.OK;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        public void Locate(int posX, int posY)
        {
            if (positionSet == false)
            {
                var x = posX + 50;
                var y = posY + 50;
                xPosition = x;
                yPosition = y;
                Location = new Point(x, y);
                positionSet = true;
            }
            else
            {
                Location = new Point(xPosition, yPosition);
            }
        }

        private void ActionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool valueEnabled = true;
            bool validationMessageEnabled = true;
            switch (ActionComboBox.SelectedItem?.ToString().ToLower())
            {
                case "click":
                    valueEnabled = false;
                    validationMessageEnabled = false;
                    break;
            }
            ValueTextBox.Enabled = valueEnabled;
            ValidationErrorCheckBox.Enabled = validationMessageEnabled;
            ValidationErrorMsgTextBox.Enabled = validationMessageEnabled;
        }

        private void ValidationErrorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ValidationErrorMsgTextBox.ReadOnly = !ValidationErrorCheckBox.Checked;
        }

        private void ValidationErrorMsgTextBox_LocationChanged(object sender, EventArgs e)
        {
            positionSet = true;
            xPosition = Location.X;
            yPosition = Location.Y;
        }

        private void MsCrmSetValueForm_LocationChanged(object sender, EventArgs e)
        {
            if (Location.X > 0)
                xPosition = Location.X;
            if (Location.Y > 0)
                yPosition = Location.Y;
        }


        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOACTIVATE = 0x001;
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 SWP_SHOWWINDOW = 0x004;
        private const UInt32 SWP_NOZORDER = 0x0004;

        private const UInt32 TOPMOST_FLAGS = SWP_NOSIZE | SWP_NOMOVE;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);


        private void MsCrmSetValueForm_Shown(object sender, EventArgs e)
        {
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }
    }

    public enum ResultType
    {
        SetValue = 0,
        Click = 1
    }
}
