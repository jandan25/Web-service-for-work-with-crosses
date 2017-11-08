using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAppCrosses.Attributes;

namespace WebAppCrosses.Models
{
    public class ProductsAndOesModel
    {
        public int ProductAndOeID { get; set; }

        [Required]
        [RegularExpression("([1-9][0-9]*)")]
        public int ProductID { get; set; }

        [Required]
        [RegularExpression("([1-9][0-9]*)")]
        public int OeID { get; set; }

        [MyBool]
        public bool InPrice { get; set; }
    }
}