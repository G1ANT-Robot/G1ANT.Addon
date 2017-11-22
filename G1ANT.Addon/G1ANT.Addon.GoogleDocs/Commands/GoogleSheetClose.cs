using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;

namespace G1ANT.Language.GoogleDocs.Commands
{
    [Command(Name = "googlesheet.close", ToolTip = "This command closes Google Sheets instance.")]
    public class GoogleSheetClose : CommandBase<GoogleSheetClose.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Id of spreadsheet that we are closing")]
            public Structures.Integer Id { get; set; }

            [Argument]
            public Structures.Bool If { get; set; } = new Structures.Bool(true);

            [Argument]
            public Structures.String ErrorJump { get; set; }

            [Argument]
            public Structures.String ErrorMessage { get; set; }
        }

        public override void Execute(Arguments arguments, IExecutionContext executionContext)
        {
            var id = arguments.Id != null ? arguments.Id.Value : SheetsManager.CurrentSheet.Id;
            SheetsManager.Remove(id);
        }
    }
}
