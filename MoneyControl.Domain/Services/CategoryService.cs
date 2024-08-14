using Microsoft.EntityFrameworkCore;

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

}
