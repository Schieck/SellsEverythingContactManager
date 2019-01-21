using Microsoft.AspNetCore.Identity;
using SEContactManager.ApplicationCore.Entity;
using SEContactManager.ApplicationCore.Interfaces.Repositories;
using SEContactManager.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace SEContactManager.ApplicationCore.Services
{
    public class CustomerService : GenericService<Customer>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerService(ICustomerRepository repository, UserManager<ApplicationUser> userManager) : base(repository)
        {            
            _customerRepository = repository;
            _userManager = userManager;
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

        public IEnumerable<Customer> FindByLastPruchase(DateTime from, DateTime to, ClaimsPrincipal claimsPrincipal)
        {
            var userId = _userManager.GetUserId(claimsPrincipal);
            ApplicationUser owner = new ApplicationUser()
            {
                Id = userId
            };
            
            return _customerRepository.FindByLastPruchase(from, to, owner);
        }

        public IEnumerable<Customer> FindByLastPruchase(DateTime date, ClaimsPrincipal claimsPrincipal)
        {
            var userId = _userManager.GetUserId(claimsPrincipal);
            ApplicationUser owner = new ApplicationUser()
            {
                Id = userId
            };

            return _customerRepository.FindByLastPruchase(date, owner);
        }

        public IEnumerable<Customer> FindByName(string name, ClaimsPrincipal claimsPrincipal)
        {
            var userId = _userManager.GetUserId(claimsPrincipal);
            ApplicationUser owner = new ApplicationUser()
            {
                Id = userId
            };

            return _customerRepository.FindByName(name, owner);
        }

        public IEnumerable<Customer> FindByUser(ApplicationUser user, ClaimsPrincipal claimsPrincipal)
        {        
            return _customerRepository.FindByUser(user);
        }      

        public Customer Add(Customer entity, ClaimsPrincipal claimsPrincipal)
        {
            var userId = _userManager.GetUserId(claimsPrincipal);
            
            entity.OwnerId = userId;

            return _customerRepository.Add(entity);
        }

        public void Update(Customer entity, ClaimsPrincipal claimsPrincipal)
        {            

            _customerRepository.Update(entity);
        }

        public void Remove(Customer entity, ClaimsPrincipal claimsPrincipal)
        {            
            _customerRepository.Remove(entity);
        }

        public Customer FindById(int id, ClaimsPrincipal claimsPrincipal)
        {
            var userId = _userManager.GetUserId(claimsPrincipal);
            ApplicationUser owner = new ApplicationUser()
            {
                Id = userId
            };

           return _customerRepository.FindById(id, owner);
        }

        public IEnumerable<Customer> Find(Expression<Func<Customer, bool>> filter, ClaimsPrincipal claimsPrincipal)
        {
            var userId = _userManager.GetUserId(claimsPrincipal);
            ApplicationUser owner = new ApplicationUser()
            {
                Id = userId
            };

           return _customerRepository.Find(filter, owner);
        }

        public IEnumerable<Customer> FindAll(ClaimsPrincipal claimsPrincipal)
        {

            var userId = _userManager.GetUserId(claimsPrincipal);
            ApplicationUser owner = new ApplicationUser()
            {
                Id = userId
            };

            return _customerRepository.FindAll(owner);
        }

        override public IEnumerable<Customer> FindAll()
        {            
            return _customerRepository.FindAll();
        }
    }
}
