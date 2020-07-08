using Medical.CrossCutting.Common.Services;
using Medical.Domain.Entities;

namespace Medical.Domain.Services
{

    /// <summary>
    /// Doctor repository interface
    /// </summary>
    public interface IDoctorRepository : IRepositoryBase<Doctor>
    {
    }

}
