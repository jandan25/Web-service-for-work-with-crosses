using GenericRepository.Implementation;
using GenericRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CrossEntities;

namespace Repositories.Implementations
{
    public partial class RepositoryProvider
    {
        public RepositoryProvider()
        { }

        public RepositoryProvider(GoodWillDbContext context, RepositoryProvider repository)
        {
            this._context = context;
            this._repository = repository;
            //this._repositoryFactory = factory;
            Repositories = new Dictionary<Type, object>();
        }

        private GoodWillDbContext _context;
        //private RepositoryFactory _repositoryFactory;
        private RepositoryProvider _repository;
        private Dictionary<Type, object> Repositories { get; set; }


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
            Repositories[typeof(T)] = repository;
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
        private T MakeRepository<T, U>() where U : class
        {
            var r = _repository.GetFactory<U>();
            var repo = r(this._context);
            return (T) repo;
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
        private T GetRepository<T, U>() where U : class
        {
            object repo;
            Repositories.TryGetValue(typeof(T), out repo);
            if (repo == null)
            {
                repo = MakeRepository<T, U>();
                SetRepository(repo);
            }
            return (T) repo;
        }


        public IGenericRepository<T> GetStandartRepository<T>() where T : class
        {
            return GetRepository<IGenericRepository<T>, T>();
        }


        //public T GetCustomRepository<T, U>() where U : class
        //{
        //    return GetRepository<T, U>();
        //}
    }
}
