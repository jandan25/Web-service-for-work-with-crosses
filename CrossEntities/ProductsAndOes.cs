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
    
    public partial class ProductsAndOes
    {
        public int ProductAndOeID { get; set; }
        public int ProductID { get; set; }
        public int OeID { get; set; }
        public bool InPrice { get; set; }
    
        public virtual Products Products { get; set; }
    }
}
