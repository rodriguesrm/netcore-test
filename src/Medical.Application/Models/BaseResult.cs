using System.Collections.Generic;

namespace Medical.Application.Models
{

    /// <summary>
    /// Base result model
    /// </summary>
    public abstract class BaseResult
    {

        public BaseResult()
        {
            Messages = new Dictionary<string, string>();
        }

        /// <summary>
        /// Sucess operation flag
        /// </summary>
        public bool Sucess { get; set; }

        /// <summary>
        /// Valid model flag
        /// </summary>
        public bool Valid => Messages.Count == 0;

        /// <summary>
        /// Errors/Critical messages
        /// </summary>
        public IDictionary<string, string> Messages { get; set; }

    }
}
