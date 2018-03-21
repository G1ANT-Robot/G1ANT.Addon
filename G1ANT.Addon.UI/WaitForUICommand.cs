using System;
using G1ANT.Language;

namespace G1ANT.Addon.UI
{
    [Command(Name = "waitfor.ui",
        Tooltip = "Wait for the control of desktop application described by WPathStructure")]
    public class WaitForUICommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "WPath structure defining control of the desktop application")]
            public WPathStructure WPath { get; set; }

            [Argument(DefaultVariable = "timeoutui")]
            public override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(5000);

        }

        public WaitForUICommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
        }
    }
}
