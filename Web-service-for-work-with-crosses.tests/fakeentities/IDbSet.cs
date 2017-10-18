using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Web_service_for_work_with_crosses.tests.fakeentities
{
    interface IDbSet<T> where T : class
    {
        /// <summary>
        /// Get IEnumerable of entities. Result may be filtered by lambda-expresions.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties">Properties</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Number of entities on one page</param>
        /// <returns>Entities</returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int page = 0, int pageSize = 12);

    }
}
