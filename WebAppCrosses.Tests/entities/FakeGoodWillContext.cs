using CrossEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using CrossEntities.Interfaces;
using System.Data.Entity.Core.Objects;
using WebAppCrosses.Tests.entities.FakeDbsets;
using System.Data.Entity.Infrastructure;

namespace WebAppCrosses.Tests.entities
{
    public class FakeGoodWillContext : DbContext, IGoodWillEntitiesContext
    {
        IDictionary<Type, object> _FakeDbSets;
        IDictionary<Type, object> _FakeDbEntityEntries;

        public FakeGoodWillContext()
        {
            _FakeDbSets = new Dictionary<Type, object>();
            _FakeDbEntityEntries = new Dictionary<Type, object>();

            var _fakecarmodelsset = new FakeCarModelsSet();
            _FakeDbSets.Add(typeof(CarModels), _fakecarmodelsset);
            _FakeDbEntityEntries.Add(typeof(CarModels), new FakeDbEntityEntry<CarModels>());
            CarModels = _fakecarmodelsset;
        }

        public virtual FakeDbSet<CarModels> CarModels { get; set; }
        public virtual FakeDbSet<Manufactors> Manufactors { get; set; }
        public virtual FakeDbSet<Motors> Motors { get; set; }
        public virtual FakeDbSet<Products> Products { get; set; }
        public virtual FakeDbSet<ProductsAndMotors> ProductsAndMotors { get; set; }
        public virtual FakeDbSet<ProductsAndOes> ProductsAndOes { get; set; }
        public virtual FakeDbSet<VenycleTypes> VenycleTypes { get; set; }
        public virtual FakeDbSet<UserRoles> UserRoles { get; set; }
        public virtual FakeDbSet<Users> Users { get; set; }

        public virtual ObjectResult<CrossSelectionResult> pr_GetCrossSelection()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CrossSelectionResult>("pr_GetCrossSelection");
        }

        public new IDbSet<Type> Set (Type FakeEntity)
        {
            _FakeDbSets.TryGetValue(typeof(Type), out object output);
            return (FakeDbSet<Type>)output;
        }

        public new IDbSet<U> Set <U>() where U : class
        {
            _FakeDbSets.TryGetValue(typeof(U), out object output);
            return (FakeDbSet<U>) output;
        }

        public void SetModified<U>(U item) where U : class
        {
            Entry(item).State = EntityState.Modified;
        }

    }
}
