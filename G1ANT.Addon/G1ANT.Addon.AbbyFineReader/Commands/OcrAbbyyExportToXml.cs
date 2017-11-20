using G1ANT.Interop;
using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;
using GStructures = G1ANT.Language.Structures;
using G1ANT.Sdk.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FREngine;
using System.Text.RegularExpressions;
using G1ANT.Language.Ocr.AbbyyFineReader.Structures;

namespace G1ANT.Language.Ocr.AbbyyFineReader.Commands
{
    [Command(Name = "ocrabbyy.exportxml")]
    public class OcrAbbyyExportToXml : CommandBase<OcrAbbyyExportToXml.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Path to a file to be processed")]
            public GStructures.String Path { get; set; }

            [Argument]
            public GStructures.Bool If { get; set; } = new GStructures.Bool(true);

            [Argument]
            public GStructures.String ErrorJump { get; set; }

            [Argument]
            public GStructures.String ErrorMessage { get; set; }
        }

        public override void Execute(Arguments arguments, IExecutionContext executionContext)
        {
            AbbyyManager manager = AbbyyManager.Instance;
            FineReaderDocument document = manager.GetDocument(manager.CurentDocumentCount);
            document.ExportToXml(arguments.Path.Value);
        }
    }
}
