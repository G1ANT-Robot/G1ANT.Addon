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
    [Command(Name = "ie.fireevent", Tooltip = "This command allows to fire event on specified element")]

    public class IEFireEventCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Name of event to fire")]
            public TextStructure EventName { get; set; }

            [Argument(Tooltip = "Parameters to be passed to the event handler")]
            public ListStructure Parameters { get; set; }

            [Argument(Required = true, Tooltip = "Phrase to find element by")]
            public TextStructure Search { get; set; }

            [Argument(Tooltip = "Specifies an element selector, possible values are: 'id', 'name', 'text', 'title', 'class', 'selector', 'query', 'jquery'")]
            public TextStructure By { get; set; } = new TextStructure(ElementSearchBy.Id.ToString().ToLower());

            [Argument(Tooltip = "If true, script will continue without waiting for event to complete")]
            public BooleanStructure NoWait { get; set; } = new BooleanStructure(false);

            [Argument(DefaultVariable = "timeoutie")]
            public  override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(IeSettings.IeTimeout);

        }
        public IEFireEventCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                List<string> argumentsList = new List<string>();
                if (arguments.Parameters != null && arguments.Parameters.Value.Count > 0)
                {
                    argumentsList = arguments.Parameters.Value.Select(x => x.ToString()).ToList();
                }
                IEWrapper ie = IEManager.CurrentIE;
                ie.FireEvent(arguments.EventName.Value,
                             argumentsList,
                             arguments.Search.Value,
                             arguments.By.Value,
                             (int)arguments.Timeout.Value.TotalMilliseconds,
                             arguments.NoWait.Value);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while firing '{arguments.EventName.Value}' event. Message: {ex.Message}", ex);
            }
        }
    }
}
