using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Language.GoogleDocs.Commands
{

    [Command(Name = "googlesheet.download", ToolTip = "This command allows to download whole spreadsheet.")]
    public class GoogleSheetDownload : CommandBase<GoogleSheetDownload.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument (Required =true)]
            public Structures.String Path { get; set; }
            [Argument]
            public Structures.String Type { get; set; }
            [Argument]
            public Structures.String Result { get; set; } = new Structures.String("result");

            [Argument]
            public Structures.Bool If { get; set; } = new Structures.Bool(true);

            [Argument]
            public Structures.String ErrorJump { get; set; }

            [Argument]
            public Structures.String ErrorMessage { get; set; }
        }

        public override void Execute(Arguments arguments, IExecutionContext executionContext)
        {
            var sheetsManager = SheetsManager.CurrentSheet;
            string res = sheetsManager.DownloadFile(arguments.Path.Value, arguments.Type.Value);
            SetVariableValue(arguments.Result.Value, new Language.Structures.String(res));
        }
    }
}
