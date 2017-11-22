using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;

namespace G1ANT.Language.GoogleDocs.Commands
{
    [Command(Name = "googlesheet.settitle", ToolTip = "This command allows to set title of opened Google Sheets instance.")]
    public class GoogleSheetSetTitle : CommandBase<GoogleSheetSetTitle.Arguments>
    {
        public new class Arguments : CommandArguments
        {

            [Argument(Required = true, Tooltip = "New spreadsheet title")]
            public Structures.String Title { get; set; } 

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
            sheetsManager.SetNewTitle(arguments.Title.Value);
            
        }
    }
}
