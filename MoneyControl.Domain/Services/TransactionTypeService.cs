using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Data.Entities;

namespace MoneyControl.Domain.Services;
public class TransactionTypeService : ServiceBase, IDisposable
{
    public TransactionTypeService(SqliteDbContext dbContext) : base(dbContext) { }
    public void Dispose() => MyDbContext.Dispose();

    public async Task<List<TransactionType>> GetAllTransTypes()
    {
        var query =
            from transType in MyDbContext.AllTransactionTypes.AsNoTracking()
            select StaticBuilder.BuildTransactionTypeFromEntity(transType);
        return await query.ToListAsync();
    }

  

}
