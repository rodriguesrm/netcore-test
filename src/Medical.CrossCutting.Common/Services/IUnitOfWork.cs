using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medical.CrossCutting.Common.Services
{

    // <summary>
    /// Unit of work interface (transaction)
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {

        #region Properties

        /// <summary>
        /// Indicates whether the transaction was initiated
        /// </summary>
        bool TransactionStarted { get; }

        #endregion

        #region Public methods

        /// <summary>
        /// Saves all changes made in this context to the database
        /// </summary>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Start a transaction
        /// </summary>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Commit transaction
        /// </summary>
        void Commit();

        /// <summary>
        /// Commit transaction
        /// </summary>
        void RollBack();

        #endregion

    }

}
