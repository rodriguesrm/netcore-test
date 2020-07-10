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
    /// Patient domain service tests
    /// </summary>
    public class PatientDomainServiceTest
    {

        private Patient GetPatient()
            => new Patient()
            {
                FirstName = "OLIVER",
                LastName = "QUEEN",
                Cpf = "26725871402"
            };

        /// <summary>
        /// Patient add test
        /// </summary>
        [Fact]
        public void AddPatientTest()
        {

            Patient Patient = GetPatient();
            IPatientRepository repository = Substitute.For<IPatientRepository>();
            repository.AddAsync(Arg.Any<Patient>(), Arg.Any<CancellationToken>()).Returns(Patient);

            PatientDomainService service = new PatientDomainService(repository);
            Patient result = service.AddAsync(Patient, default).Result;

            Assert.NotNull(result);

        }

        /// <summary>
        /// Patient update test
        /// </summary>
        [Fact]
        public void UpdatePatientTest()
        {

            Patient Patient = GetPatient();
            IPatientRepository repository = Substitute.For<IPatientRepository>();
            repository.Update(Arg.Any<Patient>()).Returns(Patient);

            PatientDomainService service = new PatientDomainService(repository);
            Patient result = service.Update(Patient);

            Assert.NotNull(result);

        }

        /// <summary>
        /// Patient get by id test
        /// </summary>
        [Fact]
        public void GetByIdPatientTest()
        {

            Patient patientToFind = GetPatient();

            IEnumerable<Patient> Patients = new List<Patient>()
            {
                new Patient() { FirstName = "NATASHA", LastName = "ROMANOFF", Cpf = "K0003/2000" },
                patientToFind,
                new Patient() { FirstName = "NATASHA", LastName = "ROMANOFF", Cpf = "69830658163"  }
            };

            IPatientRepository repository = Substitute.For<IPatientRepository>();
            repository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(x => Patients.FirstOrDefault(d => d.Id == new Guid(x[0].ToString())));

            PatientDomainService service = new PatientDomainService(repository);
            Patient result = service.GetByIdAsync(patientToFind.Id.Value, default).Result;

            Assert.NotNull(result);
            Assert.Equal(patientToFind.FirstName, result.FirstName);
            Assert.Equal(patientToFind.LastName, result.LastName);
            Assert.Equal(patientToFind.Cpf, result.Cpf);

        }

        /// <summary>
        /// Patient get all test
        /// </summary>
        [Fact]
        public void GetByAllPatientTest()
        {

            Patient patientToFind = GetPatient();

            IQueryable<Patient> Patients = (new List<Patient>()
            {
                new Patient() { FirstName = "CLARK", LastName = "KENT", Cpf = "95354366437"  },
                new Patient() { FirstName = "NATASHA", LastName = "ROMANOFF", Cpf = "69830658163"  }
            }).AsQueryable();

            IPatientRepository repository = Substitute.For<IPatientRepository>();
            repository.GetAllAsync(Arg.Any<CancellationToken>()).Returns(Patients);

            PatientDomainService service = new PatientDomainService(repository);
            IEnumerable<Patient> result = service.GetAllAsync(default).Result.ToList();

            Assert.NotNull(result);
            Assert.Equal(Patients.Count(), result.Count());

        }

        /// <summary>
        /// Patient delete test
        /// </summary>
        [Fact]
        public void DeletePatientTest()
        {

            IPatientRepository repository = Substitute.For<IPatientRepository>();

            repository.Delete(Arg.Any<Patient>());

            PatientDomainService service = new PatientDomainService(repository);
            service.Delete(GetPatient());

        }

    }

}
