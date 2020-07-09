using System;

namespace Medical.Application.Models
{

    /// <summary>
    /// Schedule appointment result model
    /// </summary>
    public class ScheduleAppointmentResult : BaseResult
    {

        /// <summary>
        /// Appointment identification id
        /// </summary>
        public Guid? AppointmentId { get; set; }

    }
}
