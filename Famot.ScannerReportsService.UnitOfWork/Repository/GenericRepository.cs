using Famot.ScannerReportsService.UnitOfWork.SrsDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Famot.ScannerReportsService.UnitOfWork.Repository
{
    class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly SrsContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(SrsContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public void Delete(Func<TEntity, bool> where)
        {
            IQueryable<TEntity> objects = _dbSet.Where(where).AsQueryable();
            foreach (TEntity obj in objects)
            {
                _dbSet.Remove(obj);
            }
        }

        public void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public bool Exists(object primaryKey)
        {
            return _dbSet.Find(primaryKey) != null;
        }

        public IEnumerable<TEntity> Get()
        {
            IQueryable<TEntity> query = _dbSet;
            return query.ToList();
        }

        public TEntity Get(Func<TEntity, bool> where)
        {
            return _dbSet.FirstOrDefault(where);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity GetByID(object id)
        {
            return _dbSet.Find(id);
        }

        public TEntity GetFirst(Func<TEntity, bool> predicate)
        {
            return _dbSet.First(predicate);
        }

        public IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
        {
            return _dbSet.Where(where).ToList();
        }

        public IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where)
        {
            return _dbSet.Where(where).AsQueryable();
        }

        public TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return _dbSet.Single(predicate);
        }

        public IQueryable<TEntity> GetWithInclude(Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = _dbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
