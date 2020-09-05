using System.Linq;
using eShop.Common.HelperFields;

namespace eShop.InternalServer.Interfaces
{
    public interface IMicroKernel
    {
        IQueryable<TEntity> GetEntities<TEntity>() where TEntity : BaseField;

        TEntity AddEntity<TEntity>(TEntity entity) where TEntity : BaseField;
        TEntity AddEntityAsync<TContext, TEntity>(TContext context, TEntity entity) where TEntity : BaseField;

        void SaveChanges();
        void SaveChangesAsync();
    }
}
