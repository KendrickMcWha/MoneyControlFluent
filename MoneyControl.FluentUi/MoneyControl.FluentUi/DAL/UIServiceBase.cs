﻿using Microsoft.EntityFrameworkCore;
using MoneyControl.Domain.Data.Context;

namespace MoneyControl.FluentUi.DAL;

public abstract class UIServiceBase
{
    public IDbContextFactory<SqliteDbContext> MyDbContextFactory { get; set; } = null;
    protected SqliteDbContext MyDbContext { get; set; }
    protected Result SuccessResult = new Result(true, string.Empty);
    protected SqliteDbContext NewDbContext => CreateDbContext();

    public UIServiceBase(IDbContextFactory<SqliteDbContext> factory)
    {  
        MyDbContextFactory = factory;
        CreateDbContext();
    }
    public SqliteDbContext CreateDbContext()
    {
        MyDbContext = MyDbContextFactory.CreateDbContext();        
        return MyDbContext;
    }
}
