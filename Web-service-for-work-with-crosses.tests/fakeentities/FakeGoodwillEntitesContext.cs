using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Web_service_for_work_with_crosses.tests.fakeentities
{
    public class FakeGoodwillEntitesContext : DbContext
    {
        public virtual DbSet<FakeCarModels> FakeEntities { get; set; }
    }
}
