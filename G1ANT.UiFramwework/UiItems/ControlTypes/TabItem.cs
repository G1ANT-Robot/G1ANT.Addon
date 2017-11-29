using System.Windows.Automation;

namespace G1ANT.UiFramework.UiItems.ControlTypes
{
    class TabItem : G1ANTControl
    {
        public TabItem(int controlTypeID, AutomatedItem automatedItem) : base(controlTypeID, automatedItem)
        {
        }
        public override void Click()
        {
            try
            {
                var selectionpatern = Ptr.Element.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
                selectionpatern.Select();
            }
            catch
            {
                base.Click();
            }
        }
    }
}
