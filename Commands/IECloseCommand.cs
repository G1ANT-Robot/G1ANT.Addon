using G1ANT.Language;
using System;

namespace G1ANT.Addon.IExplorer
{
    [Command(Name = "ie.close", Tooltip = "This command allows to close currently attached Internet Explorer instance.")]
    public class IECloseCommand : Command
    {
        public class Arguments : CommandArguments
        {
        }
        public IECloseCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                IEManager.Close();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Exception occured while closing Internet Explorer instance. Message: {ex.Message}", ex);
            }
        }
    }
}
