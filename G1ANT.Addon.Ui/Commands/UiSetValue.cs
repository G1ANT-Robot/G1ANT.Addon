using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;

namespace G1ANT.Language.Ui.Commands
{
    [Command(Name = "ui.setvalue", ToolTip = "This command allows you to get value from ui field.")]
    public class UiSetValue : CommandBase<UiSetValue.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public Structures.String Wpath { get; set; }

            [Argument(Required = true)]
            public Structures.String Value { get; set; }

            [Argument]
            public Structures.Bool If { get; set; } = new Structures.Bool(true);

            [Argument]
            public Structures.String ErrorJump { get; set; }

            [Argument]
            public Structures.String ErrorMessage { get; set; }
        }

        public override void Execute(Arguments arguments, IExecutionContext executionContext)
        {
            Api.UiManager.SetValue(arguments.Value.Value, arguments.Wpath.Value);
        }
    }
}
