using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Builders;
using MoneyControl.Domain.Data.Context;
using MoneyControl.Domain.Models;
using MoneyControl.FluentUi.DAL;

namespace MoneyControl.FluentUi.DAL;

public class UITransactionService : UIServiceBase, IDisposable, IUITransactionService
{
    public UITransactionService(IDbContextFactory<SqliteDbContext> factory) : base(factory) { }
    public void Dispose() => MyDbContext.Dispose();

    public async Task<List<Transaction>> GetAllTransactions(TransactionParamPayload payload)
    {
        bool skipFilterAcct = payload.accountId == 0;
        bool skipFilterCat = payload.categoryId == 0;
        DateOnly startDate = payload.startDate ?? Constants.DataStartDate;
        DateOnly endDate = payload.endDate ?? DateOnly.FromDateTime(DateTime.Now);
        int startDateInt = Convert.ToInt32(startDate.ToString("yyyyMMdd"));
        int endDateInt = Convert.ToInt32(endDate.ToString("yyyyMMdd"));

        var query =
            from transaction in MyDbContext.AllTransactions
                                    .Where(x => skipFilterAcct || x.AccountId == payload.accountId)
                                    .Where(x => skipFilterCat || x.CategoryId == payload.categoryId)
                                    .Where(x => x.BudgetDate >= startDateInt)
                                    .Where(x => x.BudgetDate <= endDateInt)
                                    .OrderBy(x => x.BudgetDate)
                                    .AsNoTracking()
            join acct in MyDbContext.AllAccounts
                                    .AsNoTracking()
                    on transaction.AccountId equals acct.Id
            join cat in MyDbContext.AllCategories
                                    .AsNoTracking()
                    on transaction.CategoryId equals cat.Id

            select StaticBuilder.BuildTransactionFromJoin(transaction, acct, cat);

        return await query.ToListAsync();
    }
}
