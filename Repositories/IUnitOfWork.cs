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

        //TODO: Добавить в интерфейс метод хранимой процедуры. Иначе она не будет доступна для работы при работе с интерфейсом.

        /// <summary>
        /// Синхронная фиксация изменений в бд
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Выборка по кроссам
        /// </summary>
        ObjectResult<CrossSelection> GetCrossSelection();

        /// <summary>
        /// Асинхронная фиксация изменений в бд
        /// </summary>
        Task SaveChangesAsync();
    }
}
