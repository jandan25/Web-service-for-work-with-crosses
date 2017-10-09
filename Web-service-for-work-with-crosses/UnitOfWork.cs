using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossEntities;

namespace Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly GoodWillDbContext _context;

        #region Constructors

        public UnitOfWork() : this(new GoodWillDbContext())
        { }

        public UnitOfWork(GoodWillDbContext context)
        {
            _context = context;
            //conyinue
            Repo
        }

        #endregion
    }
}
