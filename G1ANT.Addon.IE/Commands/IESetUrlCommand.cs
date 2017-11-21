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

            [Argument(Tooltip = "If set to 'true', command will not wait until document reache completed state")]
            public BooleanStructure NoWait { get; set; } = new BooleanStructure(false);

            [Argument(DefaultVariable = "timeoutie")]
            public override int Timeout { get; set; } = IeSettings.IeTimeout;
        }
        public IESetUrlCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                var ieWrapper = IEManager.CurrentIE;
                ieWrapper.GoToUrl(arguments.Url.Value, arguments.NoWait.Value, arguments.Timeout);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while trying to set url. Message: {ex.Message}", ex);
            }
        }
    }
}
