namespace MoneyControl.Domain.Services;
public class ServiceBase
{
    protected SqliteDbContext MyDbContext { get; set; }
    protected Result SuccessResult = new Result(true, string.Empty);

    public ServiceBase(SqliteDbContext context) => MyDbContext = context;
}
