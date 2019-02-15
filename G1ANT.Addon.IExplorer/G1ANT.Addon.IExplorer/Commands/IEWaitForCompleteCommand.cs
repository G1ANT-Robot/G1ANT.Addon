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
using System.Threading;

namespace G1ANT.Addon.IExplorer
{
    [Command(Name = "ie.waitforcomplete", Tooltip = "This command waits until page reaches completed state")]
    public class IEWaitForCompleteCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Specifies maximum number of milliseconds to wait for window to get loaded", DefaultVariable = "timeoutie")]
            public  override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(IeSettings.IeTimeout);
        }
        public IEWaitForCompleteCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                IEWrapper ieWrapper = IEManager.CurrentIE;
                ieWrapper.WaitForLoad((int)arguments.Timeout.Value.TotalMilliseconds / 1000);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while waiting for page to complete. Message: {ex.Message}", ex);
            }            
        }
    }
}
