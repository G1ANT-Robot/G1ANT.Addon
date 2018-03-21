using System;
using G1ANT.Language;

namespace G1ANT.Addon.UI
{
    [Command(Name = "ui.click",
        Tooltip = "Click control of the desktop application described by WPathStructure")]
    public class UIClickCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "WPath structure defining control of the desktop application")]
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
