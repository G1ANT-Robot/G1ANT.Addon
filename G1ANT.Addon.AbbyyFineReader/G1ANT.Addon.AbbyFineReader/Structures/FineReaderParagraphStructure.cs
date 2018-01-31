using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FREngine;

namespace G1ANT.Addon.Ocr.AbbyyFineReader
{
    public class FineReaderParagraph
    {
        internal Paragraph paragraph;

        public FineReaderParagraph(Paragraph paragraph)
        {
            this.paragraph = paragraph;
        }

        public string Text
        {
            get
            {
                return paragraph.Text;
            }
        }
    }
}
