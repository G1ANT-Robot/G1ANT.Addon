using System;
using G1ANT.Language;

namespace G1ANT.Addon.UI
{
    [Command(Name = "ui.setfocus",
        Tooltip = "Set focus on the control of desktop application described by WPathStructure")]
    public class UISetFocusCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "WPath structure defining control of the desktop application")]
            public WPathStructure WPath { get; set; }

            [Argument(Required = true, Tooltip = "Variable where the text of the cotrol will be returned")]
            public TextStructure Text { get; set; }
        }

        public UISetFocusCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            var element = UIElement.FromWPath(arguments.WPath);
            if (element != null)
            {
                element.SetFocus();
            }
        }
    }
}
