using Medical.CrossCutting.Common.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Medical.CrossCutting.Common.Services
{

    /// <summary>
    /// Common methos and properties of domains and repositories
    /// </summary>
    public interface ICommonBase<TEntity> : IDisposable
        where TEntity : EntityBase<TEntity>
    {

        /// <summary>
        /// Add entity to context
        /// </summary>
        /// <param name="entity">Entity instance</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update entity on context
        /// </summary>
        /// <param name="entity">Entity instance</param>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Get all rows
        /// </summary>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        Task<IQueryable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get row by id
        /// </summary>
        /// <param name="id">Row id</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all record by lambda expression
        /// </summary>
        /// <param name="predicate">Lambda expression</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        Task<IQueryable<TEntity>> GetByExpressionAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove entity of context
        /// </summary>
        /// <param name="entity">Entity instance</param>
        void Delete(TEntity entity);

    }

}
