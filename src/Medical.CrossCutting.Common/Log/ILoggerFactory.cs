using Serilog;

namespace Medical.CrossCutting.Common.Log
{

    /// <summary>
    /// Logger factory interface
    /// </summary>
    public interface ILoggerFactory
    {

        /// <summary>
        /// Create a logger instance
        /// </summary>
        ILogger Create();

    }
}
