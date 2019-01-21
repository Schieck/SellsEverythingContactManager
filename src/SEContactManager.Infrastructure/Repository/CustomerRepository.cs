using Microsoft.EntityFrameworkCore;
using SEContactManager.ApplicationCore.Entity;
using SEContactManager.ApplicationCore.Interfaces.Repositories;
using SEContactManager.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SEContactManager.Infrastructure.Repository
{
    public class CustomerRepository : EFRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<Customer> Find(Expression<Func<Customer, bool>> filter, ApplicationUser owner)
        {

            Expression<Func<Customer, bool>> ownerFilter = model => model.Owner.Id == owner.Id;
            var finalFilter = Expression.Lambda<Func<Customer, bool>>(Expression.And(ownerFilter, filter));

            return Find(finalFilter);
        }

        public IEnumerable<Customer> FindAll(ApplicationUser owner)
        {
            Expression<Func<Customer, bool>> ownerFilter = model => model.Owner.Id == owner.Id;

            return Find(ownerFilter);
        }

        public Customer FindById(int id, ApplicationUser owner)
        {
            Expression<Func<Customer, bool>> filter = model => model.Owner.Id == owner.Id && model.Id == id;

            return _dbContext.Customers.Include(model => model.Owner).Where(filter).FirstOrDefault();
        }

        public IEnumerable<Customer> FindByLastPruchase(DateTime from, DateTime to)
        {
            return Find(model => model.LastPurchase > from && model.LastPurchase < to);
        }

        public IEnumerable<Customer> FindByLastPruchase(DateTime date)
        {
            return Find(model => model.LastPurchase == date);
        }

        public IEnumerable<Customer> FindByLastPruchase(DateTime from, DateTime to, ApplicationUser owner)
        {
            Expression<Func<Customer, bool>> filter = model => model.Owner.Id == owner.Id &&
                                                                    model.LastPurchase > from  &&
                                                                    model.LastPurchase < to;           

            return Find(filter);
        }

        public IEnumerable<Customer> FindByLastPruchase(DateTime date, ApplicationUser owner)
        {
            Expression<Func<Customer, bool>> filter = model => model.Owner.Id == owner.Id &&
                                                               model.LastPurchase == date;

            return Find(filter);
        }

        public IEnumerable<Customer> FindByName(string name)
        {
            return Find(model => model.Name.Contains(name));
        }

        public IEnumerable<Customer> FindByName(string name, ApplicationUser owner)
        {
            Expression<Func<Customer, bool>> filter = model => model.Owner.Id == owner.Id &&
                                                               model.Name.Contains(name);

            return Find(filter);
        }

        public IEnumerable<Customer> FindByUser(ApplicationUser user)
        {
            return Find(model => model.Owner.Id == user.Id);
        }

        override public IEnumerable<Customer> FindAll()
        {
            return _dbContext.Customers
                .Include(model => model.Owner);
        }

        override public Customer FindById(int id)
        {
            return _dbContext.Customers.Include(model => model.Owner).Where(model => model.Id == id).FirstOrDefault();
        }

        override public void Update(Customer entity)
        {
            if (entity.OwnerId == null)
                entity.OwnerId = _dbContext.Customers.AsNoTracking().Where(model => model.Id == entity.Id).FirstOrDefault().OwnerId;

            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChangesAsync();
        }
    }

}
