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
    [Command(Name = "watson.classifyimage", Tooltip = "This command allows to capture part of the screen and classify the image that was captured. ")]
    public class WatsonClassifyImageCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Specifies capture screen area.")]
            public TextStructure ImagePath { get; set; } = new TextStructure();

            [Argument(Required = true, Tooltip = "Specifies api key needed to login to the service.")]
            public TextStructure ApiKey { get; set; }

            [Argument(Required = true, Tooltip = "Specifies IBM server URI.")]
            public TextStructure ServerUri { get; set; }

            [Argument(Tooltip = "Floating point value that specifies the minimum score a class must have to be displayed in the results.")]
            public FloatStructure Threshold { get; set; } = new FloatStructure(0.5f);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
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
