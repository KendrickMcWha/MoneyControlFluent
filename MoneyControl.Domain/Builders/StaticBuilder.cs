using MoneyControl.Domain.Data.Entities;

namespace MoneyControl.Domain.Builders;
public static class StaticBuilder
{

    public static Transaction BuildTransactionFromEntity(TransactionEntity entity)
    {
        Transaction trans = new Transaction()
        {
            Id = entity.Id,
            AccountId = entity.AccountId,
            CategoryId = entity.CategoryId,
            TotalAmount = entity.TotalAmount,
            TransType = entity.TransType,
            //TransDate = DateOnly.ParseExact(entity.TransDate, "yyyyMMdd", CultureInfo.InvariantCulture),
            //PostDate = DateOnly.ParseExact(entity.PostDate, "yyyyMMdd", CultureInfo.InvariantCulture),
            //BudgetDate = DateOnly.ParseExact(entity.BudgetDate, "yyyyMMdd", CultureInfo.InvariantCulture),
            TransDate = entity.TransDate,
            BudgetDate = entity.BudgetDate,
          //  RegularPaymentId = entity.RegularPaymentId ?? -1,
            Details = entity.Details,
            Reference = entity.Reference,
        };

        return trans;
    }

    public static Transaction BuildTransactionFromEntity(TransactionEntity entity, object allSubTrans)
    {
        Transaction trans = new Transaction()
        {
            Id = entity.Id,
            AccountId = entity.AccountId,
            CategoryId = entity.CategoryId,
            TotalAmount = entity.TotalAmount,
            TransType = entity.TransType,
            //TransDate = DateOnly.ParseExact( entity.TransDate, "yyyyMMdd", CultureInfo.InvariantCulture),
            //PostDate = DateOnly.ParseExact(entity.PostDate, "yyyyMMdd", CultureInfo.InvariantCulture),
            //BudgetDate = DateOnly.ParseExact(entity.BudgetDate, "yyyyMMdd", CultureInfo.InvariantCulture),
            RegularPaymentId = entity.RegularPaymentId,
            Details = entity.Details,
            Reference = entity.Reference,
        };

        return trans;
    }


    public static Account BuildAccountFromEntity(AccountEntity entity)
    {
        Account account = new Account()
        {
            Id = entity.Id,
            Name = entity.Name,
            Type = entity.Type,
            Balance = entity.Balance,
            StartingBalance = entity.StartingBalance
        };
        return account;
    }
    public static Category BuildCategoryFromEntity(CategoryEntity entity)
    {
        Category category = new Category()
        {
            Id = entity.Id,
            Name = entity.Name,
            Type = entity.Type
        };
        return category;
    }
    public static Payee BuildPayeeFromEntity(PayeeEntity entity)
    {
        Payee payee = new Payee()
        {
            Id = entity.Id,
            Name = entity.Name,
            DisplayName = entity.DisplayName,
            DefaultCategoryId = entity.DefaultCategoryId
        };
        return payee;
    }
    public static TransactionType BuildTransactionTypeFromEntity(TransactionType entity)
    {
        TransactionType transactionType = new TransactionType()
        {
            Id = entity.Id,
            Description = entity.Description,
            Value = entity.Value
        };
        return transactionType;
    }
}
