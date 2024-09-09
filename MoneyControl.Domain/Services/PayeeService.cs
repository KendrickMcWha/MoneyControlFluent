using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Data.Entities;

namespace MoneyControl.Domain.Services;
public class PayeeService : ServiceBase, IDisposable
{
    public PayeeService(SqliteDbContext dbContext) : base(dbContext) { }
    public void Dispose() => MyDbContext.Dispose();

    public async Task<Payee> GetPayeeWithId(int  id)
    {
        var query =
            from payee in MyDbContext.AllPayees
                                        .Where(x => x.Id == id)
                                        .AsNoTracking() 
            select StaticBuilder.BuildPayeeFromEntity(payee);
        return await query.FirstOrDefaultAsync();
    }
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
            select StaticBuilder.BuildPayeeDetails(details, null);//,  payee);

        return await query.ToListAsync();
    }
    public async Task<PayeeDetails> GetPayeeDetails(string value)
    {
        var query =
            from details in MyDbContext.AllPayeesDetails
                                        .Where(x => x.Details == value)
                                        .AsNoTracking()
            select StaticBuilder.BuildPayeeDetails(details, null);

        return await query.FirstOrDefaultAsync();
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
        entity.DisplayName = payee.DisplayName;
        entity.DefaultCategoryId = payee.DefaultCategoryId;

        await MyDbContext.SaveChangesAsync();
                
        return new Result(true, string.Empty, entity.Id);
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

    public async Task<Result> SaveDetailsMakePayee(Payee payee, PayeeDetails payeeDetails)
    {
        Result payeeResult = await SavePayee(payee);
        if (!payeeResult.Success) { return payeeResult; }

        payeeDetails.PayeeId = Convert.ToInt32(payeeResult.Payload);

        return await SavePayeeDetails(payeeDetails);
    }

}
