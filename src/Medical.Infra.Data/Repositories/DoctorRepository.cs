using Medical.Domain.Entities;
using Medical.Domain.Services;

namespace Medical.Infra.Data.Repositories
{

    /// <summary>
    /// Doctor respository object
    /// </summary>
    public class DoctorRepository : RepositoryBase<Doctor>, IDoctorRepository
    {

        ///<inheritdoc/>
        public DoctorRepository(MedicalDbContext ctx) : base(ctx) { }
    }
}
