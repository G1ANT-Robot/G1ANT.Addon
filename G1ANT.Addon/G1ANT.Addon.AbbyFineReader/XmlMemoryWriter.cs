using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace G1ANT.Language.Ocr.AbbyyFineReader
{
    public class XmlMemoryWriter : FREngine.IFileWriter
    {
        private System.IO.MemoryStream stream;
        private XmlDocument document = new XmlDocument();

        public XmlMemoryWriter()
        {

        }

        public void Open(string fileName, ref int bufferSize)
        {
            stream = new System.IO.MemoryStream();
        }

        public void Write(byte[] data)
        {
            stream.Write(data, 0, data.Length);
        }

        public void Close()
        {
            stream.Position = 0;
            document.Load(stream);
            stream.Close();
            stream.Dispose();
        }

        public XmlDocument GetDocument()
        {
            return document;
        }
    }
}
