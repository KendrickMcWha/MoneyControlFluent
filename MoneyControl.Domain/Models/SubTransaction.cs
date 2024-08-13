namespace MoneyControl.Domain.Models;
public class SubTransaction
{
    public int Id { get; set; }
    public int TransactionId { get; set; }
    public decimal TotalAmount { get; set; }
    public int PayeeId { get; set; }
    public int CategoryId { get; set; }
    public int RegularPaymentId { get; set; }
    public Transaction MyTransaction { get; set; }
}
