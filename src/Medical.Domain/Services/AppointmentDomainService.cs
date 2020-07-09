using Medical.Domain.Entities;
using Medical.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medical.Domain.Services
{

    /// <summary>
    /// Appointment domain service
    /// </summary>
    public class AppointmentDomainService : IAppointmentDomainService
    {

        #region Local objects/variables

        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new domain service instance object
        /// </summary>
        public AppointmentDomainService
        (
            IAppointmentRepository appointmentRepository,
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository
        )
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public async Task<ScheduleResult> ScheduleAppointment(Guid doctorId, Guid patientId, DateTime dateTime, CancellationToken cancellationToken = default)
        {

            // Find doctor;patient
            Doctor doctor = await _doctorRepository.GetByIdAsync(doctorId, cancellationToken);
            if (doctor == null)
            {
                return new ScheduleResult("Doctor not found");
            }

            Patient patient = await _patientRepository.GetByIdAsync(patientId, cancellationToken);
            if (patient == null)
            {
                return new ScheduleResult("Patient not found");
            }

            // Check schedule is free
            Appointment checkAppointment = (
                await _appointmentRepository.GetByExpressionAsync(x =>
                    x.DoctorId == doctorId &&
                    x.DateTime == dateTime
                )).FirstOrDefault();

            if (checkAppointment != null)
            {
                return new ScheduleResult("Date/time not available");
            }

            Appointment appointment = await _appointmentRepository.AddAsync(new Appointment()
            {
                DoctorId = doctorId,
                PatientId = patientId,
                DateTime = dateTime
            }, cancellationToken);

            return new ScheduleResult(appointment);

        }

        /// <summary>
        /// List a doctor's appointments from the date informed
        /// </summary>
        /// <param name="doctorId">Doctor identification guid</param>
        /// <param name="dateTime">Date/time to list</param>
        public async Task<IEnumerable<Appointment>> ListSchedulesForDoctor(Guid doctorId, DateTime dateTime)
        {
            throw new NotImplementedException();
            //TODO: NotImplementedException
        }

        /// <summary>
        /// List a doctor's appointments from today
        /// </summary>
        /// <param name="doctorId">Doctor identification guid</param>
        public async Task<IEnumerable<Appointment>> ListSchedulesForDoctor(Guid doctorId)
        {
            throw new NotImplementedException();
            //TODO: NotImplementedException
        }

        #endregion

    }
}
