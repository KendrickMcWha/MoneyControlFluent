using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Data.Context;
using MoneyControl.Domain.Services;
using MoneyControl.FluentUi.DAL.Contracts;

namespace MoneyControl.FluentUi.DAL;

public class UIWorkspaceService : UIServiceBase, IDisposable, IUIWorkspaceService
{
    public UIWorkspaceService(IDbContextFactory<SqliteDbContext> factory) : base(factory) { }
    public void Dispose() => MyDbContext.Dispose();

    public void MakeTransDetails()
    {
        TransactionService transService = new TransactionService(MyDbContext);

    }
}
