using G1ANT.UiFramework;
using G1ANT.UiFramework.Utils;
using G1ANT.UiFramework.WindowsApi;
using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Xml;

using Point = System.Drawing.Point;

namespace G1ANT.Addon.Ui
{
    public partial class UiWizard : Form
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public IntPtr curent;
        XmlDocument currentXml;
        private OverlayForm overlay;
        private static UiWizard instance;
        public static UiWizard Instance
        {
            get
            {
                if (instance == null)
                    instance = new UiWizard();
                return instance;
            }
        }
        private UiWizard()
        {
            InitializeComponent();
        }

        private void AddNode(ref XmlNode inXmlNode, ref TreeNode inTreeNode)
        {
            if (inXmlNode.HasChildNodes)
            {
                XmlNodeList nodeList = inXmlNode.ChildNodes;
                for (int i = 0; i <= nodeList.Count - 1; i++)
                {
                    XmlNode xNode = inXmlNode.ChildNodes[i];

                    inTreeNode.Nodes.Add(new TreeNode(xNode.OuterXml.Split('>').First() + ">"));
                    TreeNode tNode = inTreeNode.Nodes[i];
                    AddNode(ref xNode, ref tNode);
                }
            }
            else
            {
                inTreeNode.Text = inXmlNode.OuterXml.Trim('\\').Trim();
            }
        }
        public void xml(AutomationElement elem)
        {
            XMLGenerator.XmlIt(elem, this.checkBox1.Checked);
            LoadXml();
        }
        /// <summary>
        /// Method used to present the structure of aplication given in xml file on treeviev
        /// </summary>
        private void LoadXml()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string strXPath = "Window";
                string strRootNode = "Window";
                string strXmlFile = "window.xml";
                XmlDocument dom = new XmlDocument();
                dom.Load(strXmlFile);
                currentXml = dom;
                treeViewAppStructure.Nodes.Clear();
                treeViewAppStructure.Nodes.Add(new TreeNode(strRootNode));
                TreeNode tNode = treeViewAppStructure.Nodes[0];
                XmlNodeList oNodes = dom.SelectNodes(strXPath);
                XmlNode xNode = oNodes.Item(0).ParentNode;
                AddNode(ref xNode, ref tNode);
                treeViewAppStructure.CollapseAll();
                treeViewAppStructure.Nodes[0].Expand();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Error");
                logger.Error($"UiFramework ERROR: {ex.Message}, while loading Xml with structure");
            }
        }
        /// <summary>
        /// Generates script to click on chosen element 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick_Click(object sender, EventArgs e)
        {
            if (treeViewAppStructure.SelectedNode == null)
                MessageBox.Show("Chose item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                richTextBox1.AppendText($"ui.click wpath ‴{WPath.MakeWPath(currentXml, treeViewAppStructure.SelectedNode)}‴\n");
        }
        /// <summary>
        /// Generates script to attach to ui window of specified windowname 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAttach_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxWindowName.Text) || textBoxWindowName.Text == string.Empty)
                MessageBox.Show("Set window name!", "Window Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                richTextBox1.AppendText($"ui.attach windowname ‴{textBoxWindowName.Text}‴\n");
        }
        /// <summary>
        /// Generates script to get value from chosen element 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonGetVal_Click(object sender, EventArgs e)
        {
            if (treeViewAppStructure.SelectedNode == null)
                MessageBox.Show("Chose item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                richTextBox1.AppendText($"ui.getvalue wpath ‴{WPath.MakeWPath(currentXml, treeViewAppStructure.SelectedNode)}‴\n");
        }
        /// <summary>
        /// Generates script to put value into chosen element 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSetVal_Click(object sender, EventArgs e)
        {
            if (treeViewAppStructure.SelectedNode == null)
                MessageBox.Show("Chose item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                richTextBox1.AppendText($"ui.setvalue wpath ‴{WPath.MakeWPath(currentXml, treeViewAppStructure.SelectedNode)}‴ value ‴{textBoxSetVal.Text}‴\n");
        }
        /// <summary>
        /// Open the window with list of all opened windows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelect_Click(object sender, EventArgs e)
        {
            WindowsForm forma = new WindowsForm(this);
            forma.ShowDialog();
        }

        private void textBoxWindowName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxWindowName.Text))
                MessageBox.Show("Set window name", "Window Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (XMLGenerator.XmlIt(AutomationElement.FromHandle(curent), this.checkBox1.Checked))
                    LoadXml();
            }
        }

        private void treeViewAppStructure_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AutomatedItem ae;
            try
            {
                ae = new AutomatedItem(WPath.MakeWPath(currentXml, treeViewAppStructure.SelectedNode), AutomationElement.FromHandle(curent), checkBox1.Checked);
            }
            catch
            {
                if (overlay != null)
                    overlay.Refresh();
                ae = null;
            }

            if (ae != null)
            {
                if (checkBoxLocation.Checked)
                {
                    if (overlay == null)
                        overlay = new OverlayForm(curent);
                    overlay.Show();
                }
                drawhighlight(ae);
            }
        }

        private void drawhighlight(AutomatedItem ae)
        {
            overlay.Refresh();
            IntPtr desktopPtr = overlay.Handle;
            Graphics g = Graphics.FromHwnd(desktopPtr);
            Pen b = new Pen(Color.Green, 3);
            if (ae.Element != null)
            {
                Rectangle re = RectToRectangle(ae.Element.Current.BoundingRectangle, AutomationElement.FromHandle(curent).Current.BoundingRectangle);
                g.DrawRectangle(b, re);
                g.Dispose();
                WinApi.ReleaseDC(IntPtr.Zero, curent);
            }
        }

        Point GetRelative(System.Windows.Point w, System.Windows.Point c)
        {
            var x = c.X - w.X;
            var y = c.Y - w.Y;
            return new Point((int)x, (int)y);
        }
        Rectangle RectToRectangle(Rect rect, Rect window)
        {
            return new Rectangle()
            {
                Location = GetRelative(window.Location, rect.Location),
                Width = (int)rect.Width,
                Height = (int)rect.Height
            };
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (overlay != null)
            {
                if (overlay.Visible)
                    overlay.Hide();
                else if (!overlay.Visible)
                {
                    treeViewAppStructure_AfterSelect(null, null);
                }
            }
        }

        private void UiWizard_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (overlay != null)
                overlay.Hide();
        }
        /// <summary>
        /// Refresh the treeviev structure of application, make new xml and load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxWindowName.Text))
                MessageBox.Show("Select the app first", "Window Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (XMLGenerator.XmlIt(AutomationElement.FromHandle(curent), this.checkBox1.Checked))
                    LoadXml();
            }
        }

        private void buttonWaitFor_Click(object sender, EventArgs e)
        {
            if (treeViewAppStructure.SelectedNode == null)
                MessageBox.Show("Chose item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                richTextBox1.AppendText($"ui.waitfor wpath ‴{WPath.MakeWPath(currentXml, treeViewAppStructure.SelectedNode)}‴\n");
        }
        /// <summary>
        /// Generates script to wait for specified element 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UiWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            if (overlay != null)
                overlay.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var g = EventMonitor.SubscribeToTextChangedEvent(new AutomatedItem(WPath.MakeWPath(currentXml, treeViewAppStructure.SelectedNode), AutomationElement.FromHandle(curent), checkBox1.Checked).Element, mb);
        }
        public void mb(Object e, AutomationEventArgs args)
        {
            this.Invoke(new Action(delegate ()
            {
                MessageBox.Show("text changed");
            }));
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (treeViewAppStructure.SelectedNode == null)
                MessageBox.Show("Chose item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                richTextBox1.AppendText($"ui.getposition wpath ‴{WPath.MakeWPath(currentXml, treeViewAppStructure.SelectedNode)}‴\n");
        }
    }
}
