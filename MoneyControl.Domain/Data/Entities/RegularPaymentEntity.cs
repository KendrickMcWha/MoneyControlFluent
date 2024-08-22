namespace MoneyControl.Domain.Data.Entities;
public class RegularPaymentEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PayeeId { get; set; }
    public int CategoryId { get; set; } 
    public int TransactionType { get; set; }
}
