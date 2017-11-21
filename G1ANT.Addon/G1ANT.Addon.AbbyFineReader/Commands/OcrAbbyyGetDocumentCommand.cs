﻿using G1ANT.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Addon.Ocr.AbbyyFineReader
{
    [Command(Name = "ocrabbyy.getdocument", Tooltip = "This command gets structured document")]
    public class OcrAbbyyGetDocumentCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Id of a processed document returned by a call to processfile command. If not specified last processed document is used.")]
            public IntegerStructure DocumentID { get; set; } = null;

            [Argument]
            public TextStructure Result { get; set; } = new TextStructure("result");

 
        }
        public OcrAbbyyGetDocumentCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            AbbyyManager manager = AbbyyManager.Instance;
            int docID = arguments.DocumentID == null ? manager.CurentDocumentCount : arguments.DocumentID.Value;
            var doc = manager.GetDocument(docID); 
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new AbbyDocumentStructure(doc.CustomDocument));
        }
    }
}
