using CrossEntities;
using CrossEntities.Interfaces;
using GenericRepository.Implementation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_service_for_work_with_crosses.tests.fakeentities
{
    public class FakeGenericRepository : GenericRepository<CarModels>
    {
        public FakeGenericRepository(IGoodWillEntitiesContext context) : base(context)
        {
        }
    }
}
