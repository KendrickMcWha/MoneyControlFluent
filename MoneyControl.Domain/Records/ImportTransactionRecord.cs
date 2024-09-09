namespace MoneyControl.Domain.Records;
public record ImportTransactionRecord(string line, List<string> data,
                                    string Date,
                                    string Details,
                                    string FundsOut,
                                    string FundsIn)
{
    public string DefaultCat { get; set; } = "Unassigned";
    public string DefaultPay { get; set; } = string.Empty;
    public int? DefaultCatId { get; set; } = 0;
    public int? DefaultPayId { get; set; }

}
