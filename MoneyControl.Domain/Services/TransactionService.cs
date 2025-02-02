using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Data.Entities;

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
        bool skipFilterCat = payload.categoryId == -1;
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

    public async Task<Result> SaveImportTransactions(List<ImportTransactionRecord> allTransRecords)
    {

        foreach (var trans in allTransRecords)
        {
            await SaveImportTransaction(trans);

        }

        await MyDbContext.SaveChangesAsync();

        return new Result(true, string.Empty);
    }
    private async Task SaveImportTransaction(ImportTransactionRecord transactionRecord)
    {
        TransactionEntity entity = new()
        {
            AccountId = transactionRecord.AccountId,
            BudgetDate = transactionRecord.DateAsInt,
            TransDate = transactionRecord.DateAsInt,
            CategoryId = transactionRecord.DefaultCatId ?? 0,
            Details = transactionRecord.Details,
            PostDate = transactionRecord.DateAsInt,
            TotalAmount = transactionRecord.TransAmount,
            TransType = transactionRecord.TransType,
            Reference = string.Empty
        };

        MyDbContext.AllTransactions.Add(entity);
    }

    public async Task<bool> SetTransactionCategory(Transaction trans)
    {
        TransactionEntity entity = MyDbContext.AllTransactions.FirstOrDefault(x => x.Id == trans.Id);
        if (entity is null) return false;

        entity.CategoryId = trans.CategoryId;

        await MyDbContext.SaveChangesAsync();
        return true;
    }
    public async Task<bool> SetTransactionType(Transaction trans)
    {
        TransactionEntity entity = MyDbContext.AllTransactions.FirstOrDefault(x => x.Id == trans.Id);
        if (entity is null) return false;

        entity.TransType = trans.TransType;

        await MyDbContext.SaveChangesAsync();
        return true;
    }
    public async Task<bool> SetTransactionPayee(Transaction trans)
    {
        TransactionEntity entity = MyDbContext.AllTransactions.FirstOrDefault(x => x.Id == trans.Id);
        if (entity is null) return false;

        entity.PayeeId = trans.PayeeId;

        await MyDbContext.SaveChangesAsync();
        return true;
    }

}
