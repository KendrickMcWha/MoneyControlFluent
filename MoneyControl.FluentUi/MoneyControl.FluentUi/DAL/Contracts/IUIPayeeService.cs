namespace MoneyControl.FluentUi.DAL;

public interface IUIPayeeService
{
    Task<List<Payee>> GetAllPayees();
    Task<List<Payee>> GetAllPayeesWithDefCat();
    Task<List<PayeeDetails>> GetAllPayeeDetails();
    Task<List<Category>> GetAllCategories();
    Task<List<Transaction>> GetAllTransactions(TransactionParamPayload payload);

    Task<Result> SavePayee(Payee payee);
    Task<Result> SavePayeeDetails(PayeeDetails details);
    Task<Result> DeletePayee(Payee payee);
    Task<Result> SaveDetailsMakePayee(Payee payee, PayeeDetails details);
}
