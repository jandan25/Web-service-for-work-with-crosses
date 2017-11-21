using GenericRepository.Implementation;
using GenericRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CrossEntities;
using Repositories.Interfaces;
using CrossEntities.Interfaces;

namespace Repositories.Implementations
{
    public class RepositoryProvider : IRepositoryProvider
    {
        private readonly IGoodWillEntitiesContext _context;
        private readonly RepositoryFactory _factory;
        private IDictionary<Type, object> _repositories;

        public RepositoryProvider(IGoodWillEntitiesContext context, RepositoryFactory factory)
        {
            _context = context;
            _factory = factory;
            _repositories = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Добавить новый репозиторий в словарь
        /// </summary>
        /// <typeparam name="T">Тип репозитория</typeparam>
        /// <param name="repository"></param>
        /// <remarks>
        /// Repositories - это коллекция ключ-значение, где ключ - тип сущности, 
        /// значение - объект репозитория. Чтобы каждый раз не создавать репозитории заново, 
        /// при первом создании объект репозитория помещается в эту коллекцию. При последующем 
        /// использовании в границах контекста объект репозитория будет браться из коллекции.
        /// </remarks>
        private void SetRepository<T>(T repository)
        {
            _repositories[typeof(T)] = repository;
        }

        /// <summary>
        /// Создать репозиторий
        /// </summary>
        /// <typeparam name="T">Тип репозитория</typeparam>
        /// <typeparam name="U">Тип сущности</typeparam>
        /// <returns>Репозиторий</returns>
        /// <remarks>
        /// За непосредственное создание объекта репозитория отвечает фабрика RepositoryFactory. 
        /// Основная задача в данном случае - определить правильную фабрику по типу сущности.
        /// </remarks>
        private T MakeRepository<U, T>() where U : class
        {
            var repofactory = _factory.GetRepositoryFactory<U>();
            return (T)repofactory(_context);
        }

        /// <summary>
        /// Получить фабрику репозитория
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <remarks>
        /// Метод определеяет, какую фабрику использовать для создания репозитория
        /// </remarks>
        public Func<GoodWillDbContext, object> GetFactory<T>() where T : class
        {
            Func<GoodWillDbContext, object> repository1 = null;
            return repository1 = GetDefaultFactory<T>();
        }

        /// <summary>
        /// Фабрика создания стандартного репозитория
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private Func<GoodWillDbContext, object> GetDefaultFactory<T>() where T : class
        {
            return dbContext => new GenericRepository<T>(dbContext);
        }
        /// <summary>
        /// Получить репозиторий
        /// </summary>
        /// <typeparam name="T">Тип репозитория</typeparam>
        /// <typeparam name="U">Тип сущности</typeparam>
        /// <returns>Репозиторий</returns>
        /// <remarks>
        /// Получить репозиторий. Если необходимый репозиторий найден в коллекции Repositories, то взять оттуда.
        /// Если не найден, то создать объект репозитория и поместить в эту коллекцию.
        /// </remarks>
        public T GetCustomRepository<U, T>() where U : class
        {
            object repo;
            _repositories.TryGetValue(typeof(T), out repo);
            if (repo == null)
            {
                repo = MakeRepository<U, T>();
                _repositories.Add(typeof(T), repo);
            }
            return (T) repo;
        }

        public IGenericRepository<T> GetStandartRepository<T>() where T : class
        {
            _repositories.TryGetValue(typeof(IGenericRepository<T>), out object repo);
            if (repo == null)
            {
                repo = MakeRepository<T, IGenericRepository<T>>();
                _repositories.Add(typeof(IGenericRepository<T>), repo);
            }
            return (IGenericRepository<T>)repo;
        }
    }
}
