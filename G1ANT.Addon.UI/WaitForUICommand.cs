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
            //string wpath = arguments.Wpath.Value;
            //int timeout = arguments.Timeout.Value;
            //long start = Environment.TickCount;
            //bool found = false;
            //while (Math.Abs(Environment.TickCount - start) < timeout &&
            //       ShouldStopScript() == false &&
            //       found == false)
            //{
            //    found = UiManager.Wait(wpath);

            //    System.Windows.Forms.Application.DoEvents();
            //    Thread.Sleep(60);
            //}
            //SetVariableValue(arguments.Result.Value, new Structures.Bool(found));
            //if (!found)
            //{
            //    throw new ApplicationException("Control couldn't be found");
            //}
        }
    }
}
