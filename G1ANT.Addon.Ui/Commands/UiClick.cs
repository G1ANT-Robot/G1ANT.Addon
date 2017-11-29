using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;

namespace G1ANT.Language.Ui.Commands
{
    [Command(Name = "ui.click", ToolTip = "This command allows you to attach ui field.")]
    public class UiClick : CommandBase<UiClick.Arguments>
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
            Api.UiManager.Click(arguments.Wpath.Value);
            SetVariableValue(arguments.Result.Value, new Structures.Bool(true));
        }
    }
}
