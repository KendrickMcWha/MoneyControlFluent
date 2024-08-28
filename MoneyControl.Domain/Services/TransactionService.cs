using Microsoft.EntityFrameworkCore;

namespace MoneyControl.Domain.Services;
public class TransactionService : ServiceBase, IDisposable
{
    public TransactionService(SqliteDbContext context) : base(context) { }
    public void Dispose() => MyDbContext.Dispose();


    public async Task<List<Transaction>> GetAllTransactions(int acctId = 0)
    {
        bool skipFilterAcct = acctId != 0;
        var query =
            from transaction in MyDbContext.AllTransactions
                                    .Where(x => skipFilterAcct || x.AccountId == acctId)
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
    public async Task<List<Transaction>> GetAllTransactions(TransactionParamPayload payload)
    {
        bool skipFilterAcct = payload.accountId == 0;
        bool skipFilterCat = payload.categoryId == 0;
        DateOnly startDate = payload.startDate ?? Constants.DataStartDate;
        DateOnly endDate = payload.endDate ?? DateOnly.FromDateTime(DateTime.Now);
        int startDateInt = Convert.ToInt32( startDate.ToString("yyyyMMdd"));
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
            
            join payDetails in MyDbContext.AllPayeesDetails
                                    .AsNoTracking()
                    on transaction.Details equals payDetails.Details
                    into payDetailsGroup
            from payDetails in payDetailsGroup.DefaultIfEmpty()

            join pay in MyDbContext.AllPayees
                                    .AsNoTracking()
                    on payDetails.PayeeId equals pay.Id
                    into payGroup
            from pay in payGroup.DefaultIfEmpty()

            select StaticBuilder.BuildTransactionFromJoin(transaction, acct, cat, pay);

        return await query.ToListAsync();
    }
    //public async Task<List<Transaction>> GetAllTransactions(DateOnly dateFrom, DateOnly dateTo, Account acct)
    //{
    //    long fromDate = Convert.ToInt64(dateFrom.ToString("yyyyMMdd"));
    //    long toDate = Convert.ToInt64(dateTo.ToString("yyyyMMdd"));
    //    bool skipAcct = acct is null;
    //    var query =
    //        from transaction in MyDbContext.AllTransactions
    //                                        .Where(x => skipAcct || x.AccountId == acct.Id)
    //                                        .Where(x => x.BudgetDateInt >= fromDate && x.BudgetDateInt <= toDate)
    //                                        .AsNoTracking()
    //        select StaticBuilder.BuildTransactionFromEntity(transaction);

    //    return await query.ToListAsync();
    //}

    //public async Task<List<Transaction>> GetAllFullTransactions(DateOnly dateFrom, DateOnly dateTo, Account acct)
    //{
    //    bool skipAcct = acct is null;
    //    var query =
    //        from transaction in MyDbContext.AllTransactions
    //                                        .Where(x => skipAcct || x.AccountId == acct.Id)
    //                                        .Where(x => x.BudgetDate >= dateFrom && x.BudgetDate <= dateTo)
    //                                        .AsNoTracking()
    //        join subTrans in MyDbContext.AllSubTransactions
    //                                    .AsNoTracking()
    //                    on transaction.Id equals subTrans.TransactionId into allSubTrans
    //        select StaticBuilder.BuildTransactionFromEntity(transaction, allSubTrans);

    //    return await query.ToListAsync();
    //}

    public async Task<List<Transaction>> GetTransactionWithId(int transId)
    {

        var query =
            from transaction in MyDbContext.AllTransactions
                                            .Where(x => x.Id == transId)
                                            .AsNoTracking()
            join subTrans in MyDbContext.AllSubTransactions
                                        .AsNoTracking()
                        on transaction.Id equals subTrans.TransactionId into allSubTrans
            select StaticBuilder.BuildTransactionFromEntity(transaction, allSubTrans);

        return await query.ToListAsync();
    }


}
