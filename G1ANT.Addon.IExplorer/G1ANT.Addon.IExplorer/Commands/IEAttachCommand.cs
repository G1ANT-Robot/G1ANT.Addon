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
    [Command(Name = "ie.attach", Tooltip = "This command attaches G1ANT.Robot to an already running Internet Explorer instance and is required for other `ie.` commands to work properly if the `ie.open` command was not used to open IE and attach the robot to the browser")]
    public class IEAttachCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Browser tab title or URL address")]
            public TextStructure Phrase { get; set; }

            [Argument(Tooltip = "Determines where to search for a phrase in a tab to activate it: `title` or `url`")]
            public TextStructure By { get; set; } = new TextStructure("title");

            [Argument(Tooltip = "Name of a variable where the command's result (an attached Internet Explorer instance ID) will be stored")]
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
                int wrapperId = wrapper.Id;
                OnScriptEnd = () =>
                {
                    IEManager.Detach(wrapperId);
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
