using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossEntities.Interfaces
{
    public interface IGoodWillEntitiesContext : IDisposable
    {
        IDbSet<Type> Set(Type Entity);

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry Entry(object entity);

        void SetModified<Entity>(Entity item) where Entity : class;

        ObjectResult<CrossSelectionResult> pr_GetCrossSelection();

        Task<int> SaveChangesAsync();

        int SaveChanges();
    }
}
