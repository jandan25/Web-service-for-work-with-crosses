using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using CrossEntities;

namespace Web_service_for_work_with_crosses.tests.fakeentities
{
    public partial class FakeEntities : DbContext 
    {
        public FakeEntities()
            : base("name=FakeEntities")
        {
        }
        public virtual DbSet<FakeCarModels> FakeCarModels { get; set; }
    }
}
