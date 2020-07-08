using Medical.CrossCutting.Common.Entities;
using Medical.CrossCutting.Common.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Medical.Infra.Data.Repositories
{

    /// <summary>
    /// Repository base abstract class
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : EntityBase<TEntity>
    {

        #region Local objects/variables

        /// <summary>
        /// DbContext object
        /// </summary>
        protected MedicalContext _ctx;

        /// <summary>
        /// DbSet object
        /// </summary>
        protected DbSet<TEntity> _dbSet;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new repository instance object
        /// </summary>
        public RepositoryBase(MedicalContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<TEntity>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Add a instance object in database context
        /// </summary>
        /// <param name="entity">Entity instance to add</param>
        /// <param name="cancellationToken">Cancellation task token</param>
        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            EntityEntry<TEntity> tsk = await _dbSet.AddAsync(entity, cancellationToken);
            return tsk.Entity;
        }

        /// <summary>
        /// Update a entity in database context
        /// </summary>
        /// <param name="entity">Entity instance to update</param>
        public virtual TEntity Update(TEntity entity)
            => _dbSet.Update(entity).Entity;

        /// <summary>
        /// Get all entity record from database context
        /// </summary>
        /// <param name="cancellationToken">Cancellation task token</param>
        public virtual async Task<IQueryable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await Task.Run(()
                => { return _dbSet; }, cancellationToken);
        }

        /// <summary>
        /// Get entity by key
        /// </summary>
        /// <param name="id">Entity id key value</param>
        /// <param name="cancellationToken">Cancellation task token</param>
        public virtual async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {

            if (cancellationToken.IsCancellationRequested)
                return null;

            object[] keyValues = new object[] { id };

            TEntity entity = await _dbSet.FindAsync(keyValues: keyValues, cancellationToken: cancellationToken);

            if (cancellationToken.IsCancellationRequested)
                return null;

            return entity;

        }

        /// <summary>
        /// Get entity by lamda expression
        /// </summary>
        /// <param name="predicate">Lambda expression</param>
        /// <param name="cancellationToken">Cancellation task token</param>
        public Task<IQueryable<TEntity>> GetByExpressionAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            Task<IQueryable<TEntity>> task = Task.Factory.StartNew(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                return _dbSet.Where(predicate);
            });
            return task;
        }

        /// <summary>
        /// Remove a entity from database context
        /// </summary>
        /// <param name="entity">Entity instance to remove</param>
        public virtual void Delete(TEntity entity)
            => _dbSet.Remove(entity);

        #endregion

        #region IDisposable Support

        private bool disposedValue = false;

        /// <summary>
        /// Release resources
        /// </summary>
        /// <param name="disposing">Flag indicate dispose objects</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }

                _dbSet = null;
                _ctx = null;

                disposedValue = true;
            }
        }

        /// <summary>
        /// Destroy this object instance
        /// </summary>
        ~RepositoryBase()
        {
            Dispose(false);
        }

        ///<inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }

}
