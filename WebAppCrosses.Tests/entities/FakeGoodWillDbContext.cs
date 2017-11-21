using CrossEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppCrosses.Tests.entities
{
    public class FakeGoodWillDbContext : IFakeGoodWillDbContext
    {
        public FakeGoodWillDbContext()
        {
            this.CarModels = new TestModelDbSet();
            //this.Manufactors = new TestModelDbSet();
            //this.Motors = new TestModelDbSet();
            //this.Products = new TestModelDbSet();
            //this.ProductsAndMotors = new TestModelDbSet();
            //this.ProductsAndOes = new TestModelDbSet();
            //this.UserRoles = new TestModelDbSet();
            //this.Users = new TestModelDbSet();
            //this.VenycleTypes = new TestModelDbSet();
        }
        public virtual DbSet<CarModels> CarModels { get; }
        public virtual DbSet<Manufactors> Manufactors { get; }
        public virtual DbSet<Motors> Motors { get; }
        public virtual DbSet<Products> Products { get; }
        public virtual DbSet<ProductsAndMotors> ProductsAndMotors { get; }
        public virtual DbSet<ProductsAndOes> ProductsAndOes { get; }
        public virtual DbSet<VenycleTypes> VenycleTypes { get; }
        public virtual DbSet<UserRoles> UserRoles { get; }
        public virtual DbSet<Users> Users { get; }
        public void Dispose() { }
        // think about it
        //public  ObjectResult<CrossSelectionResult> pr_GetCrossSelection();
    }
}
