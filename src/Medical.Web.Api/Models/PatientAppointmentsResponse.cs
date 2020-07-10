using System;

namespace Medical.Web.Api.Models
{

    /// <summary>
    /// Patient appointment response
    /// </summary>
    public class PatientAppointmentsResponse
    {

        /// <summary>
        /// Appointment date/time
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Patient data
        /// </summary>
        public SimplePersonResponse Doctor { get; set; }

    }
}
