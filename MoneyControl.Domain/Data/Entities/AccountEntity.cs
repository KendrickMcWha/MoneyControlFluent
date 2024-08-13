namespace MoneyControl.Domain.Data.Entities;
public class AccountEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type {  get; set; }
    public decimal Balance { get; set; }
    public decimal StartingBalance { get; set; }

}
