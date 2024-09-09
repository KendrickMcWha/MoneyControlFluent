namespace MoneyControl.Domain.Builders;
public class PayeeDetailsBuilder
{
    private readonly PayeeDetails MyPayeeDetails = new() { Id = 0 };

    public PayeeDetailsBuilder() { }

    public PayeeDetailsBuilder(PayeeDetails payeeDetails) => MyPayeeDetails = payeeDetails;

    public PayeeDetails Build() => MyPayeeDetails;

    public PayeeDetailsBuilder WithDetails(string value)
    {
        MyPayeeDetails.Details = value;
        return this;
    }
    public PayeeDetailsBuilder WithPayeeId(int value)
    {
        MyPayeeDetails.PayeeId = value;
        return this;
    }
    public PayeeDetailsBuilder WithPayeeName(string value)
    {
        MyPayeeDetails.PayeeName = value;
        return this;
    }


}
