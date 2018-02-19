﻿/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Watson
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Language;
using System;

namespace G1ANT.Addon.Watson
{
    [Command(Name = "watson.classifyimage", Tooltip = "This command allows to capture part of the screen and classify the image that was captured. ")]
    public class WatsonClassifyImageCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Specifies capture screen area.")]
            public RectangleStructure Rectangle { get; set; }

            [Argument(Required = true, Tooltip = "Specifies api key needed to login to the service.")]
            public TextStructure ApiKey { get; set; }

            [Argument(Tooltip = "If set to true, rectangle’s position relates to currently focused window")]
            public BooleanStructure Relative { get; set; } = new BooleanStructure(true);

            [Argument(Tooltip = "Floating point value that specifies the minimum score a class must have to be displayed in the results.")]
            public FloatStructure Threshold { get; set; } = new FloatStructure(0.5f);

            [Argument(DefaultVariable = "timeoutwatson")]
            public override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(5000);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public WatsonClassifyImageCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            try
            {
                System.Drawing.Bitmap partOfScreen = RobotWin32.GetPartOfScreen(arguments.Rectangle.Value);
                WatsonClassifyImageApi watsonApi = new WatsonClassifyImageApi(arguments.ApiKey.Value);
                string output = watsonApi.ClassifyImage(partOfScreen, (int)arguments.Timeout.Value.TotalMilliseconds, arguments.Threshold.Value);
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new TextStructure(output));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while classifying image. Additional message: '{ex.Message}'", ex);
            }
        }
    }
}
