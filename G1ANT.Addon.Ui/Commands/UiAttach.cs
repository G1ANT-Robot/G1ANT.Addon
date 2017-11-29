using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;
using G1ANT.Language.Ui.Exceptions;

namespace G1ANT.Language.Ui.Commands
{
    [Command(Name = "ui.attach", ToolTip = "This command allows you to attach ui field.")]
    public class UiAttach : CommandBase<UiAttach.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public Structures.String Windowname { get; set; }

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
            int id = Api.UiManager.Attach(arguments.Windowname.Value);
            SetVariableValue(arguments.Result.Value, new Structures.Integer(id));
            if (id == -1)
            {
                throw new WindowNotFoundException($"Cannot attach to window:{arguments.Windowname.Value}");
            }
        }
    }
}
