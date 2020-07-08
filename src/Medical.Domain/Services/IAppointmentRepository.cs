using Medical.CrossCutting.Common.Services;
using Medical.Domain.Entities;

namespace Medical.Domain.Services
{

    /// <summary>
    /// Appointment repository interface
    /// </summary>
    public interface IAppointmentRepository : IRepositoryBase<Appointment>
    {
    }

}
