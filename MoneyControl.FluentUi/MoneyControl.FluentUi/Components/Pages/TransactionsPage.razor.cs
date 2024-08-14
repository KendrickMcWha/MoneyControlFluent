using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Extensions;
using MoneyControl.Domain.Data.Context;
using MoneyControl.Domain.Models;
using MoneyControl.Domain.Services;
using MoneyControl.FluentUi.Components.Global;
using System.Runtime.CompilerServices;

namespace MoneyControl.FluentUi.Components.Pages;

public partial class TransactionsPage : ComponentBase
{
    [CascadingParameter] GlobalMessage MyGlobalMsg { get; set; }
    [Inject] public IDbContextFactory<SqliteDbContext> MyDbContextFactory { get; set; } = null;
    PaginationState pagination = new PaginationState { ItemsPerPage = 15 };
    
    private IQueryable<Transaction> AllTransactions { get; set; }
    private List<Account> AllAccounts { get; set; } = new();
    private List<Category> AllCategories { get; set; } = new();
    private Account SelectedAccount { get; set; }
    private Category SelectedCategory { get; set; }
    private IEnumerable<Category> AllSelectedCategories { get; set; } 
    private DateTime? SelectedDateFrom = Constants.DataStartDate.ToDateTime(new TimeOnly(0));
    private DateTime? SelectedDateTo = DateTime.Now;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await GetAccounts();
            await GetCategories();

        }
        catch (Exception ex)
        {
            MyGlobalMsg.ProcessError(ex);
        }
    }

    private async Task GetAccounts()
    {
        using AccountService service = new(MyDbContextFactory.CreateDbContext());
        AllAccounts = await service.GetAllAccounts();
        AllAccounts.Insert(0, new Account() { Id = 0, Name = "All", Type = string.Empty });
        SelectedAccount = AllAccounts[0];
    }
    private async Task GetCategories()
    {
        using CategoryService service = new(MyDbContextFactory.CreateDbContext());
        AllCategories = await service.GetAllCategories();
        AllCategories.Insert(0, new Category() { Id = 0, Name = "All", Type = string.Empty });
        SelectedCategory = AllCategories[0];
    }
    private async void GetTransactions()
    {
        using TransactionService service = new(MyDbContextFactory.CreateDbContext());

        var allTrans = await service.GetAllTransactions(new TransactionParamPayload(SelectedAccount.Id, 
                                                                                    SelectedCategory.Id, 
                                                                                    SelectedDateFrom.ToDateOnly(), 
                                                                                    SelectedDateTo.ToDateOnly())
                                                                                    );
        
        AllTransactions = allTrans.AsQueryable();
    }

    

}
