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
    public interface IAppointmentDomainService
    {

        /// <summary>
        /// Schedule an appointment
        /// </summary>
        /// <param name="doctorId">Doctor identification guid</param>
        /// <param name="patientId">Patient identification guid</param>
        /// <param name="dateTime">Date/time to schedule</param>
        /// <param name="cancellationToken">Cancelation token</param>
        Task<ScheduleResult> ScheduleAppointment(Guid doctorId, Guid patientId, DateTime dateTime, CancellationToken cancellationToken = default);

        /// <summary>
        /// List a doctor's appointments from the date informed
        /// </summary>
        /// <param name="doctorId">Doctor identification guid</param>
        /// <param name="dateTime">Date/time to list</param>
        Task<IEnumerable<Appointment>> ListSchedulesForDoctor(Guid doctorId, DateTime dateTime);

        /// <summary>
        /// List a doctor's appointments from today
        /// </summary>
        /// <param name="doctorId">Doctor identification guid</param>
        Task<IEnumerable<Appointment>> ListSchedulesForDoctor(Guid doctorId);

    }
}