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

    }
}