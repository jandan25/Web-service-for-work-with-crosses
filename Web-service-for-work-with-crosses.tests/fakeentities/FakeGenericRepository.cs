using GenericRepository.Implementation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_service_for_work_with_crosses.tests.fakeentities
{
    class FakeGenericRepository : GenericRepository<FakeCarModels>
    {
        public FakeGenericRepository(DbContext context) : base(context)
        {
        }
    }
}
