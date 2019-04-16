/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Ocr
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace G1ANT.Language.Ocr.Google
{
    [Command(Name = "ocrgoogle.findpoint", Tooltip = "This command finds a specified text on the active screen and returns its position in a [point](G1ANT.Robot/G1ANT.Language/G1ANT.Language/Structures/PointStructure.md) format")]
    public class OcrGoogleFindPointCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true,Tooltip = "Text to be found on the screen (the fewer words, the better results)")]
            public TextStructure Search { get; set; }

            [Argument]
            public RectangleStructure Area { get; set; } = new RectangleStructure(System.Windows.Forms.Screen.PrimaryScreen.Bounds);

            [Argument]
            public BooleanStructure Relative { get; set; } = new BooleanStructure(true);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument(DefaultVariable = "timeoutocr", Tooltip = "Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed")]
            public  override TimeSpanStructure Timeout { get; set; }

            [Argument(Tooltip = "List of languages you want to use to recognize text on the screen")]
            public TextStructure Languages { get; set; } = new TextStructure("en");
        }

        public OcrGoogleFindPointCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            Rectangle rectangle = arguments.Relative.Value ? arguments.Area.Value.ToAbsoluteCoordinates() : arguments.Area.Value;
            Bitmap partOfScreen = RobotWin32.GetPartOfScreen(rectangle);
            int timeout = (int)arguments.Timeout.Value.TotalMilliseconds;
            List<string> languages = arguments.Languages.Value.Split(',').ToList();
            string search = arguments.Search.Value;

            GoogleCloudApi googleApi = new GoogleCloudApi();
            Rectangle output = googleApi.RecognizeText(partOfScreen, search, languages, timeout);
            Point pointOutput = new Point(output.X + arguments.Area.Value.X, output.Y + arguments.Area.Value.Y);
            if (Equals(output, new Rectangle(-1, -1, -1, -1)))
                throw new NullReferenceException("Ocr was unable to find text");
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new PointStructure(pointOutput));
        }
    }
}
