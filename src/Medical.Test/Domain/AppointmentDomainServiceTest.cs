using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Medical.Domain.Entities;
using Medical.Domain.Models;
using Medical.Domain.Services;
using NSubstitute;
using NSubstitute.Extensions;
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
    public class AppointmentDomainServiceTest
    {
        
        private Guid _patientId1 = new Guid("07807564-6EAC-4223-A621-6566B79E8461");
        private Guid _patientId2 = new Guid("BD3247B9-73AE-40BB-9C87-CC324EF9E448");
        private Guid _patientId3 = new Guid("55AA4D7C-035D-4BD3-8605-9032F0EC3E4C");

        private Guid _doctorId1 = new Guid("137E949E-64F6-453C-9C11-5B040F85F955");
        private Guid _doctorId2 = new Guid("7489E258-A147-4ACB-92AB-B029A0994D96");
        private Guid _doctorId3 = new Guid("B2AE7259-C895-4AC4-8C88-E34CDE0E5726");

        private Guid _appointmentId1 = new Guid("52F42569-2EE3-4560-8345-AC75DCB5FD66");
        private Guid _appointmentId2 = new Guid("F2C17DEA-A6B9-47DC-AD96-7AF2071DE7CA");
        private Guid _appointmentId3 = new Guid("1AC86417-7C38-4D7D-92F5-7A8DB852123B");

        IEnumerable<Doctor> _doctors;
        IEnumerable<Patient> _patients;
        IList<Appointment> _appointments;

        /// <summary>
        /// Prepara mock entities data
        /// </summary>
        private void PrepareMockData()
        {

            _doctors = new List<Doctor>()
            {
                new Doctor() { Id = _doctorId1, FirstName = "JOAO", LastName = "CARLOS", Crm = "10001/2001" },
                new Doctor() { Id = _doctorId2, FirstName = "MARIA", LastName = "ANTONIA", Crm = "20002/2001" },
                new Doctor() { Id = _doctorId3, FirstName = "JOSE", LastName = "ALBUQUERQUE", Crm = "30003/2001" }
            };

            _patients = new List<Patient>()
            {
                new Patient() { Id = _patientId1, Cpf = "71310656509", FirstName = "AYRTON", LastName = "DA SILVA" },
                new Patient() { Id = _patientId2, Cpf = "29397818260", FirstName = "LUISA", LastName = "DE OLIVEIRA" },
                new Patient() { Id = _patientId3, Cpf = "84259471112", FirstName = "RODRIGO", LastName = "RODRIGUES" }
            };

            DateTime date = DateTime.Now.Date.AddDays(1).AddHours(14);

            _appointments = new List<Appointment>()
            {
                new Appointment() { Id = _appointmentId1, DateTime = date, DoctorId = _doctorId1, Doctor = _doctors.Where(x => x.Id == _doctorId1).First(), PatientId = _patientId2, Patient = _patients.Where(x => x.Id == _patientId1).First() },
                new Appointment() { Id = _appointmentId2, DateTime = date, DoctorId = _doctorId2, Doctor = _doctors.Where(x => x.Id == _doctorId2).First(), PatientId = _patientId3, Patient = _patients.Where(x => x.Id == _patientId2).First() },
                new Appointment() { Id = _appointmentId3, DateTime = date.AddMinutes(30), DoctorId = _doctorId3, Doctor = _doctors.Where(x => x.Id == _doctorId3).First(), PatientId = _patientId1, Patient = _patients.Where(x => x.Id == _patientId3).First() }
            };

        }

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
