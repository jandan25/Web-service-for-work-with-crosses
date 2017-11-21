using CrossEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_service_for_work_with_crosses.tests.fakeentities
{
    class FakeCarModelsSet : FakeDbSet<CarModels>
    {
        public override CarModels Find (params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.CarModelID == (int)keyValues.Single());
        }
    }
}
