using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Builders;
using MoneyControl.Domain.Data.Context;
using MoneyControl.Domain.Data.Entities;
using MoneyControl.Domain.Models;
using MoneyControl.Domain.Services;
using MoneyControl.FluentUi.DAL;

namespace MoneyControl.FluentUi.DAL;

public class UIPayeeService : UIServiceBase, IDisposable, IUIPayeeService
{
    public UIPayeeService(IDbContextFactory<SqliteDbContext> factory) : base(factory) { }
    public void Dispose() => MyDbContext.Dispose();

    public async Task<Result> DeletePayee(Payee payee)
    {
        CreateDbContext();
        using PayeeService service = new(MyDbContext);
        return await service.DeletePayee(payee);
    }


    public async Task<List<PayeeDetails>> GetAllPayeeDetails()
    {
        CreateDbContext();
        using PayeeService service = new(MyDbContext);
        return await service.GetAllPayeeDetails();
    }

    public async Task<List<Payee>> GetAllPayees()
    {
        CreateDbContext();
        using PayeeService service = new(MyDbContext);
        return await service.GetAllPayees();
    }

    public async Task<List<Payee>> GetAllPayeesWithDefCat()
    {
        using PayeeService service = new(CreateDbContext());
        return await service.GetAllPayeesWithDefCat();
    }
    public async Task<List<Category>> GetAllCategories()
    {
        using CategoryService service = new(MyDbContext);
        return await service.GetAllCategories();
    }
    public async Task<List<Transaction>> GetAllTransactions(TransactionParamPayload payload)
    {
        CreateDbContext();
        using TransactionService service = new(MyDbContext);
        return await service.GetAllTransactions(payload);
    }

    public async Task<Result> SavePayee(Payee payee)
    {
        CreateDbContext();
        using PayeeService service = new(MyDbContext);
        return await service.SavePayee(payee);
    }

    public async Task<Result> SavePayeeDetails(PayeeDetails details)
    {
        CreateDbContext();
        using PayeeService service = new(MyDbContext);
        return await service.SavePayeeDetails(details);
    }

    public async Task<Result> SaveDetailsMakePayee(Payee payee, PayeeDetails details)
    {
        using PayeeService service = new(CreateDbContext());
        return await service.SaveDetailsMakePayee(payee, details);
    }
}
