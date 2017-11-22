using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;
using G1ANT.Language.Structures;

namespace G1ANT.Language.GoogleDocs.Commands
{
    [Command(Name = "googlesheet.getvalue", ToolTip= "This command allows to get value from opened Google Sheets instance.")]
    public class GoogleSheetGetValue : CommandBase<GoogleSheetGetValue.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Range like A6 or A3&B7 or A3:A6&B7")]
            public Structures.String Range { get; set; }

            [Argument(Tooltip = "SheetName where range exists, can be empty or omitted")]
            public Structures.String SheetName { get; set; } = new Structures.String(string.Empty);
            [Argument]
            public Structures.String Result { get; set; } = new Structures.String("result");

            [Argument]
            public Structures.Bool If { get; set; } = new Structures.Bool(true);

            [Argument]
            public Structures.String ErrorJump { get; set; }

            [Argument]
            public Structures.String ErrorMessage { get; set; }
        }

        public override void Execute(Arguments arguments, IExecutionContext executionContext)
        {
            var sheetsManager = SheetsManager.CurrentSheet;
            var sheetName = arguments.SheetName.Value == "" ? sheetsManager.sheets[0].Properties.Title : arguments.SheetName.Value;
            var val = sheetsManager.GetValue(arguments.Range.Value, sheetName);
            Structures.List result = new Structures.List(new System.Collections.Generic.List<Structure>());
            //string result=null;
            //var result = val. == null ? "" : val.Values[0][0].ToString();
            if (val.ValueRanges[0].Values ==null)
            {
                for (int i = 0; i < val.ValueRanges.Count; i++)
                {
                    Structures.String tmp = new Structures.String("");
                    result.Value.Add(tmp);
                }
                SetVariableValue(arguments.Result.Value, result);
            }
            else
            { 
                for (int i = 0; i < val.ValueRanges.Count; i++)
                {
                    Structures.String tmp = new Structures.String(val.ValueRanges[i].Values[0][0].ToString());
                    result.Value.Add(tmp);
                }
            SetVariableValue(arguments.Result.Value, result);
            }
        }
    }
}
