using System.Windows.Automation;

namespace G1ANT.UiFramework.UiItems.ControlTypes
{/// <summary>
/// ListItem control class updating methods of G1ANTControl to work with list items
/// </summary>
    class ListItem : G1ANTControl
    {
        public ListItem(int controlTypeID, AutomatedItem automatedItem) : base(controlTypeID, automatedItem)
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
