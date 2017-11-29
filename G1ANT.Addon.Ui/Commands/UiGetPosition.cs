using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;
using G1ANT.Language.Ui.Exceptions;

namespace G1ANT.Language.Ui.Commands
{
    [Command(Name = "ui.getposition", ToolTip = "This command allows you to attach ui field.")]
    public class UiGetPosition : CommandBase<UiGetPosition.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public Structures.String Wpath { get; set; }

            [Argument]
            public Structures.Bool Relative { get; set; } = new Structures.Bool(true);

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
            try
            {
                System.Drawing.Point point = System.Drawing.Point.Empty;
                if (arguments.Relative.Value)
                    point = Api.UiManager.GetRelativePosition(arguments.Wpath.Value);
                else
                    point = Api.UiManager.GetGlobalPosition(arguments.Wpath.Value);

                SetVariableValue(arguments.Result.Value, new Structures.Point(point));
            }
            catch
            {
                throw new ControlNotFoundException($"Cannot find control:{arguments.Wpath.Value}");
            }
        }
    }
}
