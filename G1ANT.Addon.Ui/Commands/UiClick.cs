using G1ANT.Language;

namespace G1ANT.Addon.Ui
{
    [Command(Name = "ui.click", Tooltip = "This command allows you to attach ui field.")]
    public class UiClickCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public TextStructure Wpath { get; set; }

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public UiClickCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            UiManager.Click(arguments.Wpath.Value);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(true));
        }
    }
}
