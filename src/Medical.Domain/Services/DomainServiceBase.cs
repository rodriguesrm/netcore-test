using Medical.CrossCutting.Common.Entities;
using Medical.CrossCutting.Common.Services;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Medical.Domain.Services
{

    /// <summary>
    /// Abstract class domain service
    /// </summary>
    /// <typeparam name="TEntity">Entity tipe</typeparam>
    /// <typeparam name="TRepository">Repository type</typeparam>
    public abstract class DomainServiceBase<TEntity, TRepository> : IDomainServiceBase<TEntity>
        where TEntity : EntityBase<TEntity>
        where TRepository : class, IRepositoryBase<TEntity>
    {

        #region Local objects/variables

        protected TRepository _repository;

        #endregion

        #region Construtores

        /// <inheritdoc />
        public DomainServiceBase(TRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity.Invalid) return entity;
            if (cancellationToken.IsCancellationRequested)
            {
                entity.AddNotification(entity.GetName(), "Operação foi cancelada");
                return entity;
            }
            return await _repository.AddAsync(entity, cancellationToken);
        }


        ///<inheritdoc/>
        public TEntity Update(TEntity entity)
        {
            if (entity.Invalid) return entity;
            return _repository.Update(entity);
        }

        ///<inheritdoc/>
        public async Task<IQueryable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _repository.GetAllAsync(cancellationToken);

        ///<inheritdoc/>
        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _repository.GetByIdAsync(id, cancellationToken);

        ///<inheritdoc/>
        public async Task<IQueryable<TEntity>> GetByExpressionAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => await _repository.GetByExpressionAsync(predicate, cancellationToken);

        ///<inheritdoc/>
        public void Delete(TEntity entity)
            => _repository.Delete(entity);

        #endregion

        #region IDisposable Support

        private bool disposedValue = false; // Para detectar chamadas redundantes

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
                    _repository?.Dispose();
                }

                _repository = null;

                disposedValue = true;
            }
        }

        /// <summary>
        /// Destroy this object instance
        /// </summary>
        ~DomainServiceBase()
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
