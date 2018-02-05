using G1ANT.Language;
using System;
using System.Windows.Forms;

namespace G1ANT.Addon.IExplorer
{
    [Command(Name = "ie.activatetab", Tooltip = "This command allows to activate Internet Explorer tab for further use by ie commands. Before using this command, 'ie.attach' or 'ie.open' command has to be invoked.")]
    public class IEActivateTabCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Browser tab title or url")]
            public TextStructure Phrase { get; set; }

            [Argument(Tooltip = "'title' or 'url', determines what to look for in a tab to activate")]
            public TextStructure By { get; set; } = new TextStructure("title");
        }
        public IEActivateTabCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            IEWrapper ieWrapper = IEManager.CurrentIE;
            try
            {
                ieWrapper.ActivateTab(arguments.By.Value, arguments.Phrase.Value);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured during tab activation. Exception message: {ex.Message}", ex);
            }
        }
    }
}
