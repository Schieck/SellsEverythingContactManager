using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SEContactManager.ApplicationCore.Entity
{
    public class Region
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
