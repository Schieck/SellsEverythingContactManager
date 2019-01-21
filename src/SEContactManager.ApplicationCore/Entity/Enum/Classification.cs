using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SEContactManager.ApplicationCore.Entity
{
    public enum Classification
    {
        [Display(Name = "VIP")]
        VIP = 0,
        [Display(Name = "Regular")]
        Regular = 1,
        [Display(Name = "Sporadic")]
        Sporadic = 2
    }
}
