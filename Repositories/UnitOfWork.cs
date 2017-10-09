using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossEntities;
using Repositories.Implementations;
using GenericRepository.Interface;
using System.Data.Entity.Validation;

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
            RepositoryProvider = new RepositoryProvider(context, new RepositoryProvider());
        }

        #endregion

        private RepositoryProvider RepositoryProvider { get; set; }

        public IGenericRepository<T> GetStandardRepo<T>() where T : class
        {
            return RepositoryProvider.GetStandartRepository<T>();
        }

        public T GetRepo<T, TEntity>() where T : IGenericRepository<TEntity> where TEntity : class
        {
            return RepositoryProvider.GetCustomRepository<T, TEntity>();
        }

        //public T GetRepo<T>() where T : class
        //{
        //    return RepositoryProvider.GetRepository<T>();
        //}

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                foreach (var validationError in validationErrors.ValidationErrors)
                    msg += $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}" + Environment.NewLine;

                throw new Exception(msg, dbEx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task SaveChangesAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                foreach (var validationError in validationErrors.ValidationErrors)
                    msg += $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}" + Environment.NewLine;

                throw new Exception(msg, dbEx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
