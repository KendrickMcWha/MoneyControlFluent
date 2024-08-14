using Microsoft.EntityFrameworkCore;
namespace MoneyControl.Domain.Services;
public class AccountService : ServiceBase, IDisposable
{

    public AccountService(SqliteDbContext context) : base(context) { }
    public void Dispose() => MyDbContext.Dispose();

    public async Task<List<Account>> GetAllAccounts()
    {
        var query = 
            from acct in MyDbContext.AllAccounts.AsNoTracking()
            select StaticBuilder.BuildAccountFromEntity(acct);
        return await query.ToListAsync();
    }

}
