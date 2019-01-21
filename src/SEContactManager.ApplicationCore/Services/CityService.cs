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
    public class CityService : GenericService<City>, ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository repository) : base(repository)
        {
            _cityRepository = repository;
        }
    }
}
