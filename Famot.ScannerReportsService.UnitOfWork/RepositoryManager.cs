﻿using Famot.ScannerReportsService.UnitOfWork.Repository;
using Famot.ScannerReportsService.UnitOfWork.SrsDbContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Famot.ScannerReportsService.UnitOfWork
{
    public sealed class RepositoryManager : IDisposable
    {
        private readonly object _save = new object();
        private bool _disposed = false;
        private readonly SrsContext _context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public RepositoryManager(SrsContext context)
        {
            _context = context;
        }

        public IGenericRepository<TEntity> Repositories<TEntity>() where TEntity : class
        {
            lock (_save)
            {
                IGenericRepository<TEntity> repository;
                if (_repositories.Keys.Contains(typeof(TEntity)))
                {
                    return _repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
                }
                repository = new GenericRepository<TEntity>(_context);
                _repositories.Add(typeof(TEntity), repository);
                return repository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}
