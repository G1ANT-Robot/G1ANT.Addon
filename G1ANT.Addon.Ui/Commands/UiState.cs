using G1ANT.Language;

namespace G1ANT.Addon.Ui
{
    [Command(Name = "ui.state", Tooltip = "This command allows you to get state of checkbox.")]
    public class UiStateCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public TextStructure Wpath { get; set; }

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public UiStateCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            bool state = UiManager.State(arguments.Wpath.Value);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(state));
        }
    }
}
