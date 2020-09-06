using System;
using System.Collections.Generic;
using System.Linq;
using eShop.Common.HelperFields;
using eShop.InternalServer.Interfaces;
using eShop.ProductEntities.Context;
using Microsoft.Extensions.Logging;

namespace eShop.Product.API.Logics
{
    public class MicroKernel : IMicroKernel
    {
        private readonly ILogger<MicroKernel> _logger;
        private readonly IPersistence _persistence;
        private readonly ProductContext _context;
        public MicroKernel(ILogger<MicroKernel> logger, IPersistence persistence, ProductContext context)
        {
            _logger = logger;
            _persistence = persistence;
            _context = context;
        }

        public TEntity AddEntity<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : BaseField
        {
            entity = _persistence.AddEntity(context: _context, entity: entity, saveChanges: saveChanges);
            return entity;
        }

        public TEntity AddEntityAsync<TContext, TEntity>(TContext context, TEntity entity, bool saveChanges = false) where TEntity : BaseField
        {
            entity = _persistence.AddEntityAsync(context: _context, entity: entity, saveChanges: saveChanges);
            return entity;
        }

        public TEntity DeleteEntity<TEntity>(Guid externalId, bool saveChanges = false) where TEntity : BaseField
        {
            var entity = _persistence.DeleteEntity<ProductContext, TEntity>(context: _context, externalId: externalId, saveChanges: saveChanges);
            return entity;
        }

        public TEntity DeleteEntity<TEntity>(long id, bool saveChanges = false) where TEntity : BaseField
        {
            var entity = _persistence.DeleteEntity<ProductContext, TEntity>(context: _context, id: id, saveChanges: saveChanges);
            return entity;
        }

        public TEntity DeleteEntity<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : BaseField
        {
            entity = _persistence.DeleteEntity(context: _context, entity: entity, saveChanges: saveChanges);
            return entity;
        }

        public IQueryable<TEntity> GetEntities<TEntity>() where TEntity : BaseField
        {
            var entities = _persistence.GetEntities<ProductContext, TEntity>(context: _context);
            return entities;
        }

        public TEntity GetEntity<TEntity>(Guid externalId) where TEntity : BaseField
        {
            var entity = _persistence.GetEntity<ProductContext, TEntity>(context: _context, externalId: externalId);
            return entity;
        }

        public TEntity GetEntity<TEntity>(long id) where TEntity : BaseField
        {
            var entity = _persistence.GetEntity<ProductContext, TEntity>(context: _context, id: id);
            return entity;
        }

        public void SaveChanges()
        {
            _persistence.SaveChanges(context: _context);
        }

        public void SaveChangesAsync()
        {
            _persistence.SaveChangesAsync(context: _context);
        }

        public TEntity UpdateEntity<TEntity>(TEntity entity, bool saveChanges = false) where TEntity : BaseField
        {
            entity = _persistence.UpdateEntity(context: _context, entity: entity, saveChanges: saveChanges);
            return entity;
        }

        public List<TEntity> UpdateMultiple<TEntity>(List<TEntity> entities, bool saveChanges = false) where TEntity : BaseField
        {
            entities = _persistence.UpdateMultiple(context: _context, entities: entities, saveChanges: saveChanges);
            return entities;
        }

        public IQueryable<TEntity> UpdateMultiple<TEntity>(IQueryable<TEntity> entities, bool saveChanges = false) where TEntity : BaseField
        {
            entities = _persistence.UpdateMultiple(context: _context, entities: entities, saveChanges: saveChanges);
            return entities;
        }
    }
}
