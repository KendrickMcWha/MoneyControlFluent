namespace MoneyControl.FluentUi.DAL;

public interface IUIPayeeService
{
    Task<List<Payee>> GetAllPayees();
    Task<List<Payee>> GetAllPayeesWithDefCat();
    Task<List<PayeeDetails>> GetAllPayeeDetails();
    Task<Result> SavePayee(Payee payee);
    Task<Result> SavePayeeDetails(PayeeDetails details);
    Task<Result> DeletePayee(Payee payee);

}
