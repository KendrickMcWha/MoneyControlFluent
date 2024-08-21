using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Data.Context;

namespace MoneyControl.FluentUi.DAL;

public abstract class UIServiceBase
{
    public IDbContextFactory<SqliteDbContext> MyDbContextFactory { get; set; } = null;
    protected SqliteDbContext MyDbContext { get; set; }
    protected Result SuccessResult = new Result(true, string.Empty);

    public UIServiceBase(IDbContextFactory<SqliteDbContext> factory)
    {  
        MyDbContextFactory = factory;
        CreateDbContext();
    }
    public void CreateDbContext()
    {
        MyDbContext = MyDbContextFactory.CreateDbContext();        
    }
}
