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
    public class RegionRepository : EFRepository<Region>, IRegionRepository
    {
        public RegionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
       
    }
}
