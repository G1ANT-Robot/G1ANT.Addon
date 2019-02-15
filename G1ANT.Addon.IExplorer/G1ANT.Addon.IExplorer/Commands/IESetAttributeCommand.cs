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
using System.Linq;
using System;
using System.Collections.Generic;

namespace G1ANT.Addon.IExplorer
{
    [Command(Name = "ie.setattribute", Tooltip = "This command allows to set element's attribute")]

    public class IESetAttributeCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Name of attribute")]
            public TextStructure Name { get; set; }

            [Argument(Tooltip = "Value to set")]
            public TextStructure Value { get; set; }

            [Argument(Required = true, Tooltip = "Phrase to find element by")]
            public TextStructure Search { get; set; }

            [Argument(Tooltip = "Specifies an element selector, possible values are: 'name', 'text', 'title', 'class', 'id', 'selector', 'query', 'jquery'")]
            public TextStructure By { get; set; } = new TextStructure(ElementSearchBy.Id.ToString().ToLower());

            [Argument(Tooltip = "If true, robot will not wait until set attribute action is complete")]
            public BooleanStructure NoWait { get; set; } = new BooleanStructure(false);
            
        }
        public IESetAttributeCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                IEWrapper ie = IEManager.CurrentIE;
                ie.SetAttribute(arguments.Name.Value,
                             arguments.Value?.Value,
                             arguments.Search.Value,
                             arguments.By.Value,
                             (int)arguments.Timeout.Value.TotalMilliseconds,
                             arguments.NoWait.Value);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while setting value '{arguments.Value?.Value ?? string.Empty}' of attribute '{arguments.Name.Value}'. Message: {ex.Message}", ex);
            }
        }
    }
}
