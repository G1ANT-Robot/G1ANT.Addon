using G1ANT.Language;

namespace G1ANT.Addon.Ui
{
    [Command(Name = "ui.switchui", Tooltip = "This command allows you to get value from ui field.")]
    public class UiSwitchUiCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public IntegerStructure Id { get; set; } = new IntegerStructure(1);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public UiSwitchUiCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(UiManager.SwitchUi(arguments.Id.Value)));
        }
    }
}
