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
    [Command(Name = "ie.attach", Tooltip = "This command allows to attach G1ANT.Robot to running Internet Explorer instance. It activates tab with specified phrase.")]
    public class IEAttachCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Browser tab title or url")]
            public TextStructure Phrase { get; set; }

            [Argument(Tooltip = "'title' or 'url', determines what to look for in a tab to activate")]
            public TextStructure By { get; set; } = new TextStructure("title");

            [Argument(Tooltip = "Name of variable where attached Internet Explorer instance's id is going to be placed")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }

        public IEAttachCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                var wrapper = IEManager.AttachToExistingIE(arguments.Phrase.Value, arguments.By.Value);
                OnScriptEnd = () =>
                {
                    IEManager.Detach(wrapper);
                };
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new IntegerStructure(wrapper.Id));
                wrapper.ActivateTab(arguments.By.Value, arguments.Phrase.Value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Failed while trying to attach to existing Internet Explorer instance. Please, make sure Internet Explorer instance is running and that the specified searching phrase is correct. Additional message: {ex.Message}");
            }
        }
    }
}
