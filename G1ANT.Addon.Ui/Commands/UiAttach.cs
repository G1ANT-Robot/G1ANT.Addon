using G1ANT.Language;

namespace G1ANT.Addon.Ui
{
    [Command(Name = "ui.attach", Tooltip = "This command allows you to attach ui field.")]
    public class UiAttachCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public TextStructure Windowname { get; set; }

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public UiAttachCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            int id = UiManager.Attach(arguments.Windowname.Value);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new IntegerStructure(id));
            if (id == -1)
            {
                throw new WindowNotFoundException($"Cannot attach to window:{arguments.Windowname.Value}");
            }
        }
    }
}
