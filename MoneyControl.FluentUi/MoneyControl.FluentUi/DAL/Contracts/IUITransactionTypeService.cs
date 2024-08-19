namespace MoneyControl.FluentUi.DAL;

public interface IUITransactionTypeService
{
    Task<List<TransactionType>> GetAllTransTypes();
}
