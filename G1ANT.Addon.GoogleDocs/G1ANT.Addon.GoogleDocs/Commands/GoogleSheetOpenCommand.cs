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
    [Command(Name = "googlesheet.open", Tooltip = "This command opens a new Google Sheets instance")]
    public class GoogleSheetOpenCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument (Required = true, Tooltip = "Google Sheets File Id")]
            public TextStructure Id { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public BooleanStructure IsShared { get; set; } = new BooleanStructure(true);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            
        }
        public GoogleSheetOpenCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {           
            var sheetsManager = SheetsManager.AddSheet();
            sheetsManager.Open(arguments.Id.Value,arguments.IsShared.Value);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new IntegerStructure((sheetsManager.Id)));
        }
    }
}
