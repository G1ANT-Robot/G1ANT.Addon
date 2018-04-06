using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using G1ANT.UI;
using System.Windows.Automation;

namespace G1ANT.Addon.UI
{
    [PanelAttribute(Title = "UI Applications", DockingSide = DockingSide.Right, InitialAppear = true)]
    public partial class UIControlsPanel : RobotPanel
    {
        public UIControlsPanel()
        {
            InitializeComponent();
        }

        public override void Initialize(IMainForm mainForm)
        {
            InitRootElement();
        }

        public override void RefreshContent()
        {
        }

        private void controlsTree_AfterCollapse(object sender, TreeViewEventArgs e)
        {

        }

        private void controlsTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            if (e.Node.Tag is AutomationElement element)
            {
                AutomationElement elem = TreeWalker.ControlViewWalker.GetFirstChild(element);
                while (elem != null)
                {
                    string str = $"\"{elem.Current.Name}\" - {elem.Current.LocalizedControlType} (#{elem.Current.AutomationId})";
                    var node = e.Node.Nodes.Add(str);
                    node.Tag = elem;
                    node.Nodes.Add("");

                    elem = TreeWalker.ControlViewWalker.GetNextSibling(elem);
                }
            }
        }

        private void controlsTree_DoubleClick(object sender, EventArgs e)
        {

        }

        private void InitRootElement()
        {
            controlsTree.Nodes.Clear();
            var root = AutomationElement.RootElement;
            var rootNode = controlsTree.Nodes.Add("Desktop");
            rootNode.Tag = root;
            rootNode.Nodes.Add("");
            rootNode.Expand();
        }
    }
}
