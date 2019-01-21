using SEContactManager.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SEContactManager.ApplicationCore.Interfaces.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> FindByUser(ApplicationUser user);
        IEnumerable<Customer> FindByName(string name);
        IEnumerable<Customer> FindByLastPruchase(DateTime from, DateTime to);
        IEnumerable<Customer> FindByLastPruchase(DateTime date);

        IEnumerable<Customer> FindByName(string name, ApplicationUser owner);
        IEnumerable<Customer> FindByLastPruchase(DateTime from, DateTime to, ApplicationUser owner);
        IEnumerable<Customer> FindByLastPruchase(DateTime date, ApplicationUser owner);

        IEnumerable<Customer> FindAll(ApplicationUser owner);
        Customer FindById(int id, ApplicationUser owner);
        IEnumerable<Customer> Find(Expression<Func<Customer, bool>> filter, ApplicationUser owner);

        IEnumerable<Customer> FindBySearch(CustomerSearch customerSearch, ApplicationUser owner);
        IEnumerable<Customer> FindBySearch(CustomerSearch customerSearch);
    }
}
