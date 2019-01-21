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

namespace SEContactManager.UI.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomerService _customerService;

        public CustomerController(ApplicationDbContext context, ICustomerService customerService)
        {
            _customerService = customerService;
            _context = context;
        }

        // GET: Customers
        [Authorize]
        public IActionResult Index()
        {

            var role = Enum.GetName(typeof(RoleTypes), RoleTypes.Administrator);

            if(User.IsInRole(role))
            {
                return View(_customerService.FindAll());
            }
            else
            {
                return View(_customerService.FindAll(User));
            }
        }

        // GET: Customers/Details/5
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

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Phone,LastPurchase,Classification")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.Add(customer, User);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public IActionResult Edit(int id)
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

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Phone,LastPurchase,Classification")] Customer customer)
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

        // GET: Customers/Delete/5
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

        // POST: Customers/Delete/5
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
