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
    [Command(Name = "ie.seturl", Tooltip = "Navigates Internet Explorer to specified address")]
    public class IESetUrlCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true,Tooltip = "Address to navigate to")]
            public TextStructure Url { get; set; }

            [Argument(Tooltip = "If set to 'true', command will not wait until document reach completed state")]
            public BooleanStructure NoWait { get; set; } = new BooleanStructure(false);

            [Argument(DefaultVariable = "timeoutie")]
            public override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(IeSettings.IeTimeout);
        }
        public IESetUrlCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                var ieWrapper = IEManager.CurrentIE;
                ieWrapper.GoToUrl(arguments.Url.Value, arguments.NoWait.Value, (int)arguments.Timeout.Value.TotalMilliseconds);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while trying to set url. Message: {ex.Message}", ex);
            }
        }
    }
}
