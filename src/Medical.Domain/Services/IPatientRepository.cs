using Medical.CrossCutting.Common.Services;
using Medical.Domain.Entities;

namespace Medical.Domain.Services
{

    /// <summary>
    /// Patient repository interface
    /// </summary>
    public interface IPatientRepository : IRepositoryBase<Patient>
    {
    }

}
