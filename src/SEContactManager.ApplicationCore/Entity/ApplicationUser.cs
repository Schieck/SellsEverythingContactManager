using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEContactManager.ApplicationCore.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Customer> Customer { get; set; }
    }
}
