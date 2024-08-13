using Microsoft.EntityFrameworkCore;

namespace MoneyControl.Domain.Services;
public class TransactionService : ServiceBase, IDisposable
{
    public TransactionService(SqliteDbContext context) : base(context) { }
    public void Dispose() => MyDbContext.Dispose();


    public async Task<List<Transaction>> GetAllTransactions()
    {
        var query =
            from transaction in MyDbContext.AllTransactions
                                            .AsNoTracking()
                //select StaticBuilder.BuildTransactionFromEntity(transaction);
            select new Transaction()
            {
                Id = transaction.Id,
                CategoryId = transaction.CategoryId,
                TotalAmount = transaction.TotalAmount,
                TransType = transaction.TransType,
                TransDate = transaction.TransDate,
                PostDate = transaction.PostDate,
                BudgetDate = transaction.BudgetDate,
                //RegularPaymentId = transaction.RegularPaymentId,
                Details = transaction.Details,
                Reference = transaction.Reference,
            };

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
