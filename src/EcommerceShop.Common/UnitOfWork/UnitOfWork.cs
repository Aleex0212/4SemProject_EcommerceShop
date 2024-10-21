using EcommerceShop.Common.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace EcommerceShop.Common.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public TContext Context => _context;

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
            _transaction = _context.Database.CurrentTransaction ?? _context.Database.BeginTransaction(isolationLevel);
        }
    }
}