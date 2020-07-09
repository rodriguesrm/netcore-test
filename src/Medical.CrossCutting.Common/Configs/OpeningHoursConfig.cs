using System.Collections.Generic;

namespace Medical.CrossCutting.Common.Configs
{

    /// <summary>
    /// Opening hours config
    /// </summary>
    public class OpeningHoursConfig
    {

        /// <summary>
        /// Valid hours
        /// </summary>
        public IEnumerable<int> Hours { get; set; }

        /// <summary>
        /// Valid minutes
        /// </summary>
        public IEnumerable<int> Minutes { get; set; }

    }
}
