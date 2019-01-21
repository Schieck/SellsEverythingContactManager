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
    public class CityRepository : EFRepository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        override public IEnumerable<City> FindAll()
        {
            return _dbContext.Set<City>().Include(model => model.Region).AsEnumerable();
        }
    }
}
