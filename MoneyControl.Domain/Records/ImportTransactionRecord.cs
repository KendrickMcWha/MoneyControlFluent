namespace MoneyControl.Domain.Records;
public record ImportTransactionRecord(string line, 
                                    List<string> data,
                                    int AccountId,
                                    string Date,
                                    int DateAsInt,
                                    string Details,
                                    string FundsOut,
                                    string FundsIn,
                                    int TransType )
{
    public string DefaultCat { get; set; } = "Unassigned";
    public string DefaultPay { get; set; } = string.Empty;
    public int? DefaultCatId { get; set; } = 0;
    public int? DefaultPayId { get; set; }

    
    public decimal TransAmount => TransType == 1 ? Convert.ToDecimal(FundsIn)  : Convert.ToDecimal(FundsOut);

}
