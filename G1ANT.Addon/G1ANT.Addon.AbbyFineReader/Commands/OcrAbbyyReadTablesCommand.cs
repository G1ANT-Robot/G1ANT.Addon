using G1ANT.Language;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FREngine;
using System.Text.RegularExpressions;

namespace G1ANT.Addon.Ocr.AbbyyFineReader
{
    [Command(Name = "ocrabbyy.readtables", Tooltip = "This command allows to read all tables entries and process it as a list")]
    public class OcrAbbyyReadTablesCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = false, Tooltip = "Id of a processed document returned by a call to processfile command. If not specified last processed document is used.")]
            public IntegerStructure DocumentID { get; set; } = null;

            [Argument]
            public TextStructure Result { get; set; } = new TextStructure("result");
        }
        public OcrAbbyyReadTablesCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            AbbyyManager manager = AbbyyManager.Instance;

            int docID = arguments.DocumentID == null ? manager.CurentDocumentCount : arguments.DocumentID.Value;

            FineReaderDocument document = manager.GetDocument(docID);
            document.ExtractData();

            ListStructure cellsText = new ListStructure(new List<Structure>());
            foreach (FineReaderCell c in document.Cells)
                cellsText.Value.Add(new TextStructure(c.Text));

            Scripter.Variables.SetVariableValue(arguments.Result.Value, cellsText);
        }
    }
}
