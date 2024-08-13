using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using MoneyControl.Domain.Data.Context;
using MoneyControl.Domain.Models;
using MoneyControl.Domain.Services;
using MoneyControl.FluentUi.Components.Global;

namespace MoneyControl.FluentUi.Components.Pages;

public partial class TransactionsPage : ComponentBase
{
    [CascadingParameter] GlobalMessage MyGlobalMsg { get; set; }
    [Inject] public IDbContextFactory<SqliteDbContext> MyDbContextFactory { get; set; } = null;
    public IQueryable<Transaction> AllTransactions { get; set; }
    PaginationState pagination = new PaginationState { ItemsPerPage = 25 };

    protected override async Task OnInitializedAsync()
    {
        try
        {
            using TransactionService service = new(MyDbContextFactory.CreateDbContext());
            var allTrans = await service.GetAllTransactions();
            AllTransactions = allTrans.AsQueryable();

        }
        catch (Exception ex)
        {
            MyGlobalMsg.ProcessError(ex);
        }
    }

}
