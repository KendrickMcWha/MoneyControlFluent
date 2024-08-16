namespace MoneyControl.Domain.Builders;
public class PayeeBuilder
{
    private readonly Payee MyPayee = new() { Id = 0 };

    public PayeeBuilder() { }
    public PayeeBuilder(Payee payee) => MyPayee = payee;
    public Payee Build() => MyPayee;

    public PayeeBuilder WithName(string value)
    {
        MyPayee.Name = value;
        return this;
    }
    public PayeeBuilder WithDefaultCategoryId(int value)
    {
        MyPayee.DefaultCategoryId = value;
        return this;
    }

}
