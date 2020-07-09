using System;

namespace Medical.Application.Models
{

    /// <summary>
    /// Scheduler list result for doctor
    /// </summary>
    public class AppointmentForDoctorResult
    {

        /// <summary>
        /// Date/Time appointment scheduled
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Patient data
        /// </summary>
        public SingleIdentificationResult Patient { get; set; }

    }
}
