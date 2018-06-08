﻿/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Selenium
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Language;
using System;

namespace G1ANT.Addon.Selenium
{
    [Command(Name = "selenium.type", Tooltip = "Type text into element.")]
    public class SeleniumTypeCommand : Command
    {
        public class Arguments : SeleniumCommandArguments
        {
            [Argument(Required = true, Tooltip = "Text to type")]
            public TextStructure Text { get; set; }

            [Argument(DefaultVariable = "timeoutselenium")]
            public  override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(SeleniumSettings.SeleniumTimeout);
        }
        public SeleniumTypeCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                SeleniumManager.CurrentWrapper.TypeText(
                    arguments.Text.Value,
                    arguments,
                    arguments.Timeout.Value);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while typing text to element. Text: '{arguments.Text.Value}'. 'Search element phrase: '{arguments.Search.Value}', by: '{arguments.By.Value}'. Message: {ex.Message}", ex);
            }
        }
    }
}
