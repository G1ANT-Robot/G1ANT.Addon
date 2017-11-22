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
                             arguments.Timeout.Value.Milliseconds,
                             arguments.NoWait.Value);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while setting value '{arguments.Value?.Value ?? string.Empty}' of attribute '{arguments.Name.Value}'. Message: {ex.Message}", ex);
            }
        }
    }
}
