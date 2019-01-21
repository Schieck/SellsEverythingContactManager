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

            return _dbContext.Customer.Include(model => model.Owner)
                                      .Include(model => model.City)
                                      .Include(model => model.City.Region)
                                      .Where(finalFilter);
        }

        public IEnumerable<Customer> FindAll(ApplicationUser owner)
        {
            Expression<Func<Customer, bool>> ownerFilter = model => model.Owner.Id == owner.Id;

            return _dbContext.Customer.Include(model => model.Owner)
                                      .Include(model => model.City)
                                      .Include(model => model.City.Region)
                                      .Where(ownerFilter);
        }

        public Customer FindById(int id, ApplicationUser owner)
        {
            Expression<Func<Customer, bool>> filter = model => model.Owner.Id == owner.Id && model.Id == id;

            return _dbContext.Customer.Include(model => model.Owner)
                                      .Include(model => model.City)
                                      .Include(model => model.City.Region)
                                      .Where(filter).FirstOrDefault();
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
                                                                    model.LastPurchase > from &&
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
            return _dbContext.Customer
                .Include(model => model.Owner)
                .Include(model => model.City)
                .Include(model => model.City.Region);
        }

        override public Customer FindById(int id)
        {
            return _dbContext.Customer.Include(model => model.Owner).Where(model => model.Id == id).FirstOrDefault();
        }

        override public void Update(Customer entity)
        {
            if (entity.OwnerId == null)
                entity.OwnerId = _dbContext.Customer.AsNoTracking().Where(model => model.Id == entity.Id).FirstOrDefault().OwnerId;

            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChangesAsync();
        }

        public IEnumerable<Customer> FindBySearch(CustomerSearch customerSearch, ApplicationUser owner)
        {
            var result = _dbContext.Customer.AsQueryable().Where(model => model.OwnerId == owner.Id && model.Owner.Id == owner.Id);

            if (!string.IsNullOrEmpty(customerSearch.Name))
                result = result.Where(x => x.Name.Contains(customerSearch.Name));
            if (customerSearch.Gender != null)
                result = result.Where(x => x.Gender == customerSearch.Gender);
            if (customerSearch.Classification != null)
                result = result.Where(x => x.Classification == customerSearch.Classification);
            if (customerSearch.CityId != null)
                result = result.Where(x => x.City.Id == customerSearch.CityId);
            if (customerSearch.LastPurchase != null)
                result = result.Where(x => x.LastPurchase >= customerSearch.LastPurchase);
            if (customerSearch.Until != null)
                result = result.Where(x => x.LastPurchase <= customerSearch.Until);
            if (customerSearch.RegionId != null)
                if (_dbContext.City.Find(customerSearch.CityId).RegionId != customerSearch.RegionId) { }
                else
                {
                    result = result.Where(x => x.City.RegionId == customerSearch.RegionId);
                }


            result
                .Include(model => model.Owner)
                .Include(model => model.City)
                .Include(model => model.City.Region);
            return result;
        }

        public IEnumerable<Customer> FindBySearch(CustomerSearch customerSearch)
        {

            var result = _dbContext.Customer.AsQueryable();

            if (!string.IsNullOrEmpty(customerSearch.Name))
                result = result.Where(x => x.Name.Contains(customerSearch.Name));
            if (customerSearch.Gender != null)
                result = result.Where(x => x.Gender == customerSearch.Gender);
            if (customerSearch.Classification != null)
                result = result.Where(x => x.Classification == customerSearch.Classification);
            if (customerSearch.CityId != null)
                result = result.Where(x => x.City.Id == customerSearch.CityId);
            if (customerSearch.LastPurchase != null)
                result = result.Where(x => x.LastPurchase >= customerSearch.LastPurchase);
            if (customerSearch.Until != null)
                result = result.Where(x => x.LastPurchase <= customerSearch.Until);
            if (customerSearch.RegionId != null)
                if (_dbContext.City.Find(customerSearch.CityId).RegionId != customerSearch.RegionId)
                {
                    result = result.Where(x => x.City.RegionId == customerSearch.RegionId || x.CityId == customerSearch.CityId);
                }
                else
                {
                    result = result.Where(x => x.City.RegionId == customerSearch.RegionId);
                }
            if (customerSearch.SellerId != null)
                result = result.Where(x => x.OwnerId == customerSearch.SellerId);

            result
                .Include(model => model.Owner)
                .Include(model => model.City)
                .Include(model => model.City.Region);
            return result;
        }
    }

}
