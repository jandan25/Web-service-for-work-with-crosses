using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAppCrosses.Attributes;

namespace WebAppCrosses.Models
{
    public class ProductsModel
    {
        public int ProductID { get; set; }

        [RegularExpression("([1-9][0-9]*)")] 
        public int BrandID { get; set; }

        [RegularExpression("([1-9][0-9]*)")] 
        public int CategoryID { get; set; }

        [RegularExpression("([1-9][0-9]*)")] 
        public int ProductStateID { get; set; }

        [RegularExpression("([1-9][0-9]*)")] 
        public int? FormID { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Code { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Code1C { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 3)]
        public string Aplicability { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 3)]
        public string Analog { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 3)]
        public string ChangeGw { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 3)]
        public string Oem { get; set; }

        [Required]
        [RegularExpression("([1-9][0-9]*)")] 
        public int PackageCount { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter valid float Number")]
        public double Volume { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter valid float Number")]
        public double Weight { get; set; }

        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")] //decimal validation attribute 3.22
        public decimal PriceUsd { get; set; }

        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")] 
        public decimal PriceRub { get; set; }

        [MyBool]
        public bool IsShown { get; set; }


        [Required(AllowEmptyStrings = true)]
        public string BarCode { get; set; }

        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")] 
        public decimal AParam { get; set; }

        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")] 
        public decimal BParam { get; set; }

        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        public decimal BpParam { get; set; }

        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")] 
        public decimal CParam { get; set; }

        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")] 
        public decimal DParam { get; set; }

        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")] 
        public decimal EParam { get; set; }

        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        public decimal FParam { get; set; }

        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")] 
        public decimal GParam { get; set; }

        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")] 
        public decimal HParam { get; set; }

        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")] 
        public decimal NrParam { get; set; }

        [StringLength(4000)]
        public string Comment { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public int? GroupID { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "Please enter valid float Number")]
        public int AverageSales { get; set; }
    }
}