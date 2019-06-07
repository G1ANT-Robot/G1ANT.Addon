/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Watson
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/

using System;
using G1ANT.Addon.Watson.Api;
using G1ANT.Language;

namespace G1ANT.Addon.Watson.Commands
{
    [Command(Name = "watson.classifyimage", Tooltip = "This command classifies a specified image")]
    public class WatsonClassifyImageCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Path to an image file to be classified")]
            public TextStructure ImagePath { get; set; } = new TextStructure();

            [Argument(Required = true, Tooltip = "API key needed to log in to the service")]
            public TextStructure ApiKey { get; set; }

            [Argument(Required = true, Tooltip = "IBM server URI")]
            public TextStructure ServerUri { get; set; }

            [Argument(Tooltip = "Floating point value (0-1 range) that specifies a minimum score a class must have to be displayed in the results")]
            public FloatStructure Threshold { get; set; } = new FloatStructure(0.5f);

            [Argument(Tooltip = "Name of a variable where the command's result will be stored")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument(DefaultVariable = "timeoutwatson", Tooltip = "Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed")]
            public override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(5000);
        }
        public WatsonClassifyImageCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            try
            {
                var watsonApi = new WatsonClassifyImageApi(arguments.ApiKey.Value, arguments.ServerUri.Value);
                var output = watsonApi.ClassifyImage(arguments.ImagePath.Value, (int)arguments.Timeout.Value.TotalMilliseconds, arguments.Threshold.Value);
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new TextStructure(output));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while classifying image. Additional message: '{ex.Message}'", ex);
            }
        }
    }
}
