using Medical.Domain.Entities;
using Medical.Domain.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace Medical.Test.Domain
{

    /// <summary>
    /// Doctor domain service tests
    /// </summary>
    public class DoctorDomainServiceTest
    {

        private Doctor GetDoctor()
            => new Doctor()
            {
                FirstName = "OLIVER",
                LastName = "QUEEN",
                Crm = "10001/2001"
            };

        /// <summary>
        /// Doctor add test
        /// </summary>
        [Fact]
        public void AddDoctorTest()
        {

            Doctor doctor = GetDoctor();
            IDoctorRepository repository = Substitute.For<IDoctorRepository>();
            repository.AddAsync(Arg.Any<Doctor>(), Arg.Any<CancellationToken>()).Returns(doctor);

            DoctorDomainService service = new DoctorDomainService(repository);
            Doctor result = service.AddAsync(doctor, default).Result;

            Assert.NotNull(result);

        }

        /// <summary>
        /// Doctor update test
        /// </summary>
        [Fact]
        public void UpdateDoctorTest()
        {

            Doctor doctor = GetDoctor();
            IDoctorRepository repository = Substitute.For<IDoctorRepository>();
            repository.Update(Arg.Any<Doctor>()).Returns(doctor);

            DoctorDomainService service = new DoctorDomainService(repository);
            Doctor result = service.Update(doctor);

            Assert.NotNull(result);

        }

        /// <summary>
        /// Doctor get by id test
        /// </summary>
        [Fact]
        public void GetByIdDoctorTest()
        {

            Doctor doctorToFind = GetDoctor();

            IEnumerable<Doctor> doctors = new List<Doctor>()
            {
                new Doctor() { FirstName = "CLARK", LastName = "KENT", Crm = "K0001/2000"  },
                doctorToFind,
                new Doctor() { FirstName = "NATASHA", LastName = "ROMANOFF", Crm = "K0003/2000"  }
            };

            IDoctorRepository repository = Substitute.For<IDoctorRepository>();
            repository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(x => doctors.FirstOrDefault(d => d.Id == new Guid(x[0].ToString())));

            DoctorDomainService service = new DoctorDomainService(repository);
            Doctor result = service.GetByIdAsync(doctorToFind.Id.Value, default).Result;

            Assert.NotNull(result);
            Assert.Equal(doctorToFind.FirstName, result.FirstName);
            Assert.Equal(doctorToFind.LastName, result.LastName);
            Assert.Equal(doctorToFind.Crm, result.Crm);

        }

        /// <summary>
        /// Doctor get all test
        /// </summary>
        [Fact]
        public void GetByAllDoctorTest()
        {

            Doctor doctorToFind = GetDoctor();

            IQueryable<Doctor> doctors = (new List<Doctor>()
            {
                new Doctor() { FirstName = "CLARK", LastName = "KENT", Crm = "K0001/2000"  },
                new Doctor() { FirstName = "NATASHA", LastName = "ROMANOFF", Crm = "K0003/2000"  }
            }).AsQueryable();

            IDoctorRepository repository = Substitute.For<IDoctorRepository>();
            repository.GetAllAsync(Arg.Any<CancellationToken>()).Returns(doctors);

            DoctorDomainService service = new DoctorDomainService(repository);
            IEnumerable<Doctor> result = service.GetAllAsync(default).Result.ToList();

            Assert.NotNull(result);
            Assert.Equal(doctors.Count(), result.Count());

        }

        /// <summary>
        /// Doctor delete test
        /// </summary>
        [Fact]
        public void DeleteDoctorTest()
        {

            IDoctorRepository repository = Substitute.For<IDoctorRepository>();

            repository.Delete(Arg.Any<Doctor>());

            DoctorDomainService service = new DoctorDomainService(repository);
            service.Delete(GetDoctor());

        }

    }

}
