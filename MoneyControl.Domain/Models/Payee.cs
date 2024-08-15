namespace MoneyControl.Domain.Models;
public class Payee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DefaultCategoryId { get; set; }
    public string DisplayName { get; set; }


    public string DefaultCategoryName { get; set; } = string.Empty;
}
