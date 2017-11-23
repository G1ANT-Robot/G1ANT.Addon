using System;
using G1ANT.Language;


namespace G1ANT.Language.Mscrm
{
    [Command(Name = "mscrm.detach",Tooltip = "This command disconnect from instance of CRM attached by 'mscrm.attach'.")]
    class MsCrmDetach : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result"); 
        }
       public MsCrmDetach(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            try
            {
                if (MsCrmManager.CurrentCRM != null)
                {
                    MsCrmManager.Detach(MsCrmManager.CurrentCRM);
                }
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(true));
            }
            catch
            {
                throw new ApplicationException("Unable to attach to CRM");
            }
        }
    }
}
