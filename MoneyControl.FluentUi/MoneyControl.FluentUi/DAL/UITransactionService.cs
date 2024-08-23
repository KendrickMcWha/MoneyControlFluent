using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Builders;
using MoneyControl.Domain.Data.Context;
using MoneyControl.Domain.Models;
using MoneyControl.Domain.Services;
using MoneyControl.FluentUi.DAL;
using System.ComponentModel;

namespace MoneyControl.FluentUi.DAL;

public class UITransactionService : UIServiceBase, IDisposable, IUITransactionService
{
    public UITransactionService(IDbContextFactory<SqliteDbContext> factory) : base(factory) { }
    public void Dispose() => MyDbContext.Dispose();

    public async Task<List<Account>> GetAllAccounts()
    {
        using AccountService service = new(NewDbContext);
        return await service.GetAllAccounts();
    }

    public async Task<List<Category>> GetAllCategories()
    {
        using CategoryService service = new(NewDbContext);
        return await service.GetAllCategories();
    }

    public async Task<List<Transaction>> GetAllTransactions(TransactionParamPayload payload)
    {
        using TransactionService service = new(NewDbContext);
        return await service.GetAllTransactions(payload);
    }

    
}
