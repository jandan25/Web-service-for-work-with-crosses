using System;
using System.Data.Entity.Core.Objects;
using GenericRepository.Interface;
using System.Threading.Tasks;
using CrossEntities;

namespace Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetStandardRepo<T>() where T : class;
       // T GetRepo<T, TEntity>() where T : IGenericRepository<TEntity> where TEntity : class;

        /// <summary>
        /// Синхронная фиксация изменений в бд
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Выборка по кроссам
        /// </summary>
        ObjectResult<CrossSelectionResult> GetCrossSelection();

        Task<ObjectResult<CrossSelectionResult>> GetCrossSelectionAsync();

        /// <summary>
        /// Асинхронная фиксация изменений в бд
        /// </summary>
        Task SaveChangesAsync();
    }
}


