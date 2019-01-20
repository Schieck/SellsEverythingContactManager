using SEContactManager.ApplicationCore.Entity;
using SEContactManager.ApplicationCore.Interfaces.Repositories;
using SEContactManager.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SEContactManager.Infrastructure.Repository
{
    public class CustomerRepository : EFRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<Customer> FindByLastPruchase(DateTime from, DateTime to)
        {
            return Find(model => model.LastPurchase > from && model.LastPurchase < to);
        }

        public IEnumerable<Customer> FindByLastPruchase(DateTime date)
        {
            return Find(model => model.LastPurchase == date);
        }

        public IEnumerable<Customer> FindByName(string name)
        {
            return Find(model => model.Name.Contains(name));
        }

        public IEnumerable<Customer> FindByUser(ApplicationUser user)
        {
            return Find(model => model.User == user);
        }
    }
}
