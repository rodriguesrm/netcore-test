using Medical.Domain.Entities;
using Medical.Domain.Models;
using Medical.Domain.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Medical.Test.Domain
{

    /// <summary>
    /// Appointment domain service tests
    /// </summary>
    public class AppointmentDomainServiceTest : AppointmentBaseTest
    {
        
        /// <summary>
        /// Create new test object instance
        /// </summary>
        public AppointmentDomainServiceTest() => PrepareMockData();

        /// <summary>
        /// List appointment for doctor test
        /// </summary>
        [Fact]
        public void ListSchedulesForDoctorTest()
        {

            IAppointmentRepository repository = Substitute.For<IAppointmentRepository>();
            repository.GetByExpressionAsync(Arg.Any<Expression<Func<Appointment, bool>>>(), Arg.Any<CancellationToken>())
                .Returns(x => _appointments.Where(w => w.DoctorId == _doctors.First().Id).AsQueryable());

            AppointmentDomainService service = new AppointmentDomainService(repository);
            IEnumerable<Appointment> result = service.ListSchedulesForDoctor(_doctors.First(), DateTime.Now.Date.AddDays(1), default).Result;

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(_doctorId1, result.First().DoctorId);

        }

        /// <summary>
        /// List appointment for patient test
        /// </summary>
        [Fact]
        public void ListSchedulesForPatientTest()
        {

            IAppointmentRepository repository = Substitute.For<IAppointmentRepository>();
            repository.GetByExpressionAsync(Arg.Any<Expression<Func<Appointment, bool>>>(), Arg.Any<CancellationToken>())
                 .Returns(x => _appointments.Where(w => w.PatientId == _patients.First().Id).AsQueryable());

            AppointmentDomainService service = new AppointmentDomainService(repository);
            IEnumerable<Appointment> result = service.ListSchedulesForPatient(_patients.First(), DateTime.Now.Date.AddDays(1), default).Result;
            
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(_patientId1, result.First().PatientId);

        }

        /// <summary>
        /// Test call ScheduleAppointment with doctor null argument
        /// </summary>
        [Fact]
        public async Task ScheduleAppointmentDoctorArgumentException()
        {

            IAppointmentRepository repository = Substitute.For<IAppointmentRepository>();

            AppointmentDomainService service = new AppointmentDomainService(repository);

            Exception ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await service.ScheduleAppointment(null, null, DateTime.Now.Date, default));
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Contains("doctor", ex.Message);

        }

        /// <summary>
        /// Test call ScheduleAppointment with patient null argument
        /// </summary>
        [Fact]
        public async Task ScheduleAppointmentPatientArgumentException()
        {

            IAppointmentRepository repository = Substitute.For<IAppointmentRepository>();

            AppointmentDomainService service = new AppointmentDomainService(repository);

            Exception ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await service.ScheduleAppointment(new Doctor(), null, DateTime.Now.Date, default));
            Assert.IsType<ArgumentNullException>(ex);
            Assert.Contains("patient", ex.Message);

        }

        /// <summary>
        /// Test call ScheduleAppointment with patient schedule conflict
        /// </summary>
        [Fact]
        public void ScheduleAppointmentPatientScheduleConflict()
        {

            IAppointmentRepository repository = Substitute.For<IAppointmentRepository>();

            AppointmentDomainService service = new AppointmentDomainService(repository);

            Doctor doctor = _doctors.FirstOrDefault(x => x.Id == _doctorId1);
            Patient patient = _patients.FirstOrDefault(x => x.Id == _patientId2);
            DateTime date = DateTime.Now.Date.AddDays(1).AddHours(14);

            repository.GetByExpressionAsync(Arg.Any<Expression<Func<Appointment, bool>>>(), Arg.Any<CancellationToken>())
                .Returns(x => _appointments.Where(a => a.DateTime == date && (a.DoctorId == doctor.Id || a.PatientId == patient.Id)).AsQueryable());

            ScheduleResult result = service.ScheduleAppointment(doctor, patient, date, default).Result;
            Assert.NotNull(result);
            Assert.False(result.Sucess);
            Assert.Contains("conflict", result.Message);

        }

        /// <summary>
        /// Test call ScheduleAppointment with date/time doctor not available
        /// </summary>
        [Fact]
        public void ScheduleAppointmentDoctorDateTimeNotAvailable()
        {

            IAppointmentRepository repository = Substitute.For<IAppointmentRepository>();

            AppointmentDomainService service = new AppointmentDomainService(repository);

            Doctor doctor = _doctors.FirstOrDefault(x => x.Id == _doctorId1);
            Patient patient = _patients.FirstOrDefault(x => x.Id == _patientId1);
            DateTime date = DateTime.Now.Date.AddDays(1).AddHours(14);

            repository.GetByExpressionAsync(Arg.Any<Expression<Func<Appointment, bool>>>(), Arg.Any<CancellationToken>())
                .Returns(x => _appointments.Where(a => a.DateTime == date && (a.DoctorId == doctor.Id || a.PatientId == patient.Id)).AsQueryable());

            ScheduleResult result = service.ScheduleAppointment(doctor, patient, date, default).Result;
            Assert.NotNull(result);
            Assert.False(result.Sucess);

            Assert.Contains("available", result.Message);

        }

        /// <summary>
        /// Test sucessful call ScheduleAppointment
        /// </summary>
        [Fact]
        public void ScheduleAppointmentSucess()
        {

            IAppointmentRepository repository = Substitute.For<IAppointmentRepository>();

            AppointmentDomainService service = new AppointmentDomainService(repository);

            Doctor doctor = _doctors.FirstOrDefault(x => x.Id == _doctorId1);
            Patient patient = _patients.FirstOrDefault(x => x.Id == _patientId1);
            DateTime date = DateTime.Now.Date.AddDays(1).AddHours(15);

            Appointment appointment = new Appointment()
            {
                DateTime = date,
                DoctorId = doctor.Id,
                Doctor = doctor,
                PatientId = patient.Id,
                Patient = patient
            };

            repository.GetByExpressionAsync(Arg.Any<Expression<Func<Appointment, bool>>>(), Arg.Any<CancellationToken>())
                .Returns(x => _appointments.Where(a => a.DateTime == date && (a.DoctorId == doctor.Id || a.PatientId == patient.Id)).AsQueryable());

            repository.AddAsync(Arg.Any<Appointment>(), Arg.Any<CancellationToken>())
                .Returns(appointment);

            ScheduleResult result = service.ScheduleAppointment(doctor, patient, date, default).Result;

            Assert.NotNull(result);
            Assert.True(result.Sucess);
            Assert.NotNull(result.Appointment);

        }

    }

}
