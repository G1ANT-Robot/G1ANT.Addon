﻿using G1ANT.Language;
using System;

namespace G1ANT.Addon.IExplorer
{
    [Command(Name = "ie.refresh", Tooltip = "This command refreshes currently attached Internet Explorer instance")]
    public class IERefreshCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument]
            public BooleanStructure If { get; set; } = new BooleanStructure(true);

            [Argument(DefaultVariable = "timeoutie")]
            public override int Timeout { get; set; } = IeSettings.IeTimeout;
        }
        public IERefreshCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                IEWrapper ieWrapper = IEManager.CurrentIE;
                ieWrapper.Refresh(arguments.Timeout);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while refreshing page. Message: {ex.Message}", ex);
            }       
        }
    }
}
