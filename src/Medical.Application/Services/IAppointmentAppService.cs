using Medical.Application.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medical.Application.Services
{
    public interface IAppointmentAppService : IDisposable
    {

        /// <summary>
        /// Schedule an appointment
        /// </summary>
        /// <param name="args">Arguments data object</param>
        /// <param name="cancellationToken">Cancellation task token</param>
        Task<ScheduleAppointmentResult> ScheduleAppointment(ScheduleAppointmentArgs args, CancellationToken cancellationToken = default);

        /// <summary>
        /// List a doctor's appointments from the date informed
        /// </summary>
        /// <param name="doctorId">Doctor id</param>
        /// <param name="dateTime">Date/time to list</param>
        /// <param name="cancellationToken">Cancellation task token</param>
        Task<ListSchedulesAppointmentForDoctorResult> ListSchedulesForDoctor(Guid doctorId, DateTime dateTime, CancellationToken cancellationToken = default);

        /// <summary>
        /// List a doctor's appointments from the date informed
        /// </summary>
        /// <param name="doctorId">Doctor id</param>
        /// <param name="cancellationToken">Cancellation task token</param>
        Task<ListSchedulesAppointmentForDoctorResult> ListSchedulesForDoctor(Guid doctorId, CancellationToken cancellationToken = default);

        /// <summary>
        /// List a patient's appointments from the date informed
        /// </summary>
        /// <param name="patientId">Patient id</param>
        /// <param name="dateTime">Date/time to list</param>
        /// <param name="cancellationToken">Cancellation task token</param>
        Task<ListSchedulesAppointmentForPatientResult> ListSchedulesForPatient(Guid patientId, DateTime dateTime, CancellationToken cancellationToken = default);

        /// <summary>
        /// List a patient's appointments from the date informed
        /// </summary>
        /// <param name="patientId">Patient id</param>
        /// <param name="cancellationToken">Cancellation task token</param>
        Task<ListSchedulesAppointmentForPatientResult> ListSchedulesForPatient(Guid patientId, CancellationToken cancellationToken = default);

    }
}