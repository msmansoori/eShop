using System;
using System.Collections.Generic;
using System.Linq;
using eShop.Common.HelperFields;
using eShop.Common.Utilities;
using eShop.InternalServer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eShop.InternalServer.Services
{
    public class Database : IDatabase
    {
        private readonly ILogger<Database> _logger;
        public Database(ILogger<Database> logger)
        {
            _logger = logger;
        }

        public TEntity AddEntity<TContext, TEntity>(TContext context, TEntity entity, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField
        {
            context.Set<TEntity>().Add(entity);
            SaveChanges(context: context, saveChanges: saveChanges);
            return entity;
        }

        public TEntity AddEntityAsync<TContext, TEntity>(TContext context, TEntity entity, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField
        {
            context.Set<TEntity>().AddAsync(entity);
            SaveChanges(context: context, saveChanges: saveChanges);
            return entity;
        }

        public TEntity DeleteEntity<TContext, TEntity>(TContext context, Guid externalId, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField
        {
            var entity = GetEntity<TContext, TEntity>(context: context, externalId: externalId);
            if (entity.IsNull())
            {
                throw new Exception("Invalid id");
            }
            entity.Active = false;
            SaveChanges(context: context, saveChanges: saveChanges);
            return entity;
        }

        public TEntity DeleteEntity<TContext, TEntity>(TContext context, long id, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField
        {
            var entity = GetEntity<TContext, TEntity>(context: context, id: id);
            if (entity.IsNull())
            {
                throw new Exception("Invalid id");
            }
            entity.Active = false;
            SaveChanges(context: context, saveChanges: saveChanges);
            return entity;
        }

        public TEntity DeleteEntity<TContext, TEntity>(TContext context, TEntity entity, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField
        {
            if (entity.IsNull())
            {
                throw new Exception("Invalid id");
            }
            entity.Active = false;
            SaveChanges(context: context, saveChanges: saveChanges);
            return entity;
        }

        public IQueryable<TEntity> GetEntities<TContext, TEntity>(TContext context) where TContext : DbContext where TEntity : BaseField
        {
            var entities = context.Set<TEntity>().Where(entity => entity.Active).AsQueryable();
            return entities;
        }

        public TEntity GetEntity<TContext, TEntity>(TContext context, Guid externalId) where TContext : DbContext where TEntity : BaseField
        {
            var entity = context.Set<TEntity>().SingleOrDefault(entity => entity.Active && entity.ExternalId == externalId);
            return entity;
        }

        public TEntity GetEntity<TContext, TEntity>(TContext context, long id) where TContext : DbContext where TEntity : BaseField
        {
            var entity = context.Set<TEntity>().SingleOrDefault(entity => entity.Active && entity.Id == id);
            return entity;
        }

        public void SaveChanges<TContext>(TContext context) where TContext : DbContext
        {
            context.SaveChanges();
        }

        public void SaveChangesAsync<TContext>(TContext context) where TContext : DbContext
        {
            context.SaveChangesAsync();
        }

        public TEntity UpdateEntity<TContext, TEntity>(TContext context, TEntity entity, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField
        {
            context.Set<TEntity>().Update(entity);
            SaveChanges(context: context, saveChanges: saveChanges);
            return entity;
        }

        public List<TEntity> UpdateMultiple<TContext, TEntity>(TContext context, List<TEntity> entities, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField
        {
            context.UpdateRange(entities: entities);
            SaveChanges(context: context, saveChanges: saveChanges);
            return entities;
        }

        public IQueryable<TEntity> UpdateMultiple<TContext, TEntity>(TContext context, IQueryable<TEntity> entities, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField
        {
            context.UpdateRange(entities: entities);
            SaveChanges(context: context, saveChanges: saveChanges);
            return entities;
        }

        #region private funcionality
        private void SaveChanges<TContext>(TContext context, bool saveChanges) where TContext : DbContext
        {
            if (saveChanges)
            {
                SaveChanges(context: context);
            }
        }
        #endregion private funcionality
    }
}