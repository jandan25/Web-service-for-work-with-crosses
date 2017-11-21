using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppCrosses.Tests.entities.FakeDbsets
{
    class FakeDbEntityEntry<T> where T : class
    {
        public EntityState State { get; set; }
    }
}
