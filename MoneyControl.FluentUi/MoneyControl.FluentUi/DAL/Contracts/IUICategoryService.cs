namespace MoneyControl.FluentUi.DAL;

public interface IUICategoryService
{
    Task<List<Category>> GetAllCategories();
    Task<Result> SaveCategory(Category cat);

}
