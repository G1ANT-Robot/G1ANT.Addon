/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Watson
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/

using G1ANT.Addon.Watson.Api;
using G1ANT.Language;

namespace G1ANT.Addon.Watson.Commands
{
    [Command(Name = "watson.speechtotext", Tooltip = "This command allows to transcript speech from audio file.", NeedsDelay = true)]
    public class WatsonSpeechToTextCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Specifies path to file with speech recorded.")]
            public TextStructure Path { get; set; }

            [Argument(Required = true, Tooltip = "Specifies service's login.")]
            public TextStructure ApiKey { get; set; }

            [Argument(Required = true, Tooltip = "Specifies IBM server URI.")]
            public TextStructure ServerUri { get; set; }

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument(Tooltip = "Specifies language of speech.")]
            public TextStructure Language { get; set; } = new TextStructure("en-US");

            [Argument(DefaultVariable = "timeoutwatson")]
            public override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(5000);
        }


        public WatsonSpeechToTextCommand(AbstractScripter scripter) : base(scripter)
        { }

        public void Execute(Arguments arguments)
        {
            var watson = new WatsonSpeechToTextApi(arguments.ApiKey.Value, arguments.ServerUri.Value);
            var result = watson.SpeechToText(arguments.Path.Value, arguments.Language.Value, (int)arguments.Timeout.Value.TotalMilliseconds);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new TextStructure(result));
        }
    }
}
