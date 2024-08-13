namespace MoneyControl.Domain.Data.Entities;
public class SubTransactionEntity
{
    public int Id { get; set; }
    public int TransactionId { get; set; } 
    public decimal TotalAmount { get; set; }
    public int PayeeId { get; set; }
    public int CategoryId {  get; set; }
    public int RegularPaymentId { get; set; }
}
