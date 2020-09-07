using System;
using System.Collections.Generic;
using System.Linq;
using eShop.Common.HelperFields;
using eShop.InternalServer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eShop.InternalServer.Services
{
    public class Persistence : IPersistence
    {
        private readonly ILogger<Persistence> _logger;
        private readonly IAudience _audience;
        private readonly IDatabase _database;

        public Persistence(ILogger<Persistence> logger, IDatabase database, IAudience audience)
        {
            _logger = logger;
            _database = database;
            _audience = audience;
        }

        public TEntity AddEntity<TContext, TEntity>(TContext context, TEntity entity, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField
        {
            AddBefore(entity: entity);
            return _database.AddEntity(context: context, entity: entity, saveChanges: saveChanges);
        }

        public TEntity AddEntityAsync<TContext, TEntity>(TContext context, TEntity entity, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField
        {
            AddBefore(entity: entity);
            return _database.AddEntityAsync(context: context, entity: entity, saveChanges: saveChanges);
        }

        public TEntity DeleteEntity<TContext, TEntity>(TContext context, Guid externalId, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField
        {            
            return _database.DeleteEntity<TContext, TEntity>(context: context, externalId: externalId, saveChanges: saveChanges);
        }

        public TEntity DeleteEntity<TContext, TEntity>(TContext context, long id, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField
        {
            return _database.DeleteEntity<TContext, TEntity>(context: context, id: id, saveChanges: saveChanges);
        }

        public TEntity DeleteEntity<TContext, TEntity>(TContext context, TEntity entity, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField
        {
            return _database.DeleteEntity<TContext, TEntity>(context: context, entity: entity, saveChanges: saveChanges);
        }

        public IQueryable<TEntity> GetEntities<TContext, TEntity>(TContext context) where TContext : DbContext where TEntity : BaseField
        {
            var entities = _database.GetEntities<TContext, TEntity>(context: context);
            return entities;
        }

        public TEntity GetEntity<TContext, TEntity>(TContext context, Guid externalId) where TContext : DbContext where TEntity : BaseField
        {
            var entity = _database.GetEntity<TContext, TEntity>(context: context, externalId: externalId);
            return entity;
        }

        public TEntity GetEntity<TContext, TEntity>(TContext context, long id) where TContext : DbContext where TEntity : BaseField
        {
            var entity = _database.GetEntity<TContext, TEntity>(context: context, id: id);
            return entity;
        }

        public void SaveChanges<TContext>(TContext context) where TContext : DbContext
        {
            _database.SaveChanges(context: context);
        }

        public void SaveChangesAsync<TContext>(TContext context) where TContext : DbContext
        {
            _database.SaveChangesAsync(context: context);
        }

        public TEntity UpdateEntity<TContext, TEntity>(TContext context, TEntity entity, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField
        {
            UpdateBefore(entity: entity);
            return _database.UpdateEntity(context: context, entity: entity, saveChanges: saveChanges);
        }

        public List<TEntity> UpdateMultiple<TContext, TEntity>(TContext context, List<TEntity> entities, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField
        {
            foreach (var entity in entities)
            {
                UpdateBefore(entity: entity);
            }
            return _database.UpdateMultiple(context: context, entities: entities, saveChanges: saveChanges);
        }

        public IQueryable<TEntity> UpdateMultiple<TContext, TEntity>(TContext context, IQueryable<TEntity> entities, bool saveChanges = false) where TContext : DbContext where TEntity : BaseField
        {
            foreach (var entity in entities)
            {
                UpdateBefore(entity: entity);
            }
            return _database.UpdateMultiple(context: context, entities: entities, saveChanges: saveChanges);
        }


        #region private functions
        private TEntity AddBefore<TEntity>(TEntity entity) where TEntity : BaseField
        {
            var currentUser = _audience.EntityId();
            if (currentUser > 0)
            {
                entity.CreatedById = currentUser;
            }
            entity.Active = true;
            entity.CreatedOn = DateTime.UtcNow;
            entity.ExternalId = Guid.NewGuid();
            return entity;
        }


        private TEntity UpdateBefore<TEntity>(TEntity entity) where TEntity : BaseField
        {
            var currentUser = _audience.EntityId();
            if (currentUser > 0)
            {
                entity.ModifiedById = currentUser;
            }
            entity.ModifiedOn = DateTime.UtcNow;
            return entity;
        }

        #endregion private functions
    }
}