using System.Linq;
using eShop.Common.HelperFields;
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

        public TEntity AddEntity<TContext, TEntity>(TContext context, TEntity entity)
            where TContext : DbContext
            where TEntity : BaseField
        {
            context.Set<TEntity>().Add(entity);
            SaveChanges(context: context);
            return entity;
        }

        public TEntity AddEntityAsync<TContext, TEntity>(TContext context, TEntity entity)
          where TContext : DbContext
          where TEntity : BaseField
        {
            context.Set<TEntity>().AddAsync(entity);
            SaveChangesAsync(context: context);
            return entity;
        }

        public IQueryable<TEntity> GetEntities<TContext, TEntity>(TContext context)
          where TContext : DbContext
          where TEntity : BaseField
        {
            var entities = context.Set<TEntity>().Where(entity => entity.Active);
            return entities;
        }

        public void SaveChanges<TContext>(TContext context) where TContext : DbContext
        {
            context.SaveChanges();
        }

        public void SaveChangesAsync<TContext>(TContext context) where TContext : DbContext
        {
            context.SaveChangesAsync();
        }
    }
}