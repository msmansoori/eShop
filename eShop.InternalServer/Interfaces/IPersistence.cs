using System;
using System.Linq;
using eShop.Common.HelperFields;
using Microsoft.EntityFrameworkCore;

namespace eShop.InternalServer.Interfaces
{
    public interface IPersistence
    {
        IQueryable<TEntity> GetEntities<TContext, TEntity>(TContext context) where TContext : DbContext where TEntity : BaseField;
      
        TEntity AddEntity<TContext, TEntity>(TContext context, TEntity entity) where TContext : DbContext where TEntity : BaseField;
        TEntity AddEntityAsync<TContext, TEntity>(TContext context, TEntity entity) where TContext : DbContext where TEntity : BaseField;

        void SaveChanges<TContext>(TContext context) where TContext : DbContext;
        void SaveChangesAsync<TContext>(TContext context) where TContext : DbContext;
    }
}
