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
    [Command(Name = "ie.refresh", Tooltip = "This command refreshes currently attached Internet Explorer instance")]
    public class IERefreshCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(DefaultVariable = "timeoutie")]
            public  override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(IeSettings.IeTimeout);
        }
        public IERefreshCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                IEWrapper ieWrapper = IEManager.CurrentIE;
                ieWrapper.Refresh((int)arguments.Timeout.Value.TotalMilliseconds);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while refreshing page. Message: {ex.Message}", ex);
            }       
        }
    }
}
