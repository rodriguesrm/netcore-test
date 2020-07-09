using Medical.Domain.Entities;
using Medical.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Medical.Domain.Services
{

    /// <summary>
    /// Appointment domain service interface
    /// </summary>
    public interface IAppointmentDomainService : IDisposable
    {

        /// <summary>
        /// Schedule an appointment
        /// </summary>
        /// <param name="doctor">Doctor object instance</param>
        /// <param name="patient">Patient object instance</param>
        /// <param name="dateTime">Date/time to schedule</param>
        /// <param name="cancellationToken">Cancelation token</param>
        Task<ScheduleResult> ScheduleAppointment(Doctor doctor, Patient patient, DateTime dateTime, CancellationToken cancellationToken = default);

        /// <summary>
        /// List a doctor's appointments from the date informed
        /// </summary>
        /// <param name="doctor">Doctor object instance</param>
        /// <param name="dateTime">Date/time to list</param>
        Task<IEnumerable<Appointment>> ListSchedulesForDoctor(Doctor doctor, DateTime dateTime);

        /// <summary>
        /// List a doctor's appointments from today
        /// </summary>
        /// <param name="doctor">Doctor object instance</param>
        Task<IEnumerable<Appointment>> ListSchedulesForDoctor(Doctor doctor);

    }
}