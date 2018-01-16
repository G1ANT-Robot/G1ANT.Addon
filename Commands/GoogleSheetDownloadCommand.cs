
using G1ANT.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Addon.GoogleDocs
{

    [Command(Name = "googlesheet.download", Tooltip = "This command allows to download whole spreadsheet.")]
    public class GoogleSheetDownloadCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument (Required =true, Tooltip = "Destination on your computer where the file will be saved")]
            public TextStructure Path { get; set; }
            [Argument (Tooltip = "Type of file extension, could be ‘pdf’ or ‘xlsx’")]
            public TextStructure Type { get; set; }
            [Argument]
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
