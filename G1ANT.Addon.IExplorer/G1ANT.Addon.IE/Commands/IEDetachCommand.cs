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
    [Command(Name = "ie.detach", Tooltip = "This command allows to detach currently attached Internet Explorer")]
    public class IEDetachCommand : Command
    {
        public class Arguments : CommandArguments
        {
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
