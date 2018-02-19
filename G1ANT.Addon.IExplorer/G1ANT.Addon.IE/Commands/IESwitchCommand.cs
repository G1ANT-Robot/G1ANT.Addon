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
    [Command(Name = "ie.switch",Tooltip = "Switches context to already opened/attached Internet Explorer instance")]
    public class IESwitchCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Id number stored in a variable of Internet Explorer instance to switch to")]
            public IntegerStructure Id { get; set; }
        }
        public IESwitchCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            if (IEManager.SwitchIE(arguments.Id.Value) == false)
            {
                throw new InvalidOperationException($"Internet Instance with specified Id: '{arguments.Id.Value}' could not be found.");
            }
        }
    }
}
