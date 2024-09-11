namespace MoneyControl.FluentUi.Records;

public record ImportFileLineRecord(string line, List<string> data, 
                                    int Account,
                                    string Date,
                                    int DateAsInt,
                                    string Details, 
                                    string FundsOut, 
                                    string FundsIn)
{
    public string DefaultCat { get; set; } = "Unassigned";
    public string DefaultPay { get; set; } = string.Empty;
    public int? DefaultCatId { get; set; } = 0;
    public int? DefaultPayId { get; set; }
    public int TransType => string.IsNullOrEmpty(FundsOut) ? 1 : -1;
    
}

