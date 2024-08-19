using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Builders;
using MoneyControl.Domain.Data.Context;
using MoneyControl.Domain.Models;
using MoneyControl.FluentUi.DAL;

namespace MoneyControl.FluentUi.DAL;

public class UITransactionTypeService : UIServiceBase, IDisposable, IUITransactionTypeService
{
    public UITransactionTypeService(IDbContextFactory<SqliteDbContext> factory) : base(factory) { }
    public void Dispose() => MyDbContext.Dispose();

    public async Task<List<TransactionType>> GetAllTransTypes()
    {
        var query =
            from transType in MyDbContext.AllTransactionTypes.AsNoTracking()
            select StaticBuilder.BuildTransactionTypeFromEntity(transType);
        return await query.ToListAsync();
    }
}
