using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Extensions;
using MoneyControl.Domain.Data.Context;
using MoneyControl.Domain.Models;
using MoneyControl.Domain.Services;
using MoneyControl.FluentUi.Components.Global;
using Serilog;

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
    private DateTime? _selectedDateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month , 1).AddMonths(-2); 
    private DateTime? _selectedDateTo = DateTime.Now;
    private Account SelectedAccount { get { return _selectedAccount; } set { _selectedAccount = value; ReloadData(); } }
    private Category SelectedCategory { get { return _selectedCategory; } set { _selectedCategory = value; ReloadData(); } }
    
    private IEnumerable<Category> AllSelectedCategories { get; set; }
    private DateTime? SelectedDateFrom { get { return _selectedDateFrom; } set { _selectedDateFrom = value; ReloadData(); } }
    private DateTime? SelectedDateTo { get { return _selectedDateTo; } set { _selectedDateTo = value; ReloadData(); } }

    private Transaction MySetTransaction { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
           // DateTime preSelectDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
           // _selectedDateFrom = preSelectDate.AddMonths(-1);
            await GetAccounts();
            await GetCategories();
            await GetTransactions();
        }
        catch (Exception ex)
        {
            MyGlobalMsg.ProcessError(ex);
        }
    }

    private string TransactionTextStyle(Transaction trans)
    {
        if (trans is null) return string.Empty;

        if (trans.TransType > 0) return "font-weight:bold;color:Red";
        return "font-weight:bold;color:Black";
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
        AllCategories = AllCategories.OrderBy(x => x.Name).ToList();
        var cat = AllCategories.FirstOrDefault(x => x.Name == "Unassigned");
        if (cat is not null)
        {
            AllCategories.Remove(cat);
            AllCategories.Insert(0, cat);
        }

        AllCategories.Insert(0, new Category() { Id = -1, Name = "All", Type = string.Empty });
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

    private void HandleDoubleClick(FluentDataGridRow<Transaction> row)
    {
        Log.Information($"Row Double Click {row.RowIndex}");
        if (row.Item is Transaction trans)
        {
            Log.Information($"{trans.TotalAmount} {trans.CategoryName} {trans.Details}");
            ClearDoubleClick();
            MySetTransaction = trans;
            
        }
    }

    private void ClearDoubleClick()
    {
        MySetTransaction = null;
        StateHasChanged();
    }
    private void UpdateTrans()
    {
        StateHasChanged();
    }

}
