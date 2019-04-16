/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.IExplorer
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Language;
using System;
using System.Windows.Forms;

namespace G1ANT.Addon.IExplorer
{
    [Command(Name = "ie.activatetab", Tooltip = "This command activates Internet Explorer tab for further use by other `ie.` commands.")]
    public class IEActivateTabCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Browser tab title or URL address")]
            public TextStructure Phrase { get; set; }

            [Argument(Tooltip = "Determines where to search for a  phrase in a tab to activate it: `title` or `url`")]
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
