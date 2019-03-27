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
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tesseract;

namespace G1ANT.Addon.Ocr.Tesseract
{
    [Command(Name = "ocrtesseract.fromscreen", Tooltip = "This command allows to capture part of the screen and recognize text from it. \nThis command may often be less accurate than 'ocr' command. \nPlease be aware that command will unpack some necessary data to folder My Documents/G1ANT.Robot.")]
    public class OcrOfflineFromScreenCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public RectangleStructure Area { get; set; } = new RectangleStructure(SystemInformation.VirtualScreen);

            [Argument]
            public BooleanStructure Relative { get; set; } = new BooleanStructure(true);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument(Tooltip = "The language which should be considered trying to recognize text")]
            public TextStructure Language { get; set; } = new TextStructure("eng");

            [Argument(Tooltip = "The ratio used for rescaling image before OCR. Values above 1.0")]
            public FloatStructure Sensitivity { get; set; } = new FloatStructure(2.0);
        }
        public OcrOfflineFromScreenCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            var rectangle = arguments.Area.Value;
            if (arguments.Relative.Value)
            {
                RobotWin32.Rect foregroundWindowRect = new RobotWin32.Rect();
                RobotWin32.GetWindowRectangle(RobotWin32.GetForegroundWindow(), ref foregroundWindowRect);
                rectangle = new Rectangle(rectangle.X + foregroundWindowRect.Left,
                    rectangle.Y + foregroundWindowRect.Top,
                    foregroundWindowRect.Right - foregroundWindowRect.Left,
                    foregroundWindowRect.Bottom - foregroundWindowRect.Top);
            }
            var partOfScreen = RobotWin32.GetPartOfScreen(rectangle);
            var imgToParse = OcrOfflineHelper.RescaleImage(partOfScreen, arguments.Sensitivity.Value);
            var language = arguments.Language.Value;
            var dataPath = OcrOfflineHelper.GetResourcesFolder(language);

            try
            {
                using (var tEngine = new TesseractEngine(dataPath, language, EngineMode.TesseractAndCube))
                using (var img = PixConverter.ToPix(imgToParse))
                using (var page = tEngine.Process(img))
                {
                    var text = page.GetText();
                    if (string.IsNullOrEmpty(text))
                        throw new NullReferenceException("Ocr was unable to find any text");
                    Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(text));
                }
            }
            catch (TesseractException)
            {
                throw new ApplicationException("Ocr engine exception, possibly missing language data in folder: " + dataPath);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

