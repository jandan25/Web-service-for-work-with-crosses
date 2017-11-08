using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAppCrosses.Attributes;

namespace WebAppCrosses.Models
{
    public class ProductsAndMotorsModel
    {
        public int ProductAndMotorID { get; set; }
        
        [RegularExpression("([1-9][0-9]*)")]
        public int ProductID { get; set; }

        [RegularExpression("([1-9][0-9]*)")]
        public int MotorID { get; set; }

        [MyBool]
        public bool InPrice { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }
    }
}