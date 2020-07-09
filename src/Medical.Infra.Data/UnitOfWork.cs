using Medical.CrossCutting.Common.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medical.Infra.Data
{

    /// <summary>
    /// Unit of work database transaction control object
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {

        #region Local objects/variables

        private DbContext _ctx;
        private IDbContextTransaction _transaction;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new object intantance
        /// </summary>
        /// <param name="ctx">Database context</param>
        public UnitOfWork(MedicalDbContext ctx)
        {
            _ctx = ctx;
            TransactionStarted = false;
        }

        #endregion

        #region Propriedades

        /// <summary>
        /// Flag indicate if a transaction started exists
        /// </summary>
        public bool TransactionStarted { get; private set; }

        #endregion

        #region Métodos Públicos

        ///<inheritdoc/>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _ctx.SaveChangesAsync(cancellationToken);
        }

        ///<inheritdoc/>
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _ctx.Database.BeginTransactionAsync(cancellationToken)
                    .ContinueWith((tsk) =>
                    {
                        _transaction = tsk.Result;
                        TransactionStarted = true;
                    });
            }
        }

        ///<inheritdoc/>
        public void Commit()
           => _transaction.Commit();

        ///<inheritdoc/>
        public void RollBack()
            => _transaction.Rollback();

        #endregion

        #region IDisposable Support

        private bool disposedValue = false;

        /// <summary>
        /// Release resources
        /// </summary>
        /// <param name="disposing">Flag indicate disposing resources</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _ctx?.Dispose();
                    _transaction?.Dispose();
                }

                _ctx = null;
                _transaction = null;

                disposedValue = true;
            }
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        ~UnitOfWork()
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
