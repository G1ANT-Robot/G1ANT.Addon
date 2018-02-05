
using G1ANT.Language;

namespace G1ANT.Addon.GoogleDocs
{
    [Command(Name = "googlesheet.getvalue", Tooltip= "This command allows to get value from opened Google Sheets instance.")]
    public class GoogleSheetGetValueCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Range like A6 or A3&B7 or A3:A6&B7")]
            public TextStructure Range { get; set; }

            [Argument(Tooltip = "SheetName where range exists, can be empty or omitted")]
            public TextStructure SheetName { get; set; } = new TextStructure(string.Empty);
            [Argument]
            public TextStructure Result { get; set; } = new TextStructure("result");

            
        }
        public GoogleSheetGetValueCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            var sheetsManager = SheetsManager.CurrentSheet;
            var sheetName = arguments.SheetName.Value == "" ? sheetsManager.sheets[0].Properties.Title : arguments.SheetName.Value;
            var val = sheetsManager.GetValue(arguments.Range.Value, sheetName);
            ListStructure result = new ListStructure(new System.Collections.Generic.List<Structure>());
            //string result=null;
            //var result = val. == null ? "" : val.Values[0][0].ToString();
            if (val.ValueRanges[0].Values ==null)
            {
                for (int i = 0; i < val.ValueRanges.Count; i++)
                {
                    TextStructure tmp = new TextStructure("");
                    result.Value.Add(tmp);
                }
                Scripter.Variables.SetVariableValue(arguments.Result.Value, result);
            }
            else
            { 
                for (int i = 0; i < val.ValueRanges.Count; i++)
                {
                    TextStructure tmp = new TextStructure(val.ValueRanges[i].Values[0][0].ToString());
                    result.Value.Add(tmp);
                }
            Scripter.Variables.SetVariableValue(arguments.Result.Value, result);
            }
        }
    }
}
