using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Language.GoogleDocs.Commands
{
    [Command(Name = "googlesheet.setvalue", ToolTip = "This command allows to set value in opened Google Sheets instance.")]
    public class GoogleSheetSetValue : CommandBase<GoogleSheetSetValue.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Cell name (like A6) where you want to inject data")]
            public Structures.String Range { get; set; }

            [Argument(Required = true, Tooltip = "New value to be inserted inside of a chosen cell")]
            public Structures.String Value { get; set; }

            [Argument(Tooltip = "SheetName where range exists, can be empty or omitted")]
            public Structures.String SheetName { get; set; } = new Structures.String(string.Empty);

            [Argument(Tooltip = "Determines if new value should be inserted as numeric or not")]
            public Structures.Bool Numeric { get; set; } = new Structures.Bool(true);

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
            var sheetName = arguments.SheetName.Value == "" ? sheetsManager.sheets[0].Properties.Title : arguments.SheetName.Value;
            sheetsManager.SetValue(arguments.Range.Value, arguments.Value.Value, sheetName,arguments.Numeric.Value);

        }
    }
}
