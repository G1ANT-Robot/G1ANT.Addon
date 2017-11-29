using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;

namespace G1ANT.Language.Ui.Commands
{
    [Command(Name = "ui.state", ToolTip = "This command allows you to get state of checkbox.")]
    public class UiState : CommandBase<UiState.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public Structures.String Wpath { get; set; }

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
            bool state = Api.UiManager.State(arguments.Wpath.Value);
            SetVariableValue(arguments.Result.Value, new Structures.Bool(state));
        }
    }
}
