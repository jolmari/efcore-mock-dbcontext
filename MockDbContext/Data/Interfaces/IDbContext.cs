using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace App.Data.Interfaces
{
    /// <summary>
    ///     Defines operations implemented by all database contexts.
    /// </summary>
    public interface IDbContext : IDisposable
    {
        IModel Model { get; }
        EntityEntry Entry (object entity);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Add<TEntity> (TEntity entity) where TEntity : class;
        EntityEntry<TEntity> Remove<TEntity> (TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
    }
}