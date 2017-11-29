using System.Windows.Automation;

namespace G1ANT.UiFramework.UiItems.ControlTypes
{
    class MenuItem : G1ANTControl
    {
        public MenuItem(int controlTypeID, AutomatedItem automatedItem) : base(controlTypeID, automatedItem)
        {
        }
        public override void Click()
        {
            try
            {
                var invokePattern = Ptr.Element.GetCurrentPattern(ExpandCollapsePattern.Pattern) as ExpandCollapsePattern;
                if (invokePattern.Current.ExpandCollapseState != ExpandCollapseState.Expanded)
                    invokePattern.Expand();
                else
                    invokePattern.Collapse();
            }
            catch
            {
                base.Click();
            }
        }
    }
}
