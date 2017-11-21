





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
    [Command(Name = "ocrabbyy.exportxml")]
    public class OcrAbbyyExportToXmlCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Path to a file to be processed")]
            public TextStructure Path { get; set; }
            
        }
        public OcrAbbyyExportToXmlCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            AbbyyManager manager = AbbyyManager.Instance;
            FineReaderDocument document = manager.GetDocument(manager.CurentDocumentCount);
            document.ExportToXml(arguments.Path.Value);
        }
    }
}
