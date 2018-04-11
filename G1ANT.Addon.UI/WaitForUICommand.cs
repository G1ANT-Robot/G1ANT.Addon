using System;
using G1ANT.Language;
using System.Windows.Forms;

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
            public override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(10000);

        }

        public WaitForUICommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            int timeout = (int)arguments.Timeout.Value.TotalMilliseconds;
            long start = Environment.TickCount;
            bool found = false;
            while (Math.Abs(Environment.TickCount - start) < timeout &&
                   Scripter.Stopped == false &&
                   found == false)
            {
                try
                {
                    var element = UIElement.FromWPath(arguments.WPath);
                    if (element != null)
                        return;
                }
                catch
                { }
                Application.DoEvents();
            }
            string msg = $"Control described as \"{arguments.WPath.Value}\" has not been found.";
            Scripter.Log.Log(AbstractLogger.Level.Debug, msg);
            throw new TimeoutException(msg);
        }
    }
}
