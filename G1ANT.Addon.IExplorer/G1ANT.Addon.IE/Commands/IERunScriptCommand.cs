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
    [Command(Name = "ie.runscript", Tooltip = "This command executes javascript on currently attached Internet Explorer instance")]
    public class IERunScriptCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Script to be executed")]
            public TextStructure Script { get; set; }

            [Argument(Tooltip = "Name of variable where result of javascript execution will be stored")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

        }
        public IERunScriptCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                IEWrapper ie = IEManager.CurrentIE;
                string result = ie.InsertJavaScriptAndTakeResult(arguments.Script.Value);
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new TextStructure(result));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while trying to run javascript code. Message: {ex.Message}", ex);
            }
        }
    }
}
