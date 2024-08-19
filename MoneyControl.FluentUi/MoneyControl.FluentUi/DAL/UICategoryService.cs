using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Builders;
using MoneyControl.Domain.Data.Context;
using MoneyControl.Domain.Data.Entities;
using MoneyControl.Domain.Models;
using MoneyControl.FluentUi.DAL;

namespace MoneyControl.FluentUi.DAL;

public class UICategoryService : UIServiceBase, IDisposable, IUICategoryService
{
    public UICategoryService(IDbContextFactory<SqliteDbContext> factory) : base(factory) { }
    public void Dispose() => MyDbContext.Dispose();

    public async Task<List<Category>> GetAllCategories()
    {
        var query =
            from cat in MyDbContext.AllCategories.AsNoTracking()
            select StaticBuilder.BuildCategoryFromEntity(cat);
        return await query.ToListAsync();
    }

    public async Task<Result> SaveCategory(Category cat)
    {
        // Duplicate check.
        if (MyDbContext.AllCategories
            .Where(x => x.Name == cat.Name)
            .Where(x => x.Id != cat.Id)
            .Any()
            )
        {
            return new Result(false, "Duplicate Name Found.");
        }

        CategoryEntity entity = MyDbContext.AllCategories
            .Where(x => x.Id == cat.Id).FirstOrDefault();

        if (entity == null)
        {
            entity = new CategoryEntity();
            MyDbContext.AllCategories.Add(entity);
        }
        entity.Name = cat.Name;
        entity.Type = cat.Type;

        await MyDbContext.SaveChangesAsync();

        return new Result(true, string.Empty);
    }
}
