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
    }
}
