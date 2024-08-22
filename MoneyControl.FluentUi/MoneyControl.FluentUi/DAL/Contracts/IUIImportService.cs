namespace MoneyControl.FluentUi.DAL;

public interface IUIImportService
{
    Task<List<Account>> GetAllAccounts();
    void SetDefaultCategories(List<FileLine> allFileLines);
}
