using G1ANT.Language;

namespace G1ANT.Addon.Ui
{
    [Command(Name = "ui.setvalue", Tooltip = "This command allows you to get value from ui field.")]
    public class UiSetValueCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public TextStructure Wpath { get; set; }

            [Argument(Required = true)]
            public TextStructure Value { get; set; }
        }
        public UiSetValueCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            UiManager.SetValue(arguments.Value.Value, arguments.Wpath.Value);
        }
    }
}
