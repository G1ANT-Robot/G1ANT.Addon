﻿/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Ocr
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
using System.Linq;

namespace G1ANT.Language.Ocr
{
    [Command(Name = "ocrgoogle.fromscreen", Tooltip = "This command allows to capture part of the screen and recognize text from it. \nIt uses internet connection and external data processing.")]
    public class OcrFromScreenCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public RectangleStructure Area { get; set; }

            [Argument]
            public BooleanStructure Relative { get; set; } = new BooleanStructure(true);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument(DefaultVariable = "timeoutocr")]
            public  override TimeSpanStructure Timeout { get; set; }

            [Argument(Tooltip = "Comma separated language hints for better text recognition")]
            public TextStructure Languages { get; set; } = new TextStructure("en");

        }
        public OcrFromScreenCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            System.Drawing.Rectangle rectangle = !arguments.Relative.Value ? arguments.Area.Value : arguments.Area.Value.ToAbsoluteCoordinates();

            System.Drawing.Bitmap partOfScreen = RobotWin32.GetPartOfScreen(rectangle);
            int timeout = (int)arguments.Timeout.Value.TotalMilliseconds;
            List<string> languages = arguments.Languages.Value.Split(',').ToList();
            GoogleCloudApi googleApi = new GoogleCloudApi();
            string output = googleApi.RecognizeText(partOfScreen, languages, timeout);
            if (Equals(output, new Rectangle(-1, -1, -1, -1)))
                throw new NullReferenceException("Ocr was unable to find text");
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(output));
        }
    }
}
