using G1ANT.Language;
using System;

namespace G1ANT.Addon.Watson
{
    [Command(Name = "watson.login", Tooltip = "It allows to specify api-key that is required by IBM Watson system.")]
    public class WatsonLoginCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Api-key required by IBM Watson system")]
            public TextStructure ApiKey { get; set; }
        }

        public WatsonLoginCommand(AbstractScripter scripter) : base(scripter)
        { }

        public void Execute(Arguments arguments)
        {
            WatsonClassifyImageApi.ApiKey = arguments.ApiKey.Value;            
        }
    }
}
