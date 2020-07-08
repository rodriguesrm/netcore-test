using Medical.Domain.Entities;
using Medical.Domain.Services;

namespace Medical.Infra.Data.Repositories
{

    /// <summary>
    /// Patient respository object
    /// </summary>
    public class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    { 

        ///<inheritdoc/>
        public PatientRepository(MedicalContext ctx) : base(ctx) { }
    }
}
