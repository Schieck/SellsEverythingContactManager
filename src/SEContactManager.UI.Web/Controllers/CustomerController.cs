using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEContactManager.ApplicationCore.Entity;
using SEContactManager.ApplicationCore.Interfaces.Services;
using SEContactManager.Infrastructure.Data;
using SEContactManager.UI.Web.Models;

namespace SEContactManager.UI.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ICityService _cityService;
        private readonly IRegionService _regionService;
        private readonly UserManager<ApplicationUser> _userManagerService;

        public CustomerController(ICustomerService CustomerService, ICityService cityService, IRegionService regionService, UserManager<ApplicationUser> userManagerService)
        {
            _customerService = CustomerService;
            _regionService = regionService;
            _cityService = cityService;
            _userManagerService = userManagerService;
        }

        // GET: Customer
        [Authorize]
        public IActionResult Index()
        {
            ViewData["Cities"] = new SelectList(_cityService.FindAll().ToList(), "Id", "Name");
            ViewData["Regions"] = new SelectList(_regionService.FindAll().ToList(), "Id", "Name");
            
            var role = Enum.GetName(typeof(RoleTypes), RoleTypes.Administrator);

            if(User.IsInRole(role))
            {
                ViewData["Sellers"] = new SelectList(_userManagerService.Users.ToList(), "Id", "Email");
                return View(_customerService.FindAll());                
            }
            else
            {
                return View(_customerService.FindAll(User));
            }
        }

        // GET: Customer
        [Authorize]
        [HttpPost]
        public IActionResult Index(CustomerSearch customerSearch)
        {
            ViewData["Cities"] = new SelectList(_cityService.FindAll().ToList(), "Id", "Name");
            ViewData["Regions"] = new SelectList(_regionService.FindAll().ToList(), "Id", "Name");

            var role = Enum.GetName(typeof(RoleTypes), RoleTypes.Administrator);

            if (User.IsInRole(role))
            {
                ViewData["Sellers"] = new SelectList(_userManagerService.Users.ToList(), "Id", "Email");
                return View(_customerService.FindBySearch(customerSearch));
            }
            else
            {
                return View(_customerService.FindBySearch(customerSearch, User));
            }
        }

        // GET: Customer/Details/5
        [Authorize]
        public IActionResult Details(int id)
        {
            var role = Enum.GetName(typeof(RoleTypes), RoleTypes.Administrator);
            var customer = new Customer();

            if (User.IsInRole(role))
            {
                customer = _customerService.FindById(id);
            }
            else
            {
                customer = _customerService.FindById(id, User);
            }

            if (customer == new Customer())
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            ViewData["Cities"] = new SelectList(_cityService.FindAll().ToList(), "Id", "Name");
            
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.Add(customer, User);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Edit/5
        public IActionResult Edit(int id)
        {
            ViewData["Cities"] = new SelectList(_cityService.FindAll().ToList(), "Id", "Name");

            var role = Enum.GetName(typeof(RoleTypes), RoleTypes.Administrator);
            var customer = new Customer();

            if (User.IsInRole(role))
            {
                customer = _customerService.FindById(id);
            }
            else
            {
                customer = _customerService.FindById(id, User);
            }

            if (customer == new Customer())
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            var role = Enum.GetName(typeof(RoleTypes), RoleTypes.Administrator);

            if (ModelState.IsValid)
            {
                try
                {
                    if (User.IsInRole(role))
                    {
                        _customerService.Update(customer);
                    }
                    else
                    {
                        _customerService.Update(customer, User);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Delete/5
        public IActionResult Delete(int id)
        {

            var role = Enum.GetName(typeof(RoleTypes), RoleTypes.Administrator);
            var customer = new Customer();

            if (User.IsInRole(role))
            {
                customer = _customerService.FindById(id);
            }
            else
            {
                customer = _customerService.FindById(id, User);
            }

            if (customer == new Customer())
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var role = Enum.GetName(typeof(RoleTypes), RoleTypes.Administrator);
            var customer = new Customer();

            if (User.IsInRole(role))
            {
                customer = _customerService.FindById(id);
            }
            else
            {
                customer = _customerService.FindById(id, User);
            }
            
            _customerService.Remove(customer);
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            var role = Enum.GetName(typeof(RoleTypes), RoleTypes.Administrator);
            var customer = new Customer();

            if (User.IsInRole(role))
            {
                customer = _customerService.FindById(id);
            }
            else
            {
                customer = _customerService.FindById(id, User);
            }

            if(customer == new Customer())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
