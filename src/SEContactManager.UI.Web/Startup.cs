using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SEContactManager.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using SEContactManager.ApplicationCore.Entity;
using SEContactManager.ApplicationCore.Interfaces.Repositories;
using SEContactManager.Infrastructure.Repository;
using SEContactManager.ApplicationCore.Interfaces.Services;
using SEContactManager.ApplicationCore.Services;

namespace SEContactManager.UI.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Identity/EF Configuration

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
            });

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrator"));
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/LogOut";
            });

            #endregion

            #region Dependency Injection Configuration

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IRegionService, RegionService>();

            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICityService, CityService>();

            #endregion Dependency Injection Configuration

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Customer}/{action=Index}/{id?}");
            });

            CreateRoles(serviceProvider).Wait();

            CreateRegions(serviceProvider);
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleTypes = Enum.GetNames(typeof(RoleTypes));
            IdentityResult roleResult;

            foreach (var roleName in roleTypes)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }


            //Here you could create a super user who will maintain the web app
            IList<DefaultUserModel> defaultUsers = new List<DefaultUserModel>();

            defaultUsers.Add(
                new DefaultUserModel()
                {
                    User = new ApplicationUser()
                    {
                        Email = Configuration["DefaultUsers:Administrator:Email"],
                        UserName = Configuration["DefaultUsers:Administrator:Email"]
                    },
                    Password = Configuration["DefaultUsers:Administrator:Password"],
                    RoleType = RoleTypes.Administrator
                }
            );

            defaultUsers.Add(
               new DefaultUserModel()
               {
                   User = new ApplicationUser()
                   {
                       Email = Configuration["DefaultUsers:Seller_1:Email"],
                       UserName = Configuration["DefaultUsers:Seller_1:Email"]
                   },
                   Password = Configuration["DefaultUsers:Seller_1:Password"],
                   RoleType = RoleTypes.Seller
               });

            defaultUsers.Add(
               new DefaultUserModel()
               {
                   User = new ApplicationUser()
                   {
                       Email = Configuration["DefaultUsers:Seller_2:Email"],
                       UserName = Configuration["DefaultUsers:Seller_2:Email"]
                   },
                   Password = Configuration["DefaultUsers:Seller_2:Password"],
                   RoleType = RoleTypes.Seller
               });

            foreach (var user in defaultUsers)
            {

                //Ensure you have these values in your appsettings.json file
                string userPWD = user.Password;
                var _user = await UserManager.FindByEmailAsync(user.User.Email);

                if (_user == null)
                {

                    var createPowerUser = await UserManager.CreateAsync(user.User, userPWD);
                    if (createPowerUser.Succeeded)
                    {


                        switch (user.RoleType)
                        {
                            case RoleTypes.Administrator:
                                await UserManager.AddToRoleAsync(user.User, Enum.GetName(typeof(RoleTypes), RoleTypes.Administrator));
                                break;
                            case RoleTypes.Seller:
                                await UserManager.AddToRoleAsync(user.User, Enum.GetName(typeof(RoleTypes), RoleTypes.Seller));
                                break;
                            default:
                                await UserManager.AddToRoleAsync(user.User, Enum.GetName(typeof(RoleTypes), RoleTypes.Seller));
                                break;
                        }
                    }
                }
            }
        }

        private void CreateRegions(IServiceProvider serviceProvider)
        {
            var _regionService = serviceProvider.GetRequiredService<IRegionService>();
            var _cityService = serviceProvider.GetRequiredService<ICityService>();

            if (!(_regionService.Find(region => region.Name == "Rio Grande do Sul").Count() > 0))
                _regionService.Add(new Region()
                {
                    Name = "Rio Grande do Sul"
                });

            if (!(_regionService.Find(region => region.Name == "São Paulo").Count() > 0))
                _regionService.Add(new Region()
                {
                    Name = "São Paulo"
                });


            if (!(_regionService.Find(region => region.Name == "Curitiba").Count() > 0))
                _regionService.Add(new Region()
                {
                    Name = "Curitiba"
                });

            var regionRS = _regionService.Find(region => region.Name == "Rio Grande do Sul").FirstOrDefault();

            if (!(_cityService.Find(city => city.Name == "Porto Alegre").Count() > 0))
                _cityService.Add(new City()
                {
                    Name = "Porto Alegre",
                    RegionId = regionRS.Id,
                    LatLong = "-30.1087957,-51.3172272"
                });
        }

        internal class DefaultUserModel
        {
            public ApplicationUser User;
            public string Password;
            public RoleTypes RoleType;
        }
    }
}
