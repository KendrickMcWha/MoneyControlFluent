using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace MoneyControl.Domain.Services;

public class TestService : IDisposable
{
    [Inject] public IDbContextFactory<SqliteDbContext> MyDbContextFactory { get; set; } = null;
    protected SqliteDbContext MyDbContext { get; set; }
    protected Result SuccessResult = new Result(true, string.Empty);

    public TestService( ) 
    {
        MyDbContext = MyDbContextFactory.CreateDbContext();
    }
    public void Dispose() => MyDbContext.Dispose();

    public async Task<List<Account>> GetAllAccounts()
    {
        var query =
            from acct in MyDbContext.AllAccounts.AsNoTracking()
            select StaticBuilder.BuildAccountFromEntity(acct);
        return await query.ToListAsync();
    }
}
