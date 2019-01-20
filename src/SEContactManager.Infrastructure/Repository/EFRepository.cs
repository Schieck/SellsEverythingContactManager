using SEContactManager.ApplicationCore.Interfaces.Repositories;
using SEContactManager.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SEContactManager.Infrastructure.Repository
{
    public class EFRepository<Type> : IRepository<Type> where Type : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public EFRepository(ApplicationDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public virtual Type Add(Type entity)
        {
            _dbContext.Set<Type>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public virtual IEnumerable<Type> Find(Expression<Func<Type, bool>> filter)
        {
          return  _dbContext.Set<Type>().Where(filter).AsEnumerable();
        }

        public virtual IEnumerable<Type> FindAll()
        {
            return _dbContext.Set<Type>().AsEnumerable();
        }

        public virtual Type FindById(int id)
        {
            return _dbContext.Set<Type>().Find(id);
        }

        public virtual void Remove(Type entity)
        {
            _dbContext.Set<Type>().Remove(entity);
        }

        public void Update(Type entity)
        {
            _dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChangesAsync();

        }
    }
}
