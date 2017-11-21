using G1ANT.Language;
using System;

namespace G1ANT.Addon.IExplorer
{
    [Command(Name = "ie.saveas", Tooltip = "This command allows to automatically save file to specified directory once the pop-up box appears")]

    public class IESaveasCommand : Command
	{
		public class Arguments : CommandArguments
		{
            [Argument(Required = true, Tooltip = "Specifies file's full save path")]
            public TextStructure Path { get; set; }

			[Argument]
			public BooleanStructure If { get; set; } = new BooleanStructure(true);

			[Argument]
			public TextStructure ErrorJump { get; set; }

			[Argument]
			public TextStructure ErrorMessage { get; set; }
		}
        public IESaveasCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
		{
            try
            {
                IEWrapper ie = IEManager.CurrentIE;
                ie.DownLoadFile(arguments.Path.Value);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while saving file. Message: {ex.Message}", ex);
            }
        }
	}
}
