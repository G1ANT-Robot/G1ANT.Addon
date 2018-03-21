using System;
using G1ANT.Language;

namespace G1ANT.Addon.UI
{
    [Command(Name = "ui.settext",
        Tooltip = "Set text of the control of desktop application described by WPathStructure")]
    public class UISetTextCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "WPath structure defining control of the desktop application")]
            public WPathStructure WPath { get; set; }

            [Argument(Required = true, Tooltip = "Variable where the text of the cotrol will be returned")]
            public TextStructure Text { get; set; }
        }

        public UISetTextCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            var element = UIElement.FromWPath(arguments.WPath);
            if (element != null)
            {
                element.SetText(arguments.Text.Value);
            }
        }
    }
}
