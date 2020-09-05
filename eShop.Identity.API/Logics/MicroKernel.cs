using System.Linq;
using eShop.Common.HelperFields;
using eShop.IdentityEntities.Context;
using eShop.InternalServer.Interfaces;
using Microsoft.Extensions.Logging;

namespace eShop.Identity.API.Logics
{
    public class MicroKernel : IMicroKernel
    {
        private readonly ILogger<MicroKernel> _logger;
        private readonly IPersistence _persistence;
        private readonly IdentityContext _context;
        public MicroKernel(ILogger<MicroKernel> logger, IPersistence persistence, IdentityContext context)
        {
            _logger = logger;
            _persistence = persistence;
            _context = context;
        }

        public TEntity AddEntity<TEntity>(TEntity entity) where TEntity : BaseField
        {
            entity = _persistence.AddEntity(context: _context, entity: entity);
            return entity;
        }

        public TEntity AddEntityAsync<TContext, TEntity>(TContext context, TEntity entity) where TEntity : BaseField
        {
            entity = _persistence.AddEntityAsync(context: _context, entity: entity);
            return entity;
        }

        public IQueryable<TEntity> GetEntities<TEntity>() where TEntity : BaseField
        {
            var entities = _persistence.GetEntities<IdentityContext, TEntity>(context: _context);
            return entities;
        }

        public void SaveChanges()
        {
            _persistence.SaveChanges<IdentityContext>(context: _context);
        }

        public void SaveChangesAsync()
        {
            _persistence.SaveChangesAsync<IdentityContext>(context: _context);
        }
    }
}
