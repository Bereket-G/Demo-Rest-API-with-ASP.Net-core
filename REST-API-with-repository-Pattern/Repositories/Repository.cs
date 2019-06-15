using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using REST_API_with_repository_Pattern.Models.Entities;

namespace REST_API_with_repository_Pattern.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ILogger<T> _logger;

        private readonly DbContext _context;
        private readonly DbSet<T> _entities;

        
        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        
        
        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _entities.UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _entities.RemoveRange(entities);
        }

        public int Count()
        {
            return _entities.Count();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public T GetSingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _entities.SingleOrDefault(predicate);
        }

        public T Get(int id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }
    }
}