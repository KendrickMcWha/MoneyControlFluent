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
    public async void SetDefaultCategories(List<FileLine> allFileLines)
    {
        CreateDbContext();
        PayeeService payeeService = new(MyDbContext);
        var allPayees = await payeeService.GetAllPayees();
       // CreateDbContext();
        var allPayeeDetails = await payeeService.GetAllPayeeDetails();

        foreach (var fileLine in allFileLines)
        {
            string details = fileLine.Details;
            var payeeDetails = allPayeeDetails.Find(x => x.PayeeName == details);
            if (payeeDetails is not null)
            {
                var detailsPayee = allPayees.Where(x => x.Id == payeeDetails.PayeeId).FirstOrDefault();
                fileLine.DefaultCat = detailsPayee.Name;
                fileLine.DefaultCatId = detailsPayee.Id;
                continue;
            }
            var payee = allPayees.Find(x => x.Name == details);
            if (payee is not null)
            {
                fileLine.DefaultCat = payee.Name;
                fileLine.DefaultCatId = payee.Id;
            }
        }

    }
}
