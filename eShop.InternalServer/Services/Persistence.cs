using System;
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
        private readonly IDatabase _database;
        public Persistence(ILogger<Persistence> logger, IDatabase database)
        {
            _logger = logger;
            _database = database;
        }

        public TEntity AddEntity<TContext, TEntity>(TContext context, TEntity entity)
            where TContext : DbContext
            where TEntity : BaseField
        {
            entity = AddBefore(entity: entity);
            return _database.AddEntity(context: context, entity: entity);
        }

        public TEntity AddEntityAsync<TContext, TEntity>(TContext context, TEntity entity)
            where TContext : DbContext
            where TEntity : BaseField
        {
            entity = AddBefore(entity: entity);
            return _database.AddEntityAsync(context: context, entity: entity);
        }

        public IQueryable<TEntity> GetEntities<TContext, TEntity>(TContext context)
            where TContext : DbContext
            where TEntity : BaseField
        {
            var entities = _database.GetEntities<TContext, TEntity>(context: context);
            return entities;
        }

        public void SaveChanges<TContext>(TContext context) where TContext : DbContext
        {
            _database.SaveChanges(context: context);
        }

        public void SaveChangesAsync<TContext>(TContext context) where TContext : DbContext
        {
            _database.SaveChangesAsync(context: context);
        }

        #region private functions
        private TEntity AddBefore<TEntity>(TEntity entity) where TEntity : BaseField
        {
            entity.CreatedOn = DateTime.UtcNow;
            entity.ExternalId = Guid.NewGuid();
            return entity;
        }
        #endregion private functions
    }
}
