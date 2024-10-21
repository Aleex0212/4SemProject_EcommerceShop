using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EcommerceShop.Common.UnitOfWork.Interfaces
{
    /// <summary>
    /// Interface for unit of work pattern.
    /// </summary>
    /// <typeparam name="TContext">The type of the DbContext.</typeparam>
    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Commits all changes made in the current unit of work.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rolls back all changes made in the current unit of work.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Begins a new transaction with the specified isolation level.
        /// </summary>
        /// <param name="isolationLevel">The isolation level for the transaction.</param>
        void BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// Gets the current database context.
        /// </summary>
        TContext Context { get; }
    }
}