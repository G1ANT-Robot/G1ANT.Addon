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

namespace G1ANT.Addon.GoogleDocs
{
    [Command(Name = "googlesheet.getvalue", Tooltip = "This command gets a value from a specified cell or range in an opened Google Sheets instance")]
    public class GoogleSheetGetValueCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Cell or range to get a value from, for example `A6`, `A3&B7` or `A3:A6&B7`")]
            public TextStructure Range { get; set; }

            [Argument(Tooltip = "Sheet name containing the specified range; can be empty or omitted")]
            public TextStructure SheetName { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            
        }
        public GoogleSheetGetValueCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            var sheetsManager = SheetsManager.CurrentSheet;
            var sheetName = arguments.SheetName.Value == "" ? sheetsManager.sheets[0].Properties.Title : arguments.SheetName.Value;
            var val = sheetsManager.GetValue(arguments.Range.Value, sheetName);
            ListStructure result = new ListStructure(new System.Collections.Generic.List<object>());
            //string result=null;
            //var result = val. == null ? "" : val.Values[0][0].ToString();
            if (val.ValueRanges[0].Values ==null)
            {
                for (int i = 0; i < val.ValueRanges.Count; i++)
                {
                    TextStructure tmp = new TextStructure("");
                    result.Value.Add(tmp);
                }
                Scripter.Variables.SetVariableValue(arguments.Result.Value, result);
            }
            else
            { 
                for (int i = 0; i < val.ValueRanges.Count; i++)
                {
                   // TextStructure tmp = new TextStructure(val.ValueRanges[i].Values[0][0].ToString());
                    result.Value.Add(val.ValueRanges[i].Values[0][0].ToString());
                }
                Scripter.Variables.SetVariableValue(arguments.Result.Value, result);
            }
        }
    }
}
