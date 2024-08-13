using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Data.Configuration;
using MoneyControl.Domain.Data.Entities;
namespace MoneyControl.Domain.Data.Context;
public class SqliteDbContext : DbContext
{
    public DbSet<TransactionEntity> AllTransactions { get; set; }
    public DbSet<SubTransactionEntity> AllSubTransactions { get; set; }
    public DbSet<AccountEntity> AllAccounts { get; set; }
    public DbSet<CategoryEntity> AllCategories { get; set; }
    public DbSet<PayeeEntity> AllPayees { get; set; }
    public DbSet<PayeeDetailsEntity> AllPayeesDetails { get; set; }
    public DbSet<TransactionTypeEntity> AllTransactionTypes { get; set; }


    public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TransactionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new SubTransactionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AccountEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PayeeEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PayeeDetailsEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionTypeEntityConfiguration());

    }

}
