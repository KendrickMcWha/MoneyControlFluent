using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Extensions;
using MoneyControl.Domain.Data.Context;
using MoneyControl.Domain.Models;
using MoneyControl.Domain.Services;
using MoneyControl.FluentUi.Components.Global;

namespace MoneyControl.FluentUi.Components.Pages;

public partial class TransactionsPage : ComponentBase
{
    [CascadingParameter] GlobalMessage MyGlobalMsg { get; set; }
    [Inject] public IUITransactionService MyTransactionService { get; set; }
    PaginationState pagination = new PaginationState { ItemsPerPage = 15 };
    
    private IQueryable<Transaction> AllTransactions { get; set; }
    private List<Account> AllAccounts { get; set; } = new();
    private List<Category> AllCategories { get; set; } = new();
    private Account _selectedAccount;
    private Category _selectedCategory;
    private DateTime? _selectedDateFrom = new DateTime(DateTime.Now.Year, 1, 1); 
    private DateTime? _selectedDateTo = DateTime.Now;
    private Account SelectedAccount { get { return _selectedAccount; } set { _selectedAccount = value; ReloadData(); } }
    private Category SelectedCategory { get { return _selectedCategory; } set { _selectedCategory = value; ReloadData(); } }
    private IEnumerable<Category> AllSelectedCategories { get; set; }
    private DateTime? SelectedDateFrom { get { return _selectedDateFrom; } set { _selectedDateFrom = value; ReloadData(); } }
    private DateTime? SelectedDateTo { get { return _selectedDateTo; } set { _selectedDateTo = value; ReloadData(); } }

    

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await GetAccounts();
            await GetCategories();
            await GetTransactions();
        }
        catch (Exception ex)
        {
            MyGlobalMsg.ProcessError(ex);
        }
    }

    private async Task GetAccounts()
    {
        AllAccounts = await MyTransactionService.GetAllAccounts();
        AllAccounts.Insert(0, new Account() { Id = 0, Name = "All", Type = string.Empty });
        _selectedAccount = AllAccounts[0];
    }
    private async Task GetCategories()
    {
        AllCategories = await MyTransactionService.GetAllCategories();
        AllCategories.Insert(0, new Category() { Id = 0, Name = "All", Type = string.Empty });
        _selectedCategory = AllCategories[0];
    }
    private async void ReloadData()
    {
        await GetTransactions();
    }
    private async Task GetTransactions()
    {
        var allTrans = await MyTransactionService.GetAllTransactions(new TransactionParamPayload(SelectedAccount.Id, 
                                                                                    SelectedCategory.Id, 
                                                                                    SelectedDateFrom.ToDateOnly(), 
                                                                                    SelectedDateTo.ToDateOnly())
                                                                                    );        
        AllTransactions = allTrans.AsQueryable();
    }

    

}
