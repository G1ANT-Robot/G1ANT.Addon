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
    [Command(Name = "watson.speechtotext", Tooltip = "This command transcripts speech from an audio file", NeedsDelay = true)]
    public class WatsonSpeechToTextCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Path to a file with recorded speech")]
            public TextStructure Path { get; set; }

            [Argument(Required = true, Tooltip = "API key needed to log in to the service")]
            public TextStructure ApiKey { get; set; }

            [Argument(Required = true, Tooltip = "IBM server URI")]
            public TextStructure ServerUri { get; set; }

            [Argument(Tooltip = "Name of a variable where the command's result will be stored")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument(Tooltip = "Language of speech")]
            public TextStructure Language { get; set; } = new TextStructure("en-US");

            [Argument(DefaultVariable = "timeoutwatson", Tooltip = "Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed")]
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
