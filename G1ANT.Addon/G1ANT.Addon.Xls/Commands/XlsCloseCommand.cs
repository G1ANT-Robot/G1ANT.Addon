using System;

namespace G1ANT.Language.Xls
{
    [Command(Name = "xls.close",Tooltip = "This command allows to save changes and close .xlsx file.")]
    public class XlsCloseCommand :Command
    {
        public class Arguments : CommandArguments
        {
            [Argument]
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
