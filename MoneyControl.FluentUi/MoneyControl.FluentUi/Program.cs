using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using MoneyControl.Domain.Data.Context;
using MoneyControl.Domain.Extension;
using MoneyControl.FluentUi.Client.Pages;
using MoneyControl.FluentUi.Components;
using MoneyControl.FluentUi.DAL;
using MoneyControl.FluentUi.Utils;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddFluentUIComponents();

builder.Services.AddDomain();

builder.Services.AddDbContextFactory<SqliteDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqLiteConnection"))
    );

builder.Services.AddTransient<IUIAccountService, UIAccountService>();
builder.Services.AddTransient<IUICategoryService, UICategoryService>();
builder.Services.AddTransient<IUIPayeeService, UIPayeeService>();
builder.Services.AddTransient<IUITransactionService, UITransactionService>();
builder.Services.AddTransient<IUITransactionTypeService, UITransactionTypeService>();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(theme: SerilogTheme.Colored)
    .WriteTo.Debug()
    .MinimumLevel.Debug()
    .CreateLogger();
Log.Information("Money Control Application Startup");

// Migrations - Open package manager console.
// Set Default project to app project. Set current directory where the startup project is.
// Build a migration
// Add-Migration MigrationNameHere -StartupProject MoneyControl.FluentUi -Project MoneyControl.Domain
// Update a migration
// Update-Database -StartupProject MoneyControl.FluentUi -Project MoneyControl.Domain


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MoneyControl.FluentUi.Client._Imports).Assembly);

app.Run();
