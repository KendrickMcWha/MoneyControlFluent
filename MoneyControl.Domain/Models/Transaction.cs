namespace MoneyControl.Domain.Models;
public class Transaction
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int CategoryId { get; set; }
    public decimal TotalAmount { get; set; }
    public int TransType { get; set; }
    public string TransDate { get; set; }
    public string PostDate { get; set; }
    public string BudgetDate { get; set; }
    public int? RegularPaymentId { get; set; }
    public string Details { get; set; }
    public string Reference { get; set; }


    public List<SubTransaction> AllSubTransactions { get; set; } = new();
}
