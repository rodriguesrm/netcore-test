using Medical.Domain.Entities;
using Medical.Domain.Services;

namespace Medical.Infra.Data.Repositories
{

    /// <summary>
    /// Appointment respository object
    /// </summary>
    public class AppointmentRepository : RepositoryBase<Appointment>, IAppointmentRepository
    {

        ///<inheritdoc/>
        public AppointmentRepository(MedicalDbContext ctx) : base(ctx) { }
    }
}
