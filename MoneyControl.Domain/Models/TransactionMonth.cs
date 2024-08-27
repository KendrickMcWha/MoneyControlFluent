namespace MoneyControl.Domain.Models;

public class TransactionMonth
{
    public DateOnly MonthDate { get; set; }
    public int Type { get; set; }
    public List<Transaction> AllTransactions { get; set; } = new();
    public decimal TotalCredit { get; set; } = 0;
    public decimal TotalDebit { get; set; } = 0;
    public decimal TotalDiff => TotalCredit - TotalDebit;
    public string TypeDisplay => Type switch
    {
        1 => "Credit",
        -1 => "Debit",
        _ => "UNKNOWN"
    };
    public string MonthDisplay => MonthDate.ToString("yyyy MMM");

    public TransactionMonth(DateOnly monthDate) => MonthDate = monthDate;
    public TransactionMonth(DateOnly monthDate, int type)
    {
        MonthDate = monthDate;
        Type = type;
    }


    public void BuildBalances()
    {
        foreach (var transaction in AllTransactions)
        {
            if (transaction.TransType == 1 || transaction.TransType == 2)
            {
                TotalCredit += Math.Abs(transaction.TotalAmount);
            }
            else if (transaction.TransType == -1 || transaction.TransType == -2)
            {
                TotalDebit += Math.Abs(transaction.TotalAmount);
            }
        }
    }
}
