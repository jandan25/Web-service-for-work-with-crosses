using CrossEntities;
using CrossEntities.Interfaces;
using RepositoriesTests.Production;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using WebAppCrosses.Tests.entities.FakeDbsets;

namespace Web_service_for_work_with_crosses.tests.fakeentities
{
    public class FakeGoodwillEntitesContext : DbContext, IGoodWillEntitiesContext
    {
        IDictionary<Type, object> _FakeDbSets;
        IDictionary<Type, object> _FakeDbEntityEntries;

        public FakeGoodwillEntitesContext()
        {
            _FakeDbSets = new Dictionary<Type, object>();
            _FakeDbEntityEntries = new Dictionary<Type, object>();

            var _fakecarmodelsset = new FakeCarModelsSet();
            _FakeDbSets.Add(typeof(CarModels), _fakecarmodelsset);
            _FakeDbEntityEntries.Add(typeof(CarModels), new FakeDbEntityEntry<CarModels>());
            CarModels = _fakecarmodelsset;
        }

        public virtual FakeDbSet<CarModels> CarModels { get; set; }

        public new IDbSet<Type> Set(Type FakeEntity)
        {
            _FakeDbSets.TryGetValue(typeof(Type), out object output);
            return (FakeDbSet<Type>)output;
        }

        public new IDbSet<U> Set<U>() where U : class
        {
            _FakeDbSets.TryGetValue(typeof(U), out object output);
            return (FakeDbSet<U>)output;
        }

        public void SetModified<U>(U item) where U : class
        {
            Entry(item).State = EntityState.Modified;
        }

        public virtual ObjectResult<CrossSelectionResult> pr_GetCrossSelection()
        {
            throw new NotImplementedException();
        }
    }
}
