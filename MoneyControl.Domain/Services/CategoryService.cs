using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Data.Entities;

namespace MoneyControl.Domain.Services;
public class CategoryService : ServiceBase, IDisposable
{
    public CategoryService(SqliteDbContext context) : base(context) { }
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
