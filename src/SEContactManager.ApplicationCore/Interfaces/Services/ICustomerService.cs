using SEContactManager.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace SEContactManager.ApplicationCore.Interfaces.Services
{
    public interface ICustomerService : IService<Customer>
    {
        IEnumerable<Customer> FindByUser(ApplicationUser user, ClaimsPrincipal claimsPrincipal);
        IEnumerable<Customer> FindByName(string name, ClaimsPrincipal claimsPrincipal);
        IEnumerable<Customer> FindByLastPruchase(DateTime from, DateTime to, ClaimsPrincipal claimsPrincipal);
        IEnumerable<Customer> FindByLastPruchase(DateTime date, ClaimsPrincipal claimsPrincipal);

        Customer Add(Customer entity, ClaimsPrincipal claimsPrincipal);
        void Update(Customer entity, ClaimsPrincipal claimsPrincipal);
        void Remove(Customer entity, ClaimsPrincipal claimsPrincipal);
        Customer FindById(int id, ClaimsPrincipal claimsPrincipal);
        IEnumerable<Customer> Find(Expression<Func<Customer, bool>> filter, ClaimsPrincipal claimsPrincipal);
        IEnumerable<Customer> FindAll(ClaimsPrincipal claimsPrincipal);

        IEnumerable<Customer> FindBySearch(CustomerSearch customerSearch, ClaimsPrincipal claimsPrincipal);
        IEnumerable<Customer> FindBySearch(CustomerSearch customerSearch);
    }
}
