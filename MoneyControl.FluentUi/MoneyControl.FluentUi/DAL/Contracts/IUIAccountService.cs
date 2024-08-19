namespace MoneyControl.FluentUi.DAL;

public interface IUIAccountService
{
    Task<List<Account>> GetAllAccounts();
}
