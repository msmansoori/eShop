using System;
using System.Collections.Generic;
using System.Linq;
using eShop.Common.HelperFields;

namespace eShop.InternalServer.Interfaces
{
    public interface IMicroKernel
    {
        IQueryable<TEntity> GetEntities<TEntity>() where TEntity : BaseField;
        TEntity GetEntity<TEntity>(Guid externalId) where TEntity : BaseField;
        TEntity GetEntity<TEntity>(long id) where TEntity : BaseField;

        TEntity AddEntity<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : BaseField;
        TEntity AddEntityAsync<TContext, TEntity>(TContext context, TEntity entity, bool saveChanges = false) where TEntity : BaseField;

        void SaveChanges();
        void SaveChangesAsync();

        TEntity UpdateEntity<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : BaseField;
        List<TEntity> UpdateMultiple<TEntity>(List<TEntity> entities, bool saveChanges = false) where TEntity : BaseField;
        IQueryable<TEntity> UpdateMultiple<TEntity>(IQueryable<TEntity> entities, bool saveChanges = false) where TEntity : BaseField;

        TEntity DeleteEntity<TEntity>(Guid externalId, bool saveChanges = false) where TEntity : BaseField;
        TEntity DeleteEntity<TEntity>(long id, bool saveChanges = false) where TEntity : BaseField;
        TEntity DeleteEntity<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : BaseField;
    }
}
