using System.Collections.Generic;
using System.Drawing;

namespace G1ANT.Language.Ocr
{
    public class ImageReader
    {
        public ImageReader()
        { }

        public ImageReader (string path)
        {
            ReadText = GetTextFromImage(path);

        }

        public string ReadText { get;}

        public static string GetTextFromImage(string path)
        {
            Bitmap bitmap = new Bitmap(path);
            GoogleCloudApi googleApi = new GoogleCloudApi();
            string output = googleApi.RecognizeText(bitmap, new List<string>() { "pl", "en" }, 10000);

            return output;
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
