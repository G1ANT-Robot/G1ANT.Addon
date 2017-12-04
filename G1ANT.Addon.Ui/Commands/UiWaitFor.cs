using G1ANT.Language;
using System;

using System.Threading;


namespace G1ANT.Addon.Ui
{
    [Command(Name = "ui.waitfor", Tooltip = ".")]
    public class UiWaitForCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public TextStructure Wpath { get; set; }

            [Argument(DefaultVariable = "timeoutui")]
            public override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(10000);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
            
        }
        public UiWaitForCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            string wpath = arguments.Wpath.Value;
            double timeout = arguments.Timeout.Value.TotalMilliseconds;
            long start = Environment.TickCount;
            bool found = false;
            while (Math.Abs(Environment.TickCount - start) < timeout &&
                   Scripter.Stopped == false &&
                   found == false)
            {
                found = UiManager.Wait(wpath);

                System.Windows.Forms.Application.DoEvents();
                Thread.Sleep(60);
            }
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(found));
            if (!found)
            {
                throw new ControlNotFoundException("Control couldn't be found:");
            }
        }
    }
}
