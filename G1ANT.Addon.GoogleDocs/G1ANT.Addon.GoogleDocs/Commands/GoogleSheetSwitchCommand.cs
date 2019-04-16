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
    [Command(Name = "googlesheet.switch", Tooltip = "This command switches between opened Google Sheets instances")]
    public class GoogleSheetSwitchCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Title of Google Sheets instance that will be activated")]
            public IntegerStructure Id { get; set; }
            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            
        }
        public GoogleSheetSwitchCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            int id = arguments.Id.Value;
            bool result = SheetsManager.SwitchSheet(id);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(result));

        }
    }
}
