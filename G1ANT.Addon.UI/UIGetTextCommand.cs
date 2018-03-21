using System;
using G1ANT.Language;

namespace G1ANT.Addon.UI
{
    [Command(Name = "ui.gettext",
        Tooltip = "Get text of the control of desktop application described by WPathStructure")]
    public class UIGetTextCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "WPath structure defining control of the desktop application")]
            public WPathStructure WPath { get; set; }

            [Argument(Required = true, Tooltip = "Variable where the text of the cotrol will be returned")]
            public VariableStructure Result { get; set; }
        }

        public UIGetTextCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
        }
    }
}
