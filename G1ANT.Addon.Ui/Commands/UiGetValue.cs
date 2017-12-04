using G1ANT.Language;

namespace G1ANT.Addon.Ui
{
    [Command(Name = "ui.getvalue", Tooltip = "This command allows you to get value from ui field.")]
    public class UiGetValueCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public TextStructure Wpath { get; set; }

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public UiGetValueCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new TextStructure(UiManager.GetValue(arguments.Wpath.Value)));
        }
    }
}
