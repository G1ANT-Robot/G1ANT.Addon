/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Ocr
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/

namespace G1ANT.Language.Ocr.Google
{
    [Command(Name = "ocrgoogle.login",
        Tooltip = "This command allows to login to the Google Cloud Service.")]

    public class OcrGoogleLoginCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Json credential obtained from Google Cloud Service.")]
            public TextStructure JsonCredential { get; set; }
        }

        public OcrGoogleLoginCommand(AbstractScripter scripter) : base(scripter) { }
        
        public void Execute(Arguments arguments)
        {
            GoogleCloudApi.JsonCredential = arguments.JsonCredential.Value;

            OnScriptEnd = () =>
            {
                try
                {
                    GoogleCloudApi.JsonCredential = null;
                }
                catch
                {

                }
            };
        }
    }
}
