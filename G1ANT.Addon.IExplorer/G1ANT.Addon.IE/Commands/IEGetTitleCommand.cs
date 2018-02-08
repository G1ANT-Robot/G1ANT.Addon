/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.IExplorer
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Language;
using System;

namespace G1ANT.Addon.IExplorer
{
    [Command(Name = "ie.gettitle", Tooltip = "This command allows to get title of currently attached Internet Explorer instance")]
    public class IEGetTitleCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Name of variable where title of Internet Explorer tab will be stored")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

        }
        public IEGetTitleCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {         
            try
            {
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(IEManager.CurrentIE.Ie.Title));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while getting title of currently attached Internet Explorer instance. Message: {ex.Message}", ex);
            }
        }
    }
}
