using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoneyControl.Domain.Data.Entities;
using System.Globalization;

namespace MoneyControl.Domain.Data.Configuration;
public class TransactionEntityConfiguration : IEntityTypeConfiguration<TransactionEntity>
{
    public void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("TRANSACTIONS");
        //builder.Property(e => e.TransDate)
        //    .HasConversion(new DateOnlyToStringConverter());
        //builder.Property(e => e.PostDate)
        //            .HasConversion(new DateOnlyToStringConverter());
        //builder.Property(e => e.BudgetDate)
        //            .HasConversion(new DateOnlyToStringConverter());

    }
}

public class SubTransactionEntityConfiguration : IEntityTypeConfiguration<SubTransactionEntity>
{
    public void Configure(EntityTypeBuilder<SubTransactionEntity> builder) 
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("SUBTRANSACTION");
        
    }
}

public class AccountEntityConfiguration : IEntityTypeConfiguration<AccountEntity>
{
    public void Configure(EntityTypeBuilder<AccountEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable($"ACCOUNT");
    }
}
public class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable($"CATEGORY");
    }
}

public class PayeeEntityConfiguration : IEntityTypeConfiguration<PayeeEntity>
{
    public void Configure(EntityTypeBuilder<PayeeEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable($"PAYEE");
    }
}

public class PayeeDetailsEntityConfiguration : IEntityTypeConfiguration<PayeeDetailsEntity>
{
    public void Configure(EntityTypeBuilder<PayeeDetailsEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable($"PAYEEDETAIL");
    }
}


public class TransactionTypeEntityConfiguration : IEntityTypeConfiguration<TransactionTypeEntity>
{
    public void Configure(EntityTypeBuilder<TransactionTypeEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable($"TRANSACTIONTYPE");
    }
}

