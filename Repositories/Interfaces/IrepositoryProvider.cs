using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericRepository.Interface;
using CrossEntities;

namespace Repositories.Interfaces
{
    public interface IRepositoryProvider
    {
        /// <summary>
        /// Получить стандарный репозиторий
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <returns>Репозиторий</returns>
        /// <remarks>
        /// 
        /// </remarks>
        IGenericRepository<T> GetStandartRepository<T>() where T : class;

        /// <summary>
        /// Получить нестандартный репозиторий
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <remarks>
        /// Если для какой либо сущности определена дополнительная реализация репозитория, 
        /// </remarks>
        /// <returns></returns>
        T GetCustomRepository<U, T>() where U : class;
    }
}
