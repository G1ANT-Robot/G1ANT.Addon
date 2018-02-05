
using G1ANT.Language;

namespace G1ANT.Addon.GoogleDocs
{
    [Command(Name = "googlesheet.switch", Tooltip= "This command allows to get value from opened Google Sheets instance.")]
    public class GoogleSheetSwitchCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Title of Google Sheets instance that will be activated")]
            public IntegerStructure Id { get; set; }
            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            
        }
        public GoogleSheetSwitchCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            int id = arguments.Id.Value;
            bool result = SheetsManager.SwitchSheet(id);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(result));

        }
    }
}
