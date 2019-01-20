using SEContactManager.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SEContactManager.ApplicationCore.Interfaces.Services
{
    public interface ICustomerService : IService<Customer>
    {
        IEnumerable<Customer> FindByUser(ApplicationUser user);
        IEnumerable<Customer> FindByName(string name);
        IEnumerable<Customer> FindByLastPruchase(DateTime from, DateTime to);
        IEnumerable<Customer> FindByLastPruchase(DateTime date);
    }
}
