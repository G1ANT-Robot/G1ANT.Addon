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
    [Command(Name = "googlesheet.gettitle", Tooltip = "This command gets the title of an opened Google Sheets instance")]
    public class GoogleSheetGetTitleCommand : Command
    {
        public class Arguments : CommandArguments
        {
            
            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            
        }
        public GoogleSheetGetTitleCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            var sheetsManager = SheetsManager.CurrentSheet;
            var title = sheetsManager.spreadSheetName;
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(title));
        }
    }
}
