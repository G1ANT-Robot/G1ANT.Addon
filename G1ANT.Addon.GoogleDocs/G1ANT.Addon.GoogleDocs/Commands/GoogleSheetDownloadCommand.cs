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

    [Command(Name = "googlesheet.download", Tooltip = "This command downloads the whole spreadsheet")]
    public class GoogleSheetDownloadCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument (Required =true, Tooltip = "Destination on your computer where the file will be saved")]
            public TextStructure Path { get; set; }
            [Argument (Tooltip = "Type of file extension, could be ‘pdf’ or ‘xlsx’")]
            public TextStructure Type { get; set; }
            [Argument(Tooltip = "Name of a variable where the command's result will be stored")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public GoogleSheetDownloadCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            var sheetsManager = SheetsManager.CurrentSheet;
            string res = sheetsManager.DownloadFile(arguments.Path.Value, arguments.Type.Value);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(res));
        }
    }
}
