using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using App.Data.Entities.Base;
using App.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq;

namespace Testing.Common.Helpers
{
    public static class MockDbContextBuilder
    {
        public static Mock<DbSet<TEntity>> BuildMockDbSet<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityBase
        {
            var mockDbSet = new Mock<DbSet<TEntity>>();
            var queryable = entities.AsQueryable();
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            return mockDbSet;
        }

        public static Mock<TDbContext> BuildMockDbContext<TDbContext>() where TDbContext : class, IDbContext
        {
            return new Mock<TDbContext>();
        }

        /// <summary>
        /// Modifies a DbContext Set-method call to return a mocked DbSet instance.
        /// </summary>
        /// <typeparam name="TDbContext">Target context type</typeparam>
        /// <typeparam name="TEntity">Target entity type</typeparam>
        /// <param name="context">Target context instance</param>
        /// <param name="mockDbSet">Mocked DbSet for TEntity type entities</param>
        /// <returns>The modified DbContext instance</returns>
        public static Mock<TDbContext> AttachMockDbSetToSetMethodCall<TDbContext, TEntity>(this Mock<TDbContext> context,
            Mock<DbSet<TEntity>> mockDbSet)
            where TDbContext : class, IDbContext
            where TEntity : EntityBase
        {
            context.Setup(x => x.Set<TEntity>()).Returns(mockDbSet.Object);
            return context;
        }

        /// <summary>
        /// Modifies a DbContext Model with a specified index key to return a mocked DbSet instance.
        /// </summary>
        /// <typeparam name="TDbContext">Target context type</typeparam>
        /// <typeparam name="TEntity">Target entity type</typeparam>
        /// <param name="context">Target context instance</param>
        /// <param name="mockDbSet">Mocked DbSet for TEntity type entities</param>
        /// <param name="modelKey">Model index key for the attached DbSet</param>
        /// <returns>The modified DbContext instance</returns>
        public static Mock<TDbContext> AttachMockDbSetToModelCall<TDbContext, TEntity>(this Mock<TDbContext> context,
            Mock<DbSet<TEntity>> mockDbSet,
            string modelKey)
            where TDbContext : class, IDbContext
            where TEntity : class
        {
            var mockModel = new Mock<IModel>();
            mockModel.Setup(x => x[modelKey]).Returns(mockDbSet.Object);

            context.Setup(x => x.Model).Returns(mockModel.Object);
            return context;
        }

        /// <summary>
        /// Modifies a DbContext property to return a mocked DbSet.
        /// </summary>
        /// <typeparam name="TDbContext">Target context type</typeparam>
        /// <typeparam name="TEntity">Target entity type</typeparam>
        /// <param name="context">Target context instance</param>
        /// <param name="mockDbSet">Mocked DbSet for TEntity type entities</param>
        /// <param name="property">Expression for accessing the target DbSet property</param>
        /// <returns>The modified DbContext instance</returns>
        public static Mock<TDbContext> AttachMockDbSetToPropertyCall<TDbContext, TEntity>(this Mock<TDbContext> context,
            Mock<DbSet<TEntity>> mockDbSet, Expression<Func<TDbContext, DbSet<TEntity>>> property)
            where TDbContext : class, IDbContext
            where TEntity : EntityBase
        {
            context.Setup(property).Returns(mockDbSet.Object);
            return context;
        }
    }
}
