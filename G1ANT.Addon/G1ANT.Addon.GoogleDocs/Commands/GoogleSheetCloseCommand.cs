using G1ANT.Language;

namespace G1ANT.Addon.GoogleDocs
{
    [Command(Name = "googlesheet.close", Tooltip = "This command closes Google Sheets instance.")]
    public class GoogleSheetCloseCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Id of spreadsheet that we are closing")]
            public IntegerStructure Id { get; set; }
        }
        public GoogleSheetCloseCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            var id = arguments.Id != null ? arguments.Id.Value : SheetsManager.CurrentSheet.Id;
            SheetsManager.Remove(id);
        }
    }
}
