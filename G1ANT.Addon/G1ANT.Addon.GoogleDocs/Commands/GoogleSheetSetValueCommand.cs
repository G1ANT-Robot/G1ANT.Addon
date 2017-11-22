using G1ANT.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Addon.GoogleDocs
{
    [Command(Name = "googlesheet.setvalue", Tooltip = "This command allows to set value in opened Google Sheets instance.")]
    public class GoogleSheetSetValueCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Cell name (like A6) where you want to inject data")]
            public TextStructure Range { get; set; }

            [Argument(Required = true, Tooltip = "New value to be inserted inside of a chosen cell")]
            public TextStructure Value { get; set; }

            [Argument(Tooltip = "SheetName where range exists, can be empty or omitted")]
            public TextStructure SheetName { get; set; } = new TextStructure(string.Empty);

            [Argument(Tooltip = "Determines if new value should be inserted as numeric or not")]
            public BooleanStructure Numeric { get; set; } = new BooleanStructure(true);

            
        }
        public GoogleSheetSetValueCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            var sheetsManager = SheetsManager.CurrentSheet;
            var sheetName = arguments.SheetName.Value == "" ? sheetsManager.sheets[0].Properties.Title : arguments.SheetName.Value;
            sheetsManager.SetValue(arguments.Range.Value, arguments.Value.Value, sheetName,arguments.Numeric.Value);

        }
    }
}
