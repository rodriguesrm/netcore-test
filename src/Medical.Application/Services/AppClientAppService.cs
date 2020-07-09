using Medical.Domain.Entities;
using Medical.Domain.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Medical.Application.Services
{

    /// <summary>
    /// Transactions operations application services
    /// </summary>
    public class AppClientAppService : IAppClientAppService
    {

        #region Local objects/variables

        private IAppClientDomainService _domain;

        #endregion

        #region Constructors


        /// <summary>
        /// Create a new instante of object
        /// </summary>
        /// <param name="domain">Application client domain service instance</param>
        public AppClientAppService(IAppClientDomainService domain)
        {
            _domain = domain;
        }

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public async Task<bool> Authenticate(Guid appKey, string appSecret)
        {
            AppClient appCli = (await _domain.GetByExpressionAsync(x => x.Id == appKey && x.Key == appSecret)).FirstOrDefault();
            return appCli != null;
        }

        #endregion

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

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
                    _domain?.Dispose();
                }

                _domain = null;

                disposedValue = true;

            }
        }

        /// <summary>
        /// Destroy this object instance
        /// </summary>
        ~AppClientAppService()
            => Dispose(false);

        ///<inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
