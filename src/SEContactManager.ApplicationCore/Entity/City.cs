using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SEContactManager.ApplicationCore.Entity
{
    public class City
    {
        public City()
        {
                
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]        
        public string LatLong { get; set; }
        
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }
    }
}
