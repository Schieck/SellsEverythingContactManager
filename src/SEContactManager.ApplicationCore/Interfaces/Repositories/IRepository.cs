using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SEContactManager.ApplicationCore.Interfaces.Repositories
{
    public interface IRepository<Type> where Type : class
    {
        Type Add(Type entity);
        void Update(Type entity);
        void Remove(Type entity);
        IEnumerable<Type> FindAll();
        IEnumerable<Type> FindById(int id);
        IEnumerable<Type> Find(Expression<Func<Type, bool>> filter);
    }
}
