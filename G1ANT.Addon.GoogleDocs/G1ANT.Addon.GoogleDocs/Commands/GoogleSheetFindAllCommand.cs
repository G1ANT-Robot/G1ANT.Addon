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

    [Command(Name = "googlesheet.findall", Tooltip = "This command allows to find all cells in which specified value is.")]
    public class GoogleSheetFindAllCommand : Command
    {

        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Value that we are looking for")]
            public TextStructure Value { get; set; }

            [Argument(Tooltip = "SheetName where range exists, can be empty or omitted")]
            public TextStructure SheetName { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            
        }
        public GoogleSheetFindAllCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            var sheetsManager = SheetsManager.CurrentSheet;
            var sheetName = arguments.SheetName.Value == "" ? sheetsManager.sheets[0].Properties.Title : arguments.SheetName.Value;


            var result = sheetsManager.FindAll(arguments.Value.Value, sheetName);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(result));
        }

    }
}
