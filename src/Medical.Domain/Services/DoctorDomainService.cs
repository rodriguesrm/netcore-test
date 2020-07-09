using Medical.Domain.Entities;

namespace Medical.Domain.Services
{

    /// <summary>
    /// Doctor domain service operations
    /// </summary>
    public class DoctorDomainService : DomainServiceBase<Doctor, IDoctorRepository>, IDoctorDomainService
    {

        #region Constructors

        /// <summary>
        /// Create a new Doctor domain service instance
        /// </summary>
        /// <param name="repository">Doctor repository instance</param>
        public DoctorDomainService(IDoctorRepository repository) : base(repository) { }

        #endregion

    }

}
