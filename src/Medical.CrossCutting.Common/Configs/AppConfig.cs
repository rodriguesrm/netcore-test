using System.Collections.Generic;

namespace Medical.CrossCutting.Common.Configs
{

    /// <summary>
    /// Application config data
    /// </summary>
    public class AppConfig
    {

        /// <summary>
        /// Connection strings
        /// </summary>
        public IEnumerable<ConnectionStringsConfig> ConnectionStrings { get; set; }

        /// <summary>
        /// Opening hours config data
        /// </summary>
        public OpeningHoursConfig OpeningHours { get; set; }

    }
}
