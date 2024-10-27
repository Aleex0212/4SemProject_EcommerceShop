﻿using System.Data;
using EcommerceShop.Common.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProductService.Persistence.Context;

namespace ProductService.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductDbContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(ProductDbContext context, IDbContextTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
                _transaction?.Commit();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            _transaction = _context.Database.CurrentTransaction ?? _context.Database.BeginTransaction(isolationLevel);
        }
    }
}