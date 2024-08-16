namespace MoneyControl.Domain.Models;
public class PayeeDetails
{
    public int Id { get; set; }
    public int PayeeId { get; set; }
    public string Details { get; set; }

    public string PayeeName { get; set; }
}
