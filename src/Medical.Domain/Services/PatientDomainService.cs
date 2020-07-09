using Medical.Domain.Entities;

namespace Medical.Domain.Services
{

    /// <summary>
    /// Patient domain service operations
    /// </summary>
    public class PatientDomainService : DomainServiceBase<Patient, IPatientRepository>, IPatientDomainService
    {

        #region Constructors

        /// <summary>
        /// Create a new Patient domain service instance
        /// </summary>
        /// <param name="repository">Patient repository instance</param>
        public PatientDomainService(IPatientRepository repository) : base(repository) { }

        #endregion

    }

}
