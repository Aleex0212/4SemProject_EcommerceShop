using System.Data;
using EcommerceShop.Common.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProductService.Persistence.Context;

public class UnitOfWork : IUnitOfWork
{
    private readonly ProductDbContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(ProductDbContext context)
    {
        _context = context;
    }

    public void Commit()
    {
        _context.SaveChanges();
        _transaction?.Commit();
        _transaction?.Dispose();
    }

    public void Rollback()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
    }

    public void BeginTransaction(IsolationLevel isolationLevel)
    {
        _transaction = _context.Database.CurrentTransaction ??
                       _context.Database.BeginTransaction(isolationLevel);
    }
}