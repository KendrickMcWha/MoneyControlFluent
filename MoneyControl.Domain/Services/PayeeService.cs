using Microsoft.EntityFrameworkCore;

namespace MoneyControl.Domain.Services;
public class PayeeService : ServiceBase, IDisposable
{
    public PayeeService(SqliteDbContext dbContext) : base(dbContext) { }
    public void Dispose() => MyDbContext.Dispose();

    public async Task<List<Payee>> GetAllPayees()
    {
        var query = 
            from payee in MyDbContext.AllPayees.AsNoTracking()
            select StaticBuilder.BuildPayeeFromEntity(payee);
        return await query.ToListAsync();
    }
    public async Task<List<Payee>> GetAllPayeesWithDefCat()
    {
        var query =
            from payee in MyDbContext.AllPayees.AsNoTracking()
            join cat in MyDbContext.AllCategories.AsNoTracking()
                on payee.DefaultCategoryId equals cat.Id
            select StaticBuilder.BuildPayeeFromEntity(payee, cat);
        return await query.ToListAsync();
    }

}
