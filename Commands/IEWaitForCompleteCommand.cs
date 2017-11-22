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
                ieWrapper.WaitForLoad(arguments.Timeout.Value.Milliseconds / 1000);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while waiting for page to complete. Message: {ex.Message}", ex);
            }            
        }
    }
}
