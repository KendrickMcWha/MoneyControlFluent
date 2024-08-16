using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Data.Entities;

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
    public async Task<List<PayeeDetails>> GetAllPayeeDetails()
    {
        var query =
            from details in MyDbContext.AllPayeesDetails.AsNoTracking()
            join payee in MyDbContext.AllPayees.AsNoTracking()
                on details.PayeeId equals payee.Id
            select StaticBuilder.BuildPayeeDetails(details,  payee);

        return await query.ToListAsync();
    }


    public async Task<Result> SavePayee(Payee payee)
    {
        if (MyDbContext.AllPayees.Any(x => x.Id != payee.Id && x.Name == payee.Name ))
        {
            return new Result(false, "Duplicate Payee Name.");
        }

        PayeeEntity entity = MyDbContext.AllPayees.Where(x => x.Id == payee.Id).FirstOrDefault();
        if (entity == null)
        {
            entity = new PayeeEntity();
            MyDbContext.AllPayees.Add(entity);
        }
        entity.Name = payee.Name;
        entity.DefaultCategoryId = payee.DefaultCategoryId;

        await MyDbContext.SaveChangesAsync();

        return SuccessResult;
    }

    public async Task<Result> SavePayeeDetails(PayeeDetails details)
    {


        PayeeDetailsEntity entity = MyDbContext.AllPayeesDetails.Where(x => x.Id == details.Id).FirstOrDefault();
        if (entity == null)
        {
            entity = new PayeeDetailsEntity();
            MyDbContext.AllPayeesDetails.Add(entity);
        }
        entity.Details = details.Details;
        entity.PayeeId = details.PayeeId;

        await MyDbContext.SaveChangesAsync();

        return SuccessResult;
    }

    public async Task<Result> DeletePayee(Payee payee)
    {
        PayeeEntity entity = MyDbContext.AllPayees.Where(x => x.Id == payee.Id).FirstOrDefault();
        if (entity is null)
        {
            return new Result(false, "Could not locate Payee.");            
        }

        MyDbContext.AllPayees.Remove(entity);
        await MyDbContext.SaveChangesAsync();
        return SuccessResult;
    }


}
