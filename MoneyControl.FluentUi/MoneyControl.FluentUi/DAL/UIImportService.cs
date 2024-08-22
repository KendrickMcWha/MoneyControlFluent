using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Data.Context;
using MoneyControl.Domain.Services;

namespace MoneyControl.FluentUi.DAL;

public class UIImportService : UIServiceBase, IDisposable, IUIImportService
{
    public UIImportService(IDbContextFactory<SqliteDbContext> factory) : base(factory) { }
    public void Dispose() => MyDbContext.Dispose();

    public async Task<List<Account>> GetAllAccounts()
    {
        using AccountService service = new(MyDbContext);
        return await service.GetAllAccounts();
    }
    public async void SetDefaultCategories(List<ImportFileLineRecord> allFileLines)
    {
        CreateDbContext();
        PayeeService payeeService = new(MyDbContext);
        var allPayees = await payeeService.GetAllPayees();
       // CreateDbContext();
        var allPayeeDetails = await payeeService.GetAllPayeeDetails();
        CategoryService categoryService = new(MyDbContext);
        var allCategories = await categoryService.GetAllCategories();

        foreach (var fileLine in allFileLines)
        {
            string details = fileLine.Details;
            Payee payee = null;
            var payeeDetails = allPayeeDetails.Find(x => x.PayeeName == details);
            if (payeeDetails is not null)
            {
                payee = allPayees.Find(x => x.Id == payeeDetails.PayeeId);                
            }
            if (payee is null)
            {
                payee = allPayees.Find(x => x.Name == details);
            }
            if (payee is not null)
            {
                fileLine.DefaultPay = payee.Name;
                fileLine.DefaultPayId = payee.Id;
                fileLine.DefaultCatId = payee.DefaultCategoryId;
                fileLine.DefaultCat = allCategories.Find(x => x.Id == payee.DefaultCategoryId)?.Name ?? string.Empty;
            }
        }



    }
}
