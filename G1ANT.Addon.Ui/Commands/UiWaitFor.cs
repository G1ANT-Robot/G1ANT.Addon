using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;
using System;

using G1ANT.Language.Ui.Api;
using System.Threading;
using G1ANT.Language.Ui.Exceptions;

namespace G1ANT.Language.Ui.Commands
{
    [Command(Name = "ui.waitfor", ToolTip = ".")]
    public class UiWaitFor : CommandBase<UiWaitFor.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public Structures.String Wpath { get; set; }

            [Argument(DefaultVariable = "timeoutui")]
            public override Structures.Integer Timeout { get; set; } = new Structures.Integer(10000);

            [Argument]
            public Structures.String Result { get; set; } = new Structures.String("result");

            [Argument]
            public Structures.Bool If { get; set; } = new Structures.Bool(true);

            [Argument]
            public Structures.String ErrorJump { get; set; }


            [Argument]
            public Structures.String ErrorMessage { get; set; }
        }

        public override void Execute(Arguments arguments, IExecutionContext executionContext)
        {
            string wpath = arguments.Wpath.Value;
            int timeout = arguments.Timeout.Value;
            long start = Environment.TickCount;
            bool found = false;
            while (Math.Abs(Environment.TickCount - start) < timeout &&
                   ShouldStopScript() == false &&
                   found == false)
            {
                found = UiManager.Wait(wpath);

                System.Windows.Forms.Application.DoEvents();
                Thread.Sleep(60);
            }
            SetVariableValue(arguments.Result.Value, new Structures.Bool(found));
            if (!found)
            {
                throw new ControlNotFoundException("Control couldn't be found:");
            }
        }
    }
}
