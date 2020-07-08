using Medical.Domain.Entities;

namespace Medical.Domain.Services
{

    /// <summary>
    /// AppClient domain service operations
    /// </summary>
    public class AppClientDomainService : DomainServiceBase<AppClient, IAppClientRepository>, IAppClientDomainService
    {

        #region Constructors

        /// <summary>
        /// Create a new AppClient domain service
        /// </summary>
        /// <param name="repository">Customer repository instance</param>
        public AppClientDomainService(IAppClientRepository repository) : base(repository) { }

        #endregion

    }

}
