using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossEntities;
using Repositories.Implementations;
using GenericRepository.Interface;
using System.Data.Entity.Validation;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using CrossEntities.Interfaces;
using Repositories.Interfaces;

namespace Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        IGoodWillEntitiesContext _context;
        IRepositoryProvider _provider;

        #region Constructors
        public UnitOfWork()
        {
            _context = new GoodWillDbContext();
            _provider = new RepositoryProvider(_context, new RepositoryFactory());
        }
        public UnitOfWork(IGoodWillEntitiesContext context)
        {
            _context = context;
            _provider = new RepositoryProvider(context, new RepositoryFactory());
        }

        public UnitOfWork(GoodWillDbContext context)
        {
            _context = context;
            
        }
        #endregion

        public IGenericRepository<T> GetStandardRepo<T>() where T : class
        {
            return _provider.GetStandartRepository<T>();
        }

        //public T GetRepo<T, TEntity>() where T : IGenericRepository<TEntity> where TEntity : class
        //{
        //    return RepositoryProvider.GetCustomRepository<T, TEntity>();
        //}

        //public T GetRepo<T>() where T : class
        //{
        //    return RepositoryProvider.GetRepository<T>();
        //}

        public ObjectResult<CrossSelectionResult> GetCrossSelection()
        {
            return _context.pr_GetCrossSelection();
        }

        public async Task<ObjectResult<CrossSelectionResult>> GetCrossSelectionAsync()
        {
            return await Task.Run(() => { return GetCrossSelection(); });
        }
        public void Dispose() { }

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
