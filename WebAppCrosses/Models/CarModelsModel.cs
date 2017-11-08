using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAppCrosses.Attributes;

namespace WebAppCrosses.Models
{
    public class CarModelsModel
    { 
        public int CarModelID { get; set; }

        //range example
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int ManufactorID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        // custom attribute example
        [MyBool]
        public bool IsShown { get; set; }
    }
}