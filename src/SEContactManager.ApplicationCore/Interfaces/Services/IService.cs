using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SEContactManager.ApplicationCore.Interfaces.Services
{
    public interface IService<Type>
    {
        Type Add(Type entity);
        void Update(Type entity);
        void Remove(Type entity);
        IEnumerable<Type> FindAll();
        Type FindById(int id);
        IEnumerable<Type> Find(Expression<Func<Type, bool>> filter);
    }
}
