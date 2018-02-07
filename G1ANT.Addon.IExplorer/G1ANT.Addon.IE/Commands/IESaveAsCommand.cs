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

namespace G1ANT.Addon.IExplorer
{
    [Command(Name = "ie.saveas", Tooltip = "This command allows to automatically save file to specified directory once the pop-up box appears")]

    public class IESaveasCommand : Command
	{
		public class Arguments : CommandArguments
		{
            [Argument(Required = true, Tooltip = "Specifies file's full save path")]
            public TextStructure Path { get; set; }
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
