namespace MoneyControl.FluentUi.Records;

public record FileLine(string line, List<string> data, string Date, string Details, string FundsOut, string FundsIn)
{
    public string DefaultCat { get; set; } = string.Empty;
    public int? DefaultCatId { get; set; }
    
}
