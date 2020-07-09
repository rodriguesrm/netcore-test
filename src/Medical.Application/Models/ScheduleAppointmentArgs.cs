using System;

namespace Medical.Application.Models
{

    /// <summary>
    /// Schedule arguments model
    /// </summary>
    public class ScheduleAppointmentArgs
    {

        /// <summary>
        /// Doctor identification id
        /// </summary>
        public Guid DoctorId { get; set; }

        /// <summary>
        /// Patient identification id
        /// </summary>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Appointment date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Hour to appointment schedule.
        /// </summary>
        public int Hour { get; set; }

        /// <summary>
        /// Minut to appointment schedule.
        /// </summary>
        public int Minute { get; set; }

    }
}
