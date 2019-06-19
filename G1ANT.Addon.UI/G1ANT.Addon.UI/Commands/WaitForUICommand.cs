using System;
using G1ANT.Language;
using System.Windows.Forms;
using System.Threading;

namespace G1ANT.Addon.UI
{
    [Command(Name = "waitfor.ui",
        Tooltip = "This command waits for a UI element of a desktop application specified by WPath structure")]
    public class WaitForUICommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Desktop application UI element to be awaited")]
            public WPathStructure WPath { get; set; }

            [Argument(DefaultVariable = "timeoutui", Tooltip = "Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed")]
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
                Thread.Sleep(250);
                Application.DoEvents();
            }
            string msg;
            if (UIElement.RootElement == null)
                msg = $"Control described as \"{arguments.WPath.Value}\" has not been found.";
            else
                msg = $"Control described as \"/{UIElement.RootElement.ToWPath().Value}/{arguments.WPath.Value}\" has not been found.";

            Scripter.Log.Log(AbstractLogger.Level.Debug, msg);
            throw new TimeoutException(msg);
        }
    }
}
