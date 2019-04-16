/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.GoogleDocs
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Addon.GoogleDocs
{
    [Command(Name = "googlesheet.setvalue", Tooltip = "This command sets a value in an opened Google Sheets instance.")]
    public class GoogleSheetSetValueCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Cell or range address (e.g. `A6`) where new data will be entered")]
            public TextStructure Range { get; set; }

            [Argument(Required = true, Tooltip = "New value to be entered into a specified cell or range")]
            public TextStructure Value { get; set; }

            [Argument(Tooltip = "Name of the sheet which contains the specified cell or range; can be empty or omitted")]
            public TextStructure SheetName { get; set; } = new TextStructure(string.Empty);

            [Argument(Tooltip = "Determines if a new value should be entered as numeric or not")]
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
