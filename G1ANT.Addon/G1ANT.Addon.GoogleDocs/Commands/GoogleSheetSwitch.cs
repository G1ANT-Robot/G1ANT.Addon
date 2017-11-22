using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;

namespace G1ANT.Language.GoogleDocs.Commands
{
    [Command(Name = "googlesheet.switch", ToolTip= "This command allows to get value from opened Google Sheets instance.")]
    public class GoogleSheetSwitch : CommandBase<GoogleSheetSwitch.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Title of Google Sheets instance that will be activated")]
            public Structures.Integer Id { get; set; }
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
            int id = arguments.Id.Value;
            bool result = SheetsManager.SwitchSheet(id);
            SetVariableValue(arguments.Result.Value, new Language.Structures.Bool(result));

        }
    }
}
