using System;
using G1ANT.Language;

namespace G1ANT.Addon.UI
{
    [Command(Name = "ui.click",
        Tooltip = "This command clicks a desktop application UI element specified by WPath structure")]
    public class UIClickCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Desktop application UI element to be clicked")]
            public WPathStructure WPath { get; set; }
        }

        public UIClickCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            var element = UIElement.FromWPath(arguments.WPath);
            if (element != null)
            {
                element.Click();
            }
        }
    }
}
