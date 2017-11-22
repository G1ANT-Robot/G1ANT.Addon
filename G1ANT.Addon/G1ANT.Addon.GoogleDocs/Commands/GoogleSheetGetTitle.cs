using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;

namespace G1ANT.Language.GoogleDocs.Commands
{
    [Command(Name = "googlesheet.gettitle", ToolTip= "This command allows to get title of opened Google Sheets instance.")]
    public class GoogleSheetGetTitle : CommandBase<GoogleSheetGetTitle.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            
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
            var sheetsManager = SheetsManager.CurrentSheet;
            var title = sheetsManager.spreadSheetName;
            SetVariableValue(arguments.Result.Value, new Language.Structures.String(title));
        }
    }
}
