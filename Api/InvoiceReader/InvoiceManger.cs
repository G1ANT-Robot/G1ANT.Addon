using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Language.Ocr
{
    public static class InvoiceManger
    {
        //public InvoiceManger(string path, string type)
        //{
        //    ReadInvoice(path, type);
        //}
        public static string ReadText { get; private set; }
        public static PDFReader pdf;
        public static  ImageReader image;      
		
        public static void ReadInvoice (string path, string type)
        {
            if (type.Equals("Ocr"))
            {
                image = new ImageReader(path);
                ReadText = image.ReadText;
            } else
            {
				pdf = new PDFReader(path);
				ReadText = pdf.ReadText;
			}
        }

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
    }
}
