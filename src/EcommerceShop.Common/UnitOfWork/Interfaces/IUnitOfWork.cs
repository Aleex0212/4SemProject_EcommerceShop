using System.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceShop.Common.UnitOfWork.Interfaces
{
    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        void Commit();
        void Rollback();
        void BeginTransaction(IsolationLevel isolationLevel);
        TContext Context { get; }
    }
}