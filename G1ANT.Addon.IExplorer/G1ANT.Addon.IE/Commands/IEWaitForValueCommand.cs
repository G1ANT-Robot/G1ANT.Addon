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
using System.Windows.Forms;

namespace G1ANT.Addon.IExplorer
{
    [Command(Name = "ie.waitforvalue", Tooltip = "This command keeps on executing specified javascript until specified value is returned")]
    public class IEWaitForValueCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true,Tooltip = "Pass the full script as string to get it evaluated in browser")]
            public TextStructure Script { get; set; }

            [Argument(Required = true, Tooltip = "Value what we expect that script will return")]
            public TextStructure ExpectedValue { get; set; }
        }
        public IEWaitForValueCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            int timeout = (int)arguments.Timeout.Value.TotalMilliseconds;
            int start = Environment.TickCount;
            string result = string.Empty;
            IEWrapper ieWrapper = IEManager.CurrentIE;

            while (Math.Abs(Environment.TickCount - start) < timeout &&
                   Scripter.Stopped == false &&
                    result.ToLower() != arguments.ExpectedValue?.Value?.ToLower()) 
            {
                try
                {
                    result = ieWrapper.InsertJavaScriptAndTakeResult(arguments.Script.Value) ?? string.Empty;
                }
                catch
                {
                    // JavaScript exception occured but we don't really care
                    // maybe element did not exist and we got exception cause of that
                }                
                Application.DoEvents();
            }
            if (result.ToLower() != arguments.ExpectedValue.Value?.ToLower())
            {
                throw new TimeoutException("Timeout occured while waiting for an element. Specified element was not found.");
            }
        }
    }
}
