

using System;
/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Ocr
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
namespace G1ANT.Language.Ocr
{
    [Command(Name = "ocrgoogle.login",
        Tooltip = "This command allows to login to the Google text recognition service.",
        IsUnderConstruction = true)]
    public class OcrLoginCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Json credential obtained from Google Text Recognition Service.")]
            public TextStructure JsonCredential { get; set; }
        }

        public OcrLoginCommand(AbstractScripter scripter) : base(scripter) { }
        
        public void Execute(Arguments arguments)
        {
            try
            {
                new GoogleCloudApi(arguments.JsonCredential.Value);
            }
            catch
            {
                throw new Exception("Invalid json credential. Cannot connect to the Google Text Recognition Service");
            }

            OnScriptEnd = () => 
            {
                GoogleCloudApi.Instance.CleanUp();
            };
        }
    }
}
