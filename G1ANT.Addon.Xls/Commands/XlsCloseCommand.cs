using G1ANT.Language;
using System;

namespace G1ANT.Addon.Xls
{
    [Command(Name = "xls.close",Tooltip = "This command allows to save changes and close .xlsx file.")]
    public class XlsCloseCommand :Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "ID of file to close. If not set, will close file opened as first")]
            public IntegerStructure Id { get; set; }
        }
        public XlsCloseCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            int ID;
            if (arguments.Id == null)
            {
                ID = XlsManager.getFirstId();
            }
            else
            {
                ID = arguments.Id.Value;
            }
            XlsManager.Remove(ID);
        }
    }
}
