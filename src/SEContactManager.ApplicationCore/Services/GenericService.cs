using SEContactManager.ApplicationCore.Interfaces.Repositories;
using SEContactManager.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SEContactManager.ApplicationCore.Services
{
    public class GenericService<Type> : IService<Type> where Type : class
    {
        private readonly IRepository<Type> _repository; 

        public GenericService(IRepository<Type> repository) 
        {
            _repository = repository;
        }

        public virtual Type Add(Type entity)
        {
            return _repository.Add(entity);
        }

        public virtual IEnumerable<Type> Find(Expression<Func<Type, bool>> filter)
        {
            return _repository.Find(filter);
        }

        public virtual IEnumerable<Type> FindAll()
        {
            return _repository.FindAll();
        }

        public virtual Type FindById(int id)
        {
            return _repository.FindById(id);
        }

        public void Remove(Type entity)
        {
            _repository.Remove(entity);
        }

        public void Update(Type entity)
        {
            _repository.Update(entity);
        }
    }
}
