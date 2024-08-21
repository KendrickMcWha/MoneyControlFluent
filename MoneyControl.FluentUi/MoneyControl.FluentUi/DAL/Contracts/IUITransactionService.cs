namespace MoneyControl.FluentUi.DAL;

public interface IUITransactionService
{
    Task<List<Transaction>> GetAllTransactions(TransactionParamPayload payload);
    Task<List<Account>> GetAllAccounts();
    Task<List<Category>> GetAllCategories();
}
