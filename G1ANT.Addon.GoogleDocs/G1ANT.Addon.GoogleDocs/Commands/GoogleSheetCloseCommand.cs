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
    [Command(Name = "googlesheet.close", Tooltip = "This command closes Google Sheets instance.")]
    public class GoogleSheetCloseCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Id of spreadsheet that we are closing")]
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
