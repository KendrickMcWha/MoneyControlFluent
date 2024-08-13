namespace MoneyControl.Domain.Services;
public class ServiceBase
{
    protected SqliteDbContext MyDbContext { get; set; }

    public ServiceBase(SqliteDbContext context) => MyDbContext = context;
}
