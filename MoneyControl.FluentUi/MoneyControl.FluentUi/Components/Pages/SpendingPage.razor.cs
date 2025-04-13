using ApexCharts;
using Microsoft.AspNetCore.Components;
using MoneyControl.FluentUi.Components.Global;

namespace MoneyControl.FluentUi.Components.Pages;

public partial class SpendingPage : ComponentBase
{
    [CascadingParameter] GlobalMessage MyGlobalMsg { get; set; }
    [Inject] public IUITransactionService MyTransactionService { get; set; }
    private ApexChart<CategoryTransSpend> ChartPie;
    private ApexChart<CategoryTransSpend> ChartBar;
    private List<TransactionMonth> AllTransMonths { get; set; } = new();
    private List<Category> AllCategories { get; set; } = new();
    DateTime? SelectedDateFrom { get; set; }
    DateTime? SelectedDateTo { get; set; }
    private string ActiveTabId;
    private int TotalTransactions { get; set; }
    private decimal TotalSpending { get; set; }

    protected override async Task OnInitializedAsync()
    {
        SelectedDateTo = DateTime.Now.AddDays(-DateTime.Now.Day + 1).AddMonths(-1);
        SelectedDateFrom = DateTime.Now.AddDays(-DateTime.Now.Day + 1).AddMonths(-1);
        await GetCategories();
    }

    private async Task GetCategories()
    {
        AllCategories = await MyTransactionService.GetAllCategories();
    }

    private async void ReloadData()
    {
        DateTime tmp = (DateTime)SelectedDateTo;

        tmp = new DateTime(tmp.Year, tmp.Month, DateTime.DaysInMonth(tmp.Year, tmp.Month));

        TransactionParamPayload payload = new(0, -1,
                                                DateOnly.FromDateTime((DateTime)SelectedDateFrom),
                                                DateOnly.FromDateTime(tmp)
                                                );
        var allTrans = await MyTransactionService.GetAllTransactions(payload);
        allTrans = allTrans.Where(x => x.TransType == -1).ToList();
        AllTransMonths.Clear();
        foreach (var trans in allTrans)
        {
            DateOnly transMonthDate = new DateOnly(trans.BudgetDate.Year, trans.BudgetDate.Month, 1);

            int type = trans.TransType < 0 ? -1 : 1;

            TransactionMonth transactionMonth = AllTransMonths.Find(x => x.MonthDate == transMonthDate);
            if (transactionMonth is null)
            {
                AllTransMonths.Add(transactionMonth = new(transMonthDate));
            }
            transactionMonth.AllTransactions.Add(trans);
        }

        AllTransMonths.ForEach(x => x.BuildBalances());
        AllTransMonths.ForEach(x => x.BuildCategorySpends(AllCategories));

        if (ChartPie is not null)
        {
            await ChartPie?.RenderAsync();
            await ChartBar?.RenderAsync();
        }

        TotalTransactions = 0;
        TotalSpending = 0;

        AllTransMonths.ForEach(x => TotalTransactions += x.AllTransactions.Count);
        AllTransMonths.ForEach(x => TotalSpending += x.TotalDebit);
    }
}
