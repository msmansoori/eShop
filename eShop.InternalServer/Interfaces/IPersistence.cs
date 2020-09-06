using System;
using System.Collections.Generic;
using System.Linq;
using eShop.Common.HelperFields;
using Microsoft.EntityFrameworkCore;

namespace eShop.InternalServer.Interfaces
{
    public interface IPersistence
    {
        IQueryable<TEntity> GetEntities<TContext, TEntity>(TContext context) where TContext : DbContext where TEntity : BaseField;
        TEntity GetEntity<TContext, TEntity>(TContext context, Guid externalId) where TContext : DbContext where TEntity : BaseField;
        TEntity GetEntity<TContext, TEntity>(TContext context, long id) where TContext : DbContext where TEntity : BaseField;

        TEntity AddEntity<TContext, TEntity>(TContext context, TEntity entity, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField;
        TEntity AddEntityAsync<TContext, TEntity>(TContext context, TEntity entity, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField;

        void SaveChanges<TContext>(TContext context) where TContext : DbContext;
        void SaveChangesAsync<TContext>(TContext context) where TContext : DbContext;

        TEntity UpdateEntity<TContext, TEntity>(TContext context, TEntity entity, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField;
        List<TEntity> UpdateMultiple<TContext, TEntity>(TContext context, List<TEntity> entities, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField;
        IQueryable<TEntity> UpdateMultiple<TContext, TEntity>(TContext context, IQueryable<TEntity> entities, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField;

        TEntity DeleteEntity<TContext, TEntity>(TContext context, Guid externalId, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField;
        TEntity DeleteEntity<TContext, TEntity>(TContext context, long id, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField;
        TEntity DeleteEntity<TContext, TEntity>(TContext context, TEntity entity, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField;
    }
}
