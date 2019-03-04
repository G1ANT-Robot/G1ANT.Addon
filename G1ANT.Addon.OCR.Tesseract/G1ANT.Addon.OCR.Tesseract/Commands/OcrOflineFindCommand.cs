/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.OCR.Tesseract
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Language;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Tesseract;

namespace G1ANT.Addon.Ocr.Tesseract
{
    [Command(Name = "ocrtesseract.find", Tooltip = "This command allows to find the text on the active screen and return it's position as a 'rectangle' format. \nIf the text will not be found, the result will be Rectangle(-1,-1,-2,-2).  \nPlease be aware that using this command result in unpacking the necessary data to directory My Documents/G1ANT.Robot.")]
    public class OcrOfflineFindCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Text that you want to find in the screen. Provide only single words")]
            public TextStructure Search { get; set; }

            [Argument]
            public RectangleStructure Area { get; set; } = new RectangleStructure(System.Windows.Forms.Screen.PrimaryScreen.Bounds);

            [Argument]
            public BooleanStructure Relative { get; set; } = new BooleanStructure(true);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument(Tooltip = "The language which should be considered trying to recognize text")]
            public TextStructure Language { get; set; } = new TextStructure("eng");
        }
        public OcrOfflineFindCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        double imgRescaleRatio = 4.0;
        public void Execute(Arguments arguments)
        {
            Rectangle rectangle = !arguments.Relative.Value ? arguments.Area.Value : arguments.Area.Value.ToAbsoluteCoordinates();
            Bitmap partOfScreen = RobotWin32.GetPartOfScreen(rectangle);
            int timeout = (int)arguments.Timeout.Value.TotalMilliseconds;
            string language = arguments.Language.Value;

            string search = arguments.Search.Value.ToLower();
            var imgToParse = OcrOfflineHelper.RescaleImage(partOfScreen, imgRescaleRatio);

            var imagePath = OcrOfflineHelper.SaveImageToTemporaryFolder(imgToParse);
            OcrOfflineHelper.UnpackNeededAssemblies();
            var dataPath = OcrOfflineHelper.GetResourcesFolder(language);
            try
            {
                using (var tEngine = new TesseractEngine(dataPath, language, EngineMode.TesseractAndCube))
                {
                    using (var img = Pix.LoadFromFile(imagePath))
                    {
                        using (var page = tEngine.Process(img))
                        {
                            var rectResult = new Rectangle(-1, -1, -1, -1);
                            var wordsWithRectPositions = GetWords(page.GetHOCRText(0));
                            if (wordsWithRectPositions.ContainsValue(search))
                            {
                                rectResult = wordsWithRectPositions.Where(x => x.Value == search).First().Key;
                            }
                            if (Equals(rectResult,new Rectangle(-1,-1,-1,-1)))
                                throw new NullReferenceException("Ocr was unable to find text");
                            Scripter.Variables.SetVariableValue(arguments.Result.Value, new RectangleStructure(rectResult));
                        }
                    }
                }
            }
            catch (TesseractException e)
            {
                throw new ApplicationException("Ocr engine exception, possibly missing language data in folder : " + dataPath);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                File.Delete(imagePath);
            }
        }
        public Dictionary<Rectangle, string> GetWords(string tesseractHtml)
        {
            var xml = XDocument.Parse(tesseractHtml);

            var rectsWords = new Dictionary<Rectangle, string>();
            var Ocr_words = xml.Descendants("span").ToList().Where(element => element.Attribute("class").Value == "ocrx_word").ToList();
            foreach (var Ocr_word in Ocr_words)
            {
                var strs = Ocr_word.Attribute("title").Value.Split(' ');
                strs[4] = strs[4].Replace(";", "");
                int left = (int)(double.Parse(strs[1]) / imgRescaleRatio);
                int top = (int)(double.Parse(strs[2]) / imgRescaleRatio);
                int width = (int)((double.Parse(strs[3]) - double.Parse(strs[1]) + 1) / imgRescaleRatio);
                int height = (int)((double.Parse(strs[4]) - double.Parse(strs[2]) + 1) / imgRescaleRatio);
                var rectange = new Rectangle(left, top, width, height);
                if (!rectsWords.ContainsKey(rectange))
                    rectsWords.Add(new Rectangle(left, top, width, height), Ocr_word.Value.ToLower());
            }

            return rectsWords;
        }

    }
}
