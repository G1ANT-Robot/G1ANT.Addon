using G1ANT.Language;
using System;

namespace G1ANT.Addon.IExplorer
{
    [Command(Name = "ie.open",Tooltip = "This command allows to open new instance of Internet Explorer and optionally set webpage address")]
    public class IEOpenCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Webpage address to load")]
            public TextStructure Url { get; set; } = new TextStructure(string.Empty);

            [Argument(DefaultVariable = "timeoutie")]
            public override int Timeout { get; set; } = IeSettings.IeTimeout;

            [Argument(Tooltip = "If set to 'true', command will not wait until document reache completed state")]
            public BooleanStructure NoWait { get; set; } = new BooleanStructure(false);

            [Argument(Tooltip = "Name of variable where opened Internet Explorer instance's id is going to be placed")]
            public TextStructure Result { get; set; } = new TextStructure("result");

            [Argument(Tooltip = "If 'true', opened Internet Explorer instance will detach automatically after script ends")]
            public BooleanStructure AutoDetachOnClose { get; set; } = new BooleanStructure(true);

            [Argument]
            public BooleanStructure If { get; set; } = new BooleanStructure(true);

            [Argument]
            public TextStructure ErrorJump { get; set; }

            [Argument]
            public TextStructure ErrorMessage { get; set; }
        }
        public IEOpenCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            IEWrapper wrapper = CreateIeInstance(arguments.AutoDetachOnClose.Value);
            Navigate(arguments, wrapper);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new IntegerStructure(wrapper.Id));
        }

        private IEWrapper CreateIeInstance(bool closeError)
        {
            try
            {
                IEWrapper wrapper = IEManager.AddIE();
                wrapper.Ie = new WatiN.Core.IE();
                if (closeError)
                {
                    OnScriptEnd = () =>
                    {
                        IEManager.Detach(wrapper);
                    };
                }
                return wrapper;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while trying to create new Internet Explorer instance. Additional message: {ex.Message}", ex);
            }
        }

        private void Navigate(Arguments arguments, IEWrapper wrapper)
        {
            try
            {
                if (!string.IsNullOrEmpty(arguments.Url?.Value))
                {
                    wrapper.GoToUrl(arguments.Url.Value, arguments.NoWait.Value, arguments?.Timeout ?? 0);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failure while navigating Internet Explorer address to '{arguments.Url.Value}'. It's possible that there are problems with internet connection or that the specified page does never reache completed state. Additional message: {ex.Message}", ex);
            }
        }
    }
}
