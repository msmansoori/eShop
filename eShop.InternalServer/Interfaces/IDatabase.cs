using System.Linq;
using eShop.Common.HelperFields;
using Microsoft.EntityFrameworkCore;

namespace eShop.InternalServer.Interfaces
{
    public interface IDatabase
    {
        IQueryable<TEntity> GetEntities<TContext, TEntity>(TContext context) where TContext : DbContext where TEntity : BaseField;
        
        TEntity AddEntity<TContext, TEntity>(TContext context, TEntity entity) where TContext : DbContext where TEntity : BaseField;
        TEntity AddEntityAsync<TContext, TEntity>(TContext context, TEntity entity) where TContext : DbContext where TEntity : BaseField;

        void SaveChanges<TContext>(TContext context) where TContext : DbContext;
        void SaveChangesAsync<TContext>(TContext context) where TContext : DbContext;
        //TEntity GetEntity<TEntity>(long id) where TEntity : class;
        //TEntity GetEntity<TEntity>(Guid externalId) where TEntity : class;
        //IQueryable<TEntity> GetEntities<TEntity>(Guid externalId) where TEntity : class;
        //TEntity AddEntity<TEntity>(TEntity entity) where TEntity : class;
        //List<TEntity> AddMultiple<TEntity>(List<TEntity> entities) where TEntity : class;
        //TEntity UpdateEntity<TEntity>(TEntity entity) where TEntity : class;
        //List<TEntity> UpdateMultiple<TEntity>(List<TEntity> entities) where TEntity : class;
        //TEntity DeleteEntity<TEntity>(long id) where TEntity : class;
        //TEntity DeleteEntity<TEntity>(Guid externalId) where TEntity : class;
        //TEntity DeleteEntity<TEntity>(TEntity entity) where TEntity : class;
        //List<TEntity> DeleteMultiple<TEntity>(List<long> ids) where TEntity : class;
        //List<TEntity> DeleteMultiple<TEntity>(List<Guid> externalIds) where TEntity : class;
        //List<TEntity> DeleteMultiple<TEntity>(List<TEntity> entities) where TEntity : class;
    }
}
