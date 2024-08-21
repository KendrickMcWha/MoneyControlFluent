namespace MoneyControl.FluentUi.DAL;

public interface IUICategoryService
{
    Task<List<Category>> GetAllCategories();
    Task<List<TransactionType>> GetTransactionTypes();
    Task<Result> SaveCategory(Category cat);

}
