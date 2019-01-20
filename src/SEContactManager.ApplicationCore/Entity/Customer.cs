using System;
using System.Collections.Generic;
using System.Text;

namespace SEContactManager.ApplicationCore.Entity
{
    public class Customer
    {
        public Customer()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public DateTime? LastPurchase { get; set; }

        public ApplicationUser User { get; set; }
     }
}
