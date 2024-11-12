using Microsoft.EntityFrameworkCore.Storage;
using OrderService.Application.Interfaces;
using OrderService.Persistence.Context;
using System.Data;

namespace OrderService.Persistence.Repositories
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly OrderContext _db;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(OrderContext db)
    {
      _db = db;
    }
    public void BeginTransaction()
    {
      _transaction = _db.Database.CurrentTransaction ?? _db.Database.BeginTransaction();
    }
    public void Commit()
    {
      _db.SaveChanges();
      if (_transaction == null) throw new Exception("Transaction not set");
      _transaction.Commit();
      _transaction.Dispose();
    }
    public void Rollback()
    {
      if (_transaction == null) throw new Exception("Transaction not set");
      _transaction.Rollback();
      _transaction.Dispose();
      _db.Dispose();
    }
  }
}
