using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Builders;
using MoneyControl.Domain.Data.Context;
using MoneyControl.Domain.Data.Entities;
using MoneyControl.Domain.Models;
using MoneyControl.Domain.Services;
using MoneyControl.FluentUi.DAL;

namespace MoneyControl.FluentUi.DAL;

public class UICategoryService : UIServiceBase, IDisposable, IUICategoryService
{
    public UICategoryService(IDbContextFactory<SqliteDbContext> factory) : base(factory)
    { }
    public void Dispose() => MyDbContext.Dispose();

    public async Task<List<Category>> GetAllCategories()
    {
        using CategoryService service = new(MyDbContext);
        return await service.GetAllCategories();
    }
    public async Task<List<TransactionType>> GetTransactionTypes()
    {
        using TransactionTypeService service = new(MyDbContext);
        return await service.GetAllTransTypes();
    }

    public async Task<Result> SaveCategory(Category cat)
    {
        CreateDbContext();
        using CategoryService service = new(MyDbContext);
        return await service.SaveCategory(cat);        
    }
}
