using CrossEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppCrosses.Tests.entities.FakeDbsets
{
    class FakeCarModelsSet : FakeDbSet<CarModels>
    {
        public override CarModels Find (params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.CarModelID == (int)keyValues.Single());
        }
    }
}
