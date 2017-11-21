using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossEntities;
using CrossEntities.Interfaces;


namespace Repositories
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        IGoodWillEntitiesContext _context;

        public UnitOfWorkFactory()
        { }

        public UnitOfWorkFactory(IGoodWillEntitiesContext context)
        {
            _context = context;
        }

        public virtual IUnitOfWork Create()
        {
            if (_context == null)
                return new UnitOfWork();
            else
                return new UnitOfWork(_context);
        }
    }
}
