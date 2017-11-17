﻿
using G1ANT.Language;



using System;
using System.Collections.Generic;

namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "excel.runmacro", Tooltip = "Run macro in currently active excel instance.")]
    public class ExcelRunMacroCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Name of macro that is defined in a workbook")]
            public TextStructure Name { get; set; }

            [Argument(Tooltip = "Comma separated arguments that will be passed to macro")]
            public ListStructure Args { get; set; }

            [Argument]
            public TextStructure Result { get; set; } = new TextStructure("result");
        }

        public ExcelRunMacroCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            try
            {
                List<object> args = new List<object>();

                if (arguments.Args?.Value != null)
                {
                    foreach (Structure arg in arguments.Args?.Value)
                    {
                        TextStructure tmpArgument = arg as TextStructure;
                        if (tmpArgument != null)
                        {
                            args.Add(tmpArgument.Value);
                        }
                    }
                }
                //else
                //{
                //    args.Add(string.Empty);
                //}

                ExcelManager.CurrentExcel.RunMacro(arguments.Name.Value, args);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Problem occured while running excel macro. Path: '{arguments.Name.Value}', Arguments count: '{arguments.Args?.Value?.Count ?? 0}'", ex);
            }
        }
    }
}
