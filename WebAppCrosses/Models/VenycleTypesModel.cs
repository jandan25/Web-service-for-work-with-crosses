using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAppCrosses.Attributes;

namespace WebAppCrosses.Models
{
    public class VenycleTypesModel
    {
        [Key]
        public int VenycleTypeID { get; set; }

        [StringLength(255, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [MyBool]
        public bool IsShown { get; set; }

    }
}