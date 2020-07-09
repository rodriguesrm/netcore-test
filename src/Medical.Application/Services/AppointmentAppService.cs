using Medical.Application.Models;
using Medical.CrossCutting.Common.Configs;
using Medical.CrossCutting.Common.Services;
using Medical.Domain.Entities;
using Medical.Domain.Models;
using Medical.Domain.Services;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medical.Application.Services
{

    /// <summary>
    /// Appointment application service
    /// </summary>
    public class AppointmentAppService : IAppointmentAppService
    {

        #region Local objects/variables

        private IUnitOfWork _work;
        private IAppointmentDomainService _domainService;
        private IDoctorDomainService _doctorDomain;
        private IPatientDomainService _patientDomain;
        private OpeningHoursConfig _config;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new appointment application service instance
        /// </summary>
        public AppointmentAppService
        (
            IUnitOfWork work,
            IAppointmentDomainService domainService,
            IDoctorDomainService doctorDomain,
            IPatientDomainService patientDomain,
            IOptions<OpeningHoursConfig> options
        )
        {
            _work = work;
            _domainService = domainService;
            _doctorDomain = doctorDomain;
            _patientDomain = patientDomain;
            _config = options?.Value;
        }

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public async Task<ScheduleAppointmentResult> ScheduleAppointment(ScheduleAppointmentArgs args, CancellationToken cancellationToken = default)
        {

            ScheduleAppointmentResult result = new ScheduleAppointmentResult();

            Doctor doctor = await _doctorDomain.GetByIdAsync(args.DoctorId);
            Patient patient = await _patientDomain.GetByIdAsync(args.PatientId);

            if (doctor == null)
                result.Messages.Add("doctor", "Doctor not found");

            if (patient == null)
                result.Messages.Add("patient", "Patient not found");

            if (!_config.Hours.Contains(args.Hour))
            {
                result.Messages.Add("hour", "Invalid hour");
            }

            if (!_config.Minutes.Contains(args.Minute))
            {
                result.Messages.Add("minute", "Invalid minute");
            }

            if (result.Valid)
            {

                DateTime dateTime = new DateTime(args.Date.Year, args.Date.Month, args.Date.Day, args.Hour, args.Minute, 0);
                ScheduleResult resp = await _domainService.ScheduleAppointment(doctor, patient, dateTime, cancellationToken);

                result.Sucess = resp.Sucess;
                result.Messages.Add("operation", resp.Message);

                if (result.Sucess)
                    await _work.SaveChangesAsync(cancellationToken);

                result.AppointmentId = resp.Appointment.Id;

            }

            return result;

        }

        #endregion

        #region IDisposable Support

        private bool disposedValue;

        /// <summary>
        /// Release resources
        /// </summary>
        /// <param name="disposing">Dispose object flags</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _work?.Dispose();
                    _domainService?.Dispose();
                    _doctorDomain?.Dispose();
                    _patientDomain?.Dispose();
                }

                _work = null;
                _domainService = null;
                _doctorDomain = null;
                _patientDomain = null;
                disposedValue = true;
                _config = null;
            }
        }

        /// <summary>
        /// override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        /// </summary>
        ~AppointmentAppService()
        {
            Dispose(disposing: false);
        }

        ///<inheritdoc/>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion



    }

}
