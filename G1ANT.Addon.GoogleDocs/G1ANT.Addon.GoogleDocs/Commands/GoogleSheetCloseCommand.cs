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
    [Command(Name = "googlesheet.close", Tooltip = "This command closes a Google Sheets instance.")]
    public class GoogleSheetCloseCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "ID of a spreadsheet to be closed. The ID can be stored in a variable when the [`googlesheet.open`](GoogleSheetOpenCommand.md) command is used. If no ID is specified, a recently used Google Sheets instance is closed")]
            public IntegerStructure Id { get; set; }
        }
        public GoogleSheetCloseCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            var id = arguments.Id != null ? arguments.Id.Value : SheetsManager.CurrentSheet.Id;
            SheetsManager.Remove(id);
        }
    }
}
