namespace MoneyControl.Domain.Data.Entities;
public class TransactionEntity
{
    public int Id { get; set; }
    public int AccountId { get;set; }
    public int CategoryId { get; set; }
    public decimal TotalAmount { get; set; }
    public int TransType { get; set; }
    public int TransDate { get; set; } 
    public int PostDate { get; set; }
    public int BudgetDate { get; set; }
   // public int? RegularPaymentId { get; set; }
    public string Details { get; set; }
    public string Reference { get; set; }
    public int PayeeId { get; set; }


    //public long TransDateInt => Convert.ToInt64(TransDate);
    //public long BudgetDateInt => Convert.ToInt64(BudgetDate);

    
}
