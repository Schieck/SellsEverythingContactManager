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

        [Display(Name="Seller")]
        public virtual ApplicationUser Owner { get; set; }

        public Classification Classification { get; set; }
     }

    public enum Classification
    {
        [Display(Name="VIP")]
        VIP = 0,
        [Display(Name = "Regular")]
        Regular = 1,
        [Display(Name = "Sporadic")]
        Sporadic = 2
    }
}
