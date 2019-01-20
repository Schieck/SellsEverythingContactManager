using SEContactManager.ApplicationCore.Entity;
using SEContactManager.ApplicationCore.Interfaces.Repositories;
using SEContactManager.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SEContactManager.ApplicationCore.Services
{
    public class CustomerService : GenericService<Customer>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository repository) : base(repository)
        {
            _customerRepository = repository;
        }

        public IEnumerable<Customer> FindByLastPruchase(DateTime from, DateTime to)
        {
            return _customerRepository.FindByLastPruchase(from, to);
        }

        public IEnumerable<Customer> FindByLastPruchase(DateTime date)
        {
            return _customerRepository.FindByLastPruchase(date);
        }

        public IEnumerable<Customer> FindByName(string name)
        {
            return _customerRepository.FindByName(name);
        }

        public IEnumerable<Customer> FindByUser(ApplicationUser user)
        {
            return _customerRepository.FindByUser(user);
        }
    }
}
