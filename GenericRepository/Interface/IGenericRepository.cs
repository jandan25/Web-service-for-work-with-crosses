using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepository.Interface
{
    public  interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int page = 0,
            int pageSize = 12);

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


        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        T GetById(object id);

        /// <summary>
        /// Get entity by params
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns>Entity</returns>
        T GetByParam(Expression<Func<T, bool>> predicate, string includeProperties = "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<T> GetByParamAsynс(Expression<Func<T, bool>> predicate, string includeProperties = "");

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(T entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Insert(IEnumerable<T> entities);

        /// <summary>
        /// Change entity state to modified state
        /// </summary>
        void SetEntityStateModified(T entity);

        /// <summary>
        /// Update entity 
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(T entity);

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Update(IEnumerable<T> entities);

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="id"></param>
        bool Delete(object id);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(T entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(IEnumerable<T> entities);
    }
}
