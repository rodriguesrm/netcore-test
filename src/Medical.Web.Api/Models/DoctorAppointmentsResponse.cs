using System;

namespace Medical.Web.Api.Models
{

    /// <summary>
    /// Doctor appointment response
    /// </summary>
    public class DoctorAppointmentsResponse
    {

        /// <summary>
        /// Appointment date/time
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Patient data
        /// </summary>
        public SimplePersonResponse Patient { get; set; }

    }
}
