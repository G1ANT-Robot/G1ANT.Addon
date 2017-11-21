using G1ANT.Language;
using System;

namespace G1ANT.Addon.IExplorer
{
    [Command(Name = "ie.detach", Tooltip = "This command allows to detach currently attached Internet Explorer")]
    public class IEDetachCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument]
            public BooleanStructure If { get; set; } = new BooleanStructure(true);

            [Argument]
            public TextStructure ErrorJump { get; set; }

            [Argument]
            public TextStructure ErrorMessage { get; set; }
        }
        public IEDetachCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                IEManager.Detach(IEManager.CurrentIE);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while detaching Internet Explorer instance. Message: {ex.Message}", ex);
            }
        }
    }
}
