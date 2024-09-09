using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Data.Context;
using MoneyControl.Domain.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var payeeDetails = allPayeeDetails.Find(x => x.Details == details);
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


    public async Task<List<Payee>> GetAllPayees()
    {        
        using PayeeService service = new(CreateDbContext());
        return await service.GetAllPayees();
    }
    public async Task<List<Category>> GetAllCategories()
    {
        using CategoryService service = new(CreateDbContext());
        return await service.GetAllCategories();
    }

    public async Task<Result> CreatePayee(Payee payee)
    {
        using PayeeService service = new(CreateDbContext());
        return await service.SavePayee(payee);
    }
    public async Task<Payee> GetPayee(int id)
    {
        using PayeeService payeeService = new(CreateDbContext());
        return await payeeService.GetPayeeWithId(id);
    }
    public async Task<PayeeDetails> GetPayeeDetails(string value)
    {
        using PayeeService payeeService=new(CreateDbContext());
        return await payeeService.GetPayeeDetails(value);
    }
    public async Task<Result> SavePayeeDetails(PayeeDetails payeeDetails)
    {
        using PayeeService payeeService = new(CreateDbContext());
        return await payeeService.SavePayeeDetails(payeeDetails);
    }
    public async Task<Result> SaveImportTransaction(List<ImportFileLineRecord> allFileLines)
    {
        List<ImportTransactionRecord> allImportTrans = new();
        allFileLines.ForEach(x =>
                                allImportTrans.Add(new ImportTransactionRecord(x.line,
                                    x.data,
                                    x.Date,
                                    x.Details,
                                    x.FundsOut,
                                    x.FundsIn
                                ){
                                    DefaultCat = x.DefaultCat,
                                    DefaultPay = x.DefaultPay,
                                    DefaultCatId = x.DefaultCatId,
                                    DefaultPayId = x.DefaultPayId
                                }
                                
        ));
        using TransactionService transactionService = new(CreateDbContext());
        return await transactionService.SaveImportTransactions(allImportTrans);
    }
}
