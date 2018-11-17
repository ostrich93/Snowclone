using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Snowclone.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _entities;

        public Repository(DbContext context)
        {
            _entities = context.Set<T>();
        }

        public T GetById(int id) {
            return _entities.Find(id);
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _entities.RemoveRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate = null)
        {
            return _entities.Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }

        public void Insert(T entity)
        {
            _entities.Add(entity);
        }

        public void InsertRange(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
        }
    }
}
