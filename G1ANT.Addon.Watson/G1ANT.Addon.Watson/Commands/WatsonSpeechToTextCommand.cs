/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Watson
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Language;

namespace G1ANT.Addon.Watson
{
    [Command(Name = "watson.speechtotext", Tooltip = "This command allows to transcript speech from audio file.", NeedsDelay = true)]
    public class WatsonSpeechToTextCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Specifies path to file with speech recorded.")]
            public TextStructure Path { get; set; }

            [Argument(Required = true, Tooltip = "Specifies service's login.")]
            public TextStructure Login { get; set; }

            [Argument(Required = true, Tooltip = "Specifies service's password.")]
            public TextStructure Password { get; set; }

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument(Tooltip = "Floating point value that specifies the minimum score a class must have to be displayed in the results")]
            public FloatStructure Threshold { get; set; } = new FloatStructure(0.5f);

            [Argument(Tooltip = "Specifies language of speech.")]
            public TextStructure Language { get; set; } = new TextStructure("en-US");

            [Argument(DefaultVariable = "timeoutwatson")]
            public override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(30000);
        }
        public WatsonSpeechToTextCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            WatsonSpeechToTextApi watson = new WatsonSpeechToTextApi(arguments.Login.Value, arguments.Password.Value);
            string result = watson.SpeechToText(arguments.Path.Value, arguments.Language.Value, (int)arguments.Timeout.Value.TotalMilliseconds, 1, arguments.Threshold.Value);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new TextStructure(result));
        }
    }
}
