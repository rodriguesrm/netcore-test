using System;
using System.Threading.Tasks;

namespace Medical.Application.Services
{

    public interface IAppClientAppService : IDisposable
    {

        /// <summary>
        /// Authenticate application client
        /// </summary>
        /// <param name="appKey">Application access key</param>
        /// <param name="appSecret">Application access secret</param>
        Task<bool> Authenticate(Guid appKey, string appSecret);

    }
}
