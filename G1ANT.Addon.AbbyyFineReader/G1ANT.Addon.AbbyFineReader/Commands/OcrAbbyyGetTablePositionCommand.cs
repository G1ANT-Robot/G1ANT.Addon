using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FREngine;
using System.Text.RegularExpressions;
using G1ANT.Language;

namespace G1ANT.Addon.Ocr.AbbyyFineReader
{
    [Command(Name = "ocrabbyy.gettableposition", Tooltip = "This command allows to find indexes.")]
    public class OcrAbbyyGetTablePositionCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Text that you want to find in the screen. ")]
            public TextStructure Search { get; set; }

            [Argument(Required = false, Tooltip = "Id of a processed document returned by a call to processfile command. If not specified last processed document is used.")]
            public IntegerStructure DocumentID { get; set; } = null;

            [Argument(Required = true, Tooltip = "Index of a table in document")]
            public IntegerStructure TableIndex { get; set; } = null;

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument(DefaultVariable = "timeoutOcr")]
            public  override TimeSpanStructure Timeout { get; set; }
        }
        public OcrAbbyyGetTablePositionCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        private AbbyyManager manager = null;

        public void Execute(Arguments arguments)
        {
            manager = AbbyyManager.Instance;
            int docID = arguments.DocumentID == null ? manager.CurentDocumentCount : arguments.DocumentID.Value;
            var doc = manager.GetDocument(docID);
            List<string> table = doc.Tables[arguments.TableIndex.Value].ReturnWordPosition(arguments.Search.Value);
            ListStructure cells = new ListStructure(new List<Structure>());
            foreach (String t in table)
                cells.Value.Add(new TextStructure(t));
            Scripter.Variables.SetVariableValue(arguments.Result.Value, cells);
        }
    }
}
