using Microsoft.Extensions.Configuration;
using Serilog;

namespace Medical.CrossCutting.Common.Log
{

    public class AppLoggerFactory : ILoggerFactory
    {

        #region Local objects/variables

        private readonly ILogger _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instante of AppLogger
        /// </summary>
        /// <param name="configuration">Object IConfiguration injected</param>
        public AppLoggerFactory(IConfiguration configuration)
        {

            _logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

        }

        #endregion

        ///<inheritdoc/>
        public ILogger Create() => _logger;

    }
}
