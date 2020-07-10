using Medical.Application.Models;
using Medical.Application.Services;
using Medical.CrossCutting.Common.Configs;
using Medical.CrossCutting.Common.Services;
using Medical.Domain.Entities;
using Medical.Domain.Models;
using Medical.Domain.Services;
using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Medical.Test.Application
{
    
    /// <summary>
    /// Appointment application service test
    /// </summary>
    public class AppointmentAppServiceTest : AppointmentBaseTest
    {

        /// <summary>
        /// Create a new object test instance
        /// </summary>
        public AppointmentAppServiceTest()
        {
            PrepareMockData();
        }

        /// <summary>
        /// List schedules for doctor tests when doctor not found
        /// </summary>
        [Fact]
        public void ListSchedulesForDoctorTestWhenDoctorNotFound()
        {

            IUnitOfWork work = Substitute.For<IUnitOfWork>();
            IAppointmentDomainService domain = Substitute.For<IAppointmentDomainService>();
            IDoctorDomainService doctorDomain = Substitute.For<IDoctorDomainService>();
            IPatientDomainService patientDomain = Substitute.For<IPatientDomainService>();
            IOptions< OpeningHoursConfig > options = Substitute.For<IOptions<OpeningHoursConfig>>();

            doctorDomain.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).ReturnsNull();

            AppointmentAppService service = new AppointmentAppService(work, domain, doctorDomain, patientDomain, options);

            ListSchedulesAppointmentForDoctorResult result = service.ListSchedulesForDoctor(Guid.NewGuid(), default).Result;
            Assert.False(result.Sucess);
            Assert.Contains(result.Messages, x => x.Value.Contains("not found"));

        }

        /// <summary>
        /// List schedules for doctor tests
        /// </summary>
        [Fact]
        public void ListSchedulesForDoctorTest()
        {

            IUnitOfWork work = Substitute.For<IUnitOfWork>();
            IAppointmentDomainService domain = Substitute.For<IAppointmentDomainService>();
            IDoctorDomainService doctorDomain = Substitute.For<IDoctorDomainService>();
            IPatientDomainService patientDomain = Substitute.For<IPatientDomainService>();
            IOptions<OpeningHoursConfig> options = Substitute.For<IOptions<OpeningHoursConfig>>();

            doctorDomain.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(x => _doctors.FirstOrDefault(y => y.Id == new Guid(x[0].ToString())));

            AppointmentAppService service = new AppointmentAppService(work, domain, doctorDomain, patientDomain, options);

            ListSchedulesAppointmentForDoctorResult result = service.ListSchedulesForDoctor(_doctorId1, default).Result;
            Assert.True(result.Sucess);

        }

        /// <summary>
        /// List schedules for patient tests when patient not found
        /// </summary>
        [Fact]
        public void ListSchedulesForPatientWhenPatientNotFound()
        {

            IUnitOfWork work = Substitute.For<IUnitOfWork>();
            IAppointmentDomainService domain = Substitute.For<IAppointmentDomainService>();
            IDoctorDomainService doctorDomain = Substitute.For<IDoctorDomainService>();
            IPatientDomainService patientDomain = Substitute.For<IPatientDomainService>();
            IOptions<OpeningHoursConfig> options = Substitute.For<IOptions<OpeningHoursConfig>>();

            patientDomain.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).ReturnsNull();

            AppointmentAppService service = new AppointmentAppService(work, domain, doctorDomain, patientDomain, options);

            ListSchedulesAppointmentForPatientResult result = service.ListSchedulesForPatient(Guid.NewGuid(), default).Result;
            Assert.False(result.Sucess);
            Assert.Contains(result.Messages, x => x.Value.Contains("not found"));

        }

        /// <summary>
        /// List schedules for patient tests
        /// </summary>
        [Fact]
        public void ListSchedulesForPatientTest()
        {

            IUnitOfWork work = Substitute.For<IUnitOfWork>();
            IAppointmentDomainService domain = Substitute.For<IAppointmentDomainService>();
            IDoctorDomainService doctorDomain = Substitute.For<IDoctorDomainService>();
            IPatientDomainService patientDomain = Substitute.For<IPatientDomainService>();
            IOptions<OpeningHoursConfig> options = Substitute.For<IOptions<OpeningHoursConfig>>();

            patientDomain.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(x => _patients.FirstOrDefault(y => y.Id == new Guid(x[0].ToString())));

            AppointmentAppService service = new AppointmentAppService(work, domain, doctorDomain, patientDomain, options);

            ListSchedulesAppointmentForPatientResult result = service.ListSchedulesForPatient(_patientId1, default).Result;
            Assert.True(result.Sucess);

        }

        /// <summary>
        /// Schedule appointment critial test
        /// </summary>
        [Fact]
        public void ScheduleAppointmentDoctorCriticalTest()
        {

            IUnitOfWork work = Substitute.For<IUnitOfWork>();
            IAppointmentDomainService domain = Substitute.For<IAppointmentDomainService>();
            IDoctorDomainService doctorDomain = Substitute.For<IDoctorDomainService>();
            IPatientDomainService patientDomain = Substitute.For<IPatientDomainService>();
            IOptions<OpeningHoursConfig> options = Substitute.For<IOptions<OpeningHoursConfig>>();

            options.Value.Returns(x => new OpeningHoursConfig()
            {
                Hours = new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 },
                Minutes = new int[] { 0, 30 }
            });

            AppointmentAppService service = new AppointmentAppService(work, domain, doctorDomain, patientDomain, options);

            ScheduleAppointmentArgs args = new ScheduleAppointmentArgs()
            {
                Date = DateTime.Now.Date.AddDays(2),
                DoctorId = Guid.NewGuid(),
                PatientId = Guid.NewGuid(),
                Hour = 6,
                Minute = 45
            };

            ScheduleAppointmentResult result = service.ScheduleAppointment(args, default).Result;

            Assert.False(result.Sucess);
            Assert.Equal(4, result.Messages.Count);
            Assert.Contains(result.Messages, x => x.Key == "doctor");
            Assert.Contains(result.Messages, x => x.Key == "patient");
            Assert.Contains(result.Messages, x => x.Key == "hour");
            Assert.Contains(result.Messages, x => x.Key == "minute");


        }

        /// <summary>
        /// Schedule appointment sucess test
        /// </summary>
        [Fact]
        public void ScheduleAppointmentDoctorSucessTest()
        {

            IUnitOfWork work = Substitute.For<IUnitOfWork>();
            IAppointmentDomainService domain = Substitute.For<IAppointmentDomainService>();
            IDoctorDomainService doctorDomain = Substitute.For<IDoctorDomainService>();
            IPatientDomainService patientDomain = Substitute.For<IPatientDomainService>();
            IOptions<OpeningHoursConfig> options = Substitute.For<IOptions<OpeningHoursConfig>>();

            options.Value.Returns(x => new OpeningHoursConfig()
            {
                Hours = new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 },
                Minutes = new int[] { 0, 30 }
            });

            DateTime date = DateTime.Now.Date.AddDays(2).AddHours(8).AddMinutes(30);
            Doctor doctor = _doctors.First(x => x.Id == _doctorId1);
            Patient patient = _patients.First(x => x.Id == _patientId3);

            Appointment appointment = new Appointment()
            {
                DateTime = date,
                Doctor = doctor,
                DoctorId = doctor.Id,
                Patient = patient,
                PatientId = patient.Id
            };

            doctorDomain.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(x => _doctors.FirstOrDefault(y => y.Id == new Guid(x[0].ToString())));
            patientDomain.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(x => _patients.FirstOrDefault(y => y.Id == new Guid(x[0].ToString())));

            domain.ScheduleAppointment(Arg.Any<Doctor>(), Arg.Any<Patient>(), Arg.Any<DateTime>(), Arg.Any<CancellationToken>())
                .Returns(new ScheduleResult(appointment));

            AppointmentAppService service = new AppointmentAppService(work, domain, doctorDomain, patientDomain, options);

            ScheduleAppointmentArgs args = new ScheduleAppointmentArgs()
            {
                Date = date.Date,
                DoctorId = doctor.Id.Value,
                PatientId = patient.Id.Value,
                Hour = date.Hour,
                Minute = date.Minute
            };

            ScheduleAppointmentResult result = service.ScheduleAppointment(args, default).Result;
            Assert.True(result.Sucess);
            Assert.NotNull(result.AppointmentId);
            Assert.Equal(appointment.Id, result.AppointmentId);


        }

    }

}
