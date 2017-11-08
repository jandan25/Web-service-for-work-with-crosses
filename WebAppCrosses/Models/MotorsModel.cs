using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAppCrosses.Attributes;

namespace WebAppCrosses.Models
{
    public class MotorsModel
    {
        public int MotorID { get; set; }

        [RegularExpression("([1-9][0-9]*)")] // for 1-inf
        public int CarModelID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Engine { get; set; }

        [StringLength(500, MinimumLength = 3)]
        public string CardanShaft { get; set; }

        [StringLength(500, MinimumLength = 3)]
        public string HoursePower { get; set; }

        [MyDate]
        [Required(AllowEmptyStrings = true)]
        public DateTime? StartDate { get; set; }

        [MyDate]
        [Required(AllowEmptyStrings = true)]
        public DateTime? EndDate { get; set; }

        [MyBool]
        public bool IsShown { get; set; }

    }
}