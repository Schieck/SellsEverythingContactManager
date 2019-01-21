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
    public class RegionService : GenericService<Region>, IRegionService
    {
        private readonly IRegionRepository _RegionRepository;

        public RegionService(IRegionRepository repository) : base(repository)
        {
            _RegionRepository = repository;
        }
    }
}
