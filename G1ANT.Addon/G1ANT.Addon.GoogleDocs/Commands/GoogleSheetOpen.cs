using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;
using G1ANT.Language.GoogleDocs;

namespace G1ANT.Language.GoogleDocs.Commands
{
    [Command(Name = "googlesheet.open", ToolTip= "This command allows to open a new Google Sheets instance.")]
    public class GoogleSheetOpen : CommandBase<GoogleSheetOpen.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument (Required = true, Tooltip = "Google Sheets File Id")]
            public Structures.String Id { get; set; } = new Structures.String(string.Empty);

            [Argument]
            public Structures.Bool IsShared { get; set; } = new Structures.Bool(true);

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
            var sheetsManager = SheetsManager.AddSheet();
            sheetsManager.Open(arguments.Id.Value,arguments.IsShared.Value);
            SetVariableValue(arguments.Result.Value, new Language.Structures.Integer((sheetsManager.Id)));
        }
    }
}
