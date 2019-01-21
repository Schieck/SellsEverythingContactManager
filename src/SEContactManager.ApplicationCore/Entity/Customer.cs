using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SEContactManager.ApplicationCore.Entity
{
    public class Customer
    {
        public Customer()
        {

        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Display(Name = "Last Purchase")]
        public DateTime? LastPurchase { get; set; }

        public string OwnerId { get; set; }

        [Display(Name = "Seller")]
        public virtual ApplicationUser Owner { get; set; }


        public int CityId { get; set; }

        [Display(Name = "City")]
        public virtual City City { get; set; }

        [Required]
        public Classification Classification { get; set; }

        [Required]
        public Gender Gender { get; set; }
     }
}
