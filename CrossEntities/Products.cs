//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CrossEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            this.ProductsAndMotors = new HashSet<ProductsAndMotors>();
            this.ProductsAndOes = new HashSet<ProductsAndOes>();
        }
    
        public int ProductID { get; set; }
        public int BrandID { get; set; }
        public int CategoryID { get; set; }
        public int ProductStateID { get; set; }
        public Nullable<int> FormID { get; set; }
        public string Code { get; set; }
        public string Code1C { get; set; }
        public string Name { get; set; }
        public string Aplicability { get; set; }
        public string Analog { get; set; }
        public string ChangeGw { get; set; }
        public string Oem { get; set; }
        public int PackageCount { get; set; }
        public double Volume { get; set; }
        public double Weight { get; set; }
        public decimal PriceUsd { get; set; }
        public decimal PriceRub { get; set; }
        public bool IsShown { get; set; }
        public string BarCode { get; set; }
        public decimal AParam { get; set; }
        public decimal BParam { get; set; }
        public decimal BpParam { get; set; }
        public decimal CParam { get; set; }
        public decimal DParam { get; set; }
        public decimal EParam { get; set; }
        public decimal FParam { get; set; }
        public decimal GParam { get; set; }
        public decimal HParam { get; set; }
        public decimal NrParam { get; set; }
        public string Comment { get; set; }
        public int AverageSales { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductsAndMotors> ProductsAndMotors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductsAndOes> ProductsAndOes { get; set; }
    }
}
