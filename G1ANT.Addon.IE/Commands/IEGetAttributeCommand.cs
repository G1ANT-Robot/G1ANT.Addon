using G1ANT.Language;
using System.Linq;
using System;
using System.Collections.Generic;

namespace G1ANT.Addon.IExplorer
{
    [Command(Name = "ie.getattribute", Tooltip = "This command allows to get element's attribute.")]

    public class IEGetAttributeCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Name of attribute")]
            public TextStructure Name { get; set; }

            [Argument(Required = true, Tooltip = "Phrase to find element by")]
            public TextStructure Search { get; set; }

            [Argument(Tooltip = "Specifies an element selector, possible values are: 'name', 'text', 'title', 'class', 'id', 'selector', 'query', 'jquery'")]
            public TextStructure By { get; set; } = new TextStructure(ElementSearchBy.Id.ToString().ToLower());

            [Argument(DefaultVariable = "timeoutie")]
            public  override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(IeSettings.IeTimeout);

            [Argument(Tooltip = "Name of variable where title of Internet Explorer tab will be stored")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument(Tooltip = "If true, robot will not wait until set attribute action is complete")]
            public BooleanStructure NoWait { get; set; } = new BooleanStructure(false);
        }
        public IEGetAttributeCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                IEWrapper ie = IEManager.CurrentIE;
                string value = ie.GetAttribute(arguments.Name.Value,
                                               arguments.Search.Value,
                                               arguments.By.Value,
                                               arguments.Timeout.Value.Milliseconds,
                                               arguments.NoWait.Value);
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new TextStructure(value));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while getting attribute '{arguments.Name.Value}' value. Message: {ex.Message}", ex);
            }
        }
    }
}
