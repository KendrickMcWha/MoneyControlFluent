using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Services;

namespace MoneyControl.FluentUi.DAL;

public interface IUIImportService
{
    Task<List<Account>> GetAllAccounts();
    void SetDefaultCategories(List<ImportFileLineRecord> allFileLines);
    Task<List<Category>> GetAllCategories();
    Task<List<Payee>> GetAllPayees();
}
