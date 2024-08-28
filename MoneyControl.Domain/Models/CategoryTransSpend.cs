namespace MoneyControl.Domain.Models;

public class CategoryTransSpend
{
    public Category MyCategory { get; set; }
    public TransactionMonth MyTransactionMonth { get; set; }
    public List<Transaction> AllTransactions { get; set; } = new();
    public decimal TotalCredit { get; set; }
    public decimal TotalDebit { get; set; }
    public string CatName => MyCategory.Name ?? "NULL";
    public string CatSpendDisplay => $"{CatName} _ ${TotalDebit}";
    public string CatSpendTitle => $"Spending By Category : {MyTransactionMonth?.MonthDisplay}";

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
