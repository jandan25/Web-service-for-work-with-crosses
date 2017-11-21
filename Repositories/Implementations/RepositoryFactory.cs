using CrossEntities;
using CrossEntities.Interfaces;
using GenericRepository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class RepositoryFactory
    {
        private IDictionary<Type, Func<IGoodWillEntitiesContext, object>> _specreposfactories =
            new Dictionary<Type, Func<IGoodWillEntitiesContext, object>>
            {
                {typeof(Users), context => new UsersRepository(context) }
            };
        public Func<IGoodWillEntitiesContext, object> GetRepositoryFactory<T>() where T : class
        {
            _specreposfactories.TryGetValue(typeof(T), out var specrepofactory);
            return specrepofactory ?? GetGenericRepositoryFactory<T>();
        }

        private Func<IGoodWillEntitiesContext, object> GetGenericRepositoryFactory<T>() where T : class
        {
            return context => new GenericRepository<T>(context);
        }
    }
}
