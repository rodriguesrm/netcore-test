﻿using Medical.Domain.Entities;
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

        private IAppointmentRepository _appointmentRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new domain service instance object
        /// </summary>
        public AppointmentDomainService
        (
            IAppointmentRepository appointmentRepository
        )
        {
            _appointmentRepository = appointmentRepository;
        }

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public async Task<ScheduleResult> ScheduleAppointment(Doctor doctor, Patient patient, DateTime dateTime, CancellationToken cancellationToken = default)
        {

            // Find doctor;patient
            if (doctor == null)
                throw new ArgumentNullException("doctor");

            if (patient == null)
                throw new ArgumentNullException("patient");

            // Check schedule is free
            IEnumerable<Appointment> checkList = (
                await _appointmentRepository.GetByExpressionAsync(x =>
                    x.DateTime == dateTime &&
                    (x.DoctorId == doctor.Id || x.PatientId == patient.Id)
                , cancellationToken))
                .ToList()
                ;

            Appointment checkSchedule = checkList.FirstOrDefault(x => x.PatientId == patient.Id);
            if (checkSchedule != null)
                return new ScheduleResult("Patient schedule conflict");

            checkSchedule = checkList.FirstOrDefault(x => x.DoctorId == doctor.Id);
            if (checkSchedule != null)
                return new ScheduleResult("Date/time not available for this doctor");

            Appointment appointment = await _appointmentRepository.AddAsync(new Appointment()
            {
                DoctorId = doctor.Id,
                PatientId = patient.Id,
                DateTime = dateTime
            }, cancellationToken);

            return new ScheduleResult(appointment);

        }

        ///<inheritdoc/>
        public async Task<IEnumerable<Appointment>> ListSchedulesForDoctor(Doctor doctor, DateTime dateTime, CancellationToken cancellationToken = default)
        {
            IQueryable<Appointment> query = await _appointmentRepository.GetByExpressionAsync(x => x.DoctorId == doctor.Id && x.DateTime.Date >= dateTime.Date, cancellationToken);
            if (cancellationToken.IsCancellationRequested) return null;
            return query
                .OrderBy(o => o.DateTime)
                .ToList();
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<Appointment>> ListSchedulesForPatient(Patient patient, DateTime dateTime, CancellationToken cancellationToken = default)
        {
            IQueryable<Appointment> query = await _appointmentRepository.GetByExpressionAsync(x => x.PatientId == patient.Id && x.DateTime.Date >= dateTime.Date, cancellationToken);
            if (cancellationToken.IsCancellationRequested) return null;
            return query
                .OrderBy(o => o.DateTime)
                .ToList();
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
                    _appointmentRepository?.Dispose();
                }

                _appointmentRepository = null;
                disposedValue = true;
            }
        }

        /// <summary>
        /// override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        /// </summary>
        ~AppointmentDomainService()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        ///<inheritdoc/>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
