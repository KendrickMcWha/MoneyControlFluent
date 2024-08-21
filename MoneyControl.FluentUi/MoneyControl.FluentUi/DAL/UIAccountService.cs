using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Data.Context;
using MoneyControl.Domain.Models;
using MoneyControl.Domain.Services;

namespace MoneyControl.FluentUi.DAL;

public class UIAccountService : UIServiceBase, IUIAccountService, IDisposable
{

    public UIAccountService(IDbContextFactory<SqliteDbContext> factory) : base(factory)
    {        
    }
    public void Dispose() => MyDbContext?.Dispose();

    public async Task<List<Account>> GetAllAccounts()
    {
        using AccountService service = new(MyDbContext);
        return await service.GetAllAccounts();
    }
}
