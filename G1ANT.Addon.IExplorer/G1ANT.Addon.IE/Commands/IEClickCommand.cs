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
    [Command(Name = "ie.click", Tooltip = "This command allows to send click event to element of an active webpage.")]

    public class IEClickCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Phrase to find element by")]
            public TextStructure Search { get; set; }

            [Argument(Tooltip = "Specifies an element selector, possible values are: 'id', 'name', 'text', 'title', 'class', 'selector', 'query', 'jquery'")]
            public TextStructure By { get; set; } = new TextStructure(ElementSearchBy.Id.ToString().ToLower());

            [Argument(Tooltip = "If 'true', script will continue without waiting for webpage to respond to click event")]
            public BooleanStructure NoWait { get; set; } = new BooleanStructure (false);
        }
        public IEClickCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                IEWrapper ie = IEManager.CurrentIE;
                ie.ClickElement(arguments.Search.Value, arguments.By.Value,  (int)arguments.Timeout.Value.TotalMilliseconds, arguments.NoWait.Value);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while clicking element. Message: {ex.Message}", ex);
            }            
        }
    }
}
