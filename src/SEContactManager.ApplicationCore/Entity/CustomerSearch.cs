using SEContactManager.ApplicationCore.Entity;
using System;
using System.Collections.Generic;

namespace SEContactManager.ApplicationCore.Entity
{
    public class CustomerSearch
    {       
        public string Name { get; set; }
       
        public DateTime? LastPurchase { get; set; }
        
        public string SellerId { get; set; }

        public int? CityId { get; set; }

        public Classification? Classification { get; set; }

        public Gender? Gender { get; set; }

        public int? RegionId { get; set; }

        public DateTime? Until { get; set; }

    }
}