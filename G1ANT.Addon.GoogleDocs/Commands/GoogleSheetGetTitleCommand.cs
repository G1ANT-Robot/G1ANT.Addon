
using G1ANT.Language;

namespace G1ANT.Addon.GoogleDocs
{
    [Command(Name = "googlesheet.gettitle", Tooltip= "This command allows to get title of opened Google Sheets instance.")]
    public class GoogleSheetGetTitleCommand : Command
    {
        public class Arguments : CommandArguments
        {
            
            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            
        }
        public GoogleSheetGetTitleCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            var sheetsManager = SheetsManager.CurrentSheet;
            var title = sheetsManager.spreadSheetName;
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(title));
        }
    }
}
