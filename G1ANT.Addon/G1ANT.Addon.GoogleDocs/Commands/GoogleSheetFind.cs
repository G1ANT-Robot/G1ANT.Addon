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

    [Command(Name = "googlesheet.find", ToolTip = "This command allows to find in which cell (i.e A4) is specified value.")]
    public class GoogleSheetFind : CommandBase<GoogleSheetFind.Arguments>
    {

        public new class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Value that we are looking for")]
            public Structures.String Value { get; set; }

            [Argument(Tooltip = "SheetName where range exists, can be empty or omitted")]
            public Structures.String SheetName { get; set; } = new Structures.String(string.Empty);

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
            var sheetName = arguments.SheetName.Value == "" ? sheetsManager.sheets[0].Properties.Title : arguments.SheetName.Value;
           

            var result = sheetsManager.FindFirst(arguments.Value.Value, sheetName);
            SetVariableValue(arguments.Result.Value, new Language.Structures.String(result));
        }

    }
}
