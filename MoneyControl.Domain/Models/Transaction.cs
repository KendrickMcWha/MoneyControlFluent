namespace MoneyControl.Domain.Models;
public class Transaction
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int CategoryId { get; set; }
    public decimal TotalAmount { get; set; }
    public int TransType { get; set; }
    public int TransDateStr { get; set; }
    public int PostDateStr { get; set; }
    public int BudgetDateStr { get; set; }
    public int? RegularPaymentId { get; set; }
    public string Details { get; set; }
    public string Reference { get; set; }


    public List<SubTransaction> AllSubTransactions { get; set; } = new();
    public string AccountName { get; set; }
    public string CategoryName { get; set; }
    public string AmountCurrency => Math.Abs(TotalAmount).ToString("C");
    public DateOnly BudgetDate { get; set; }
    public int BudgetDateInt => Convert.ToInt32(BudgetDateStr);
    public string TransTypeDisplay => TransType switch
    {
        -2 => "Transfer Out",
        -1 => "Debit",
        1 => "Credit",
        2 => "Transfer In",
        _ => "ERROR"
    };

}
