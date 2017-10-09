using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GenericRepository.Interface;

namespace GenericRepository.Implementation
{
    public class GenericRepository <T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _entities;

        public GenericRepository(DbContext context)
        {
            this._context = context;
            _entities = context.Set<T>();
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int page = 0, int pageSize = 12)
        {
            IQueryable<T> query = GetQuery(predicate, orderBy, includeProperties, page, pageSize);
            return query.ToList();
        }

        public virtual T GetById(object id)
        {
            if (id == null) throw new ArgumentNullException("Identifier is null");
            return _entities.Find(id);
        }

        public virtual T GetByParam(Expression<Func<T, bool>> predicate, string includeProperties = "")
        {
            IQueryable<T> query = _entities;
            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query.Include(includeProperty);
            }
            return query.FirstOrDefault(predicate);
        }

        private IQueryable<T> GetByParamQuery(Expression<Func<T, bool>> predicate, string includeProperties = "")
        {
            IQueryable<T> query = _entities;
            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query.Include(includeProperty);
            }
            return query;
        }

        public virtual async Task<T> GetByParamAsynс(Expression<Func<T, bool>> predicate, string includeProperties = "")
        {
            IQueryable<T> query = GetByParamQuery(predicate, includeProperties);
            return await query.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            if (id == null) throw new ArgumentException("Identifier is null");
            return await _entities.FindAsync(id);
        }

        private IQueryable<T> GetQuery(Expression<Func<T,bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int page = 0,
            int pageSize = 12)
        {
            IQueryable<T> query = _entities;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (page > 0)
            {
                query = query.Skip((page - 1) * pageSize).Take(pageSize);
            }
            return query;
        }

        public virtual async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int page = 0,
            int pageSize = 12)
        {
            IQueryable<T> query = GetQuery(predicate, orderBy, includeProperties, page, pageSize);
            return await query.ToListAsync();
        }

        public virtual void Insert(T entity)
        {
            if (entity == null) throw  new ArgumentException("Entity is null");
            _entities.Add(entity);
        }

        public virtual void Insert(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentException("Entities is null");
            entities = entities as T[] ?? entities.ToArray();
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        public virtual void Update(T entity)
        {
            if (entity == null) throw  new ArgumentException("Entity is null");
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Update(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentException("Entity is null");
            entities = entities as T[] ?? entities.ToArray();
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }

        public virtual bool Delete(object id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _entities.Remove(entity);
                return true;
            }
            return false;
        }
        
        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentException("Entity is null");
            _entities.Remove(entity);
        }

        public virtual void Delete(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentException("Entity is null");
            entities = entities as T[] ?? entities.ToArray();
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        public Task<T> GetByIdAsynс(object id)
        {
            throw new NotImplementedException();
        }
    }
}
