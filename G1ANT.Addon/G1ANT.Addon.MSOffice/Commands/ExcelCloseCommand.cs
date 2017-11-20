
using G1ANT.Language;


using System;

namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "excel.close", Tooltip = "Closes currently active Excel instance.")]
    public class ExcelCloseCommand : Command
    {
        public class Arguments : CommandArguments
        {
        }

        public ExcelCloseCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            try
            {
                ExcelManager.RemoveInstance();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while closing current excel instance. Message: '{ex.Message}'", ex);
            }
        }
    }
}
