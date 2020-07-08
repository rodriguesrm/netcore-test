using Medical.CrossCutting.Common.Entities;
using Medical.CrossCutting.Common.Services;

namespace Medical.Domain.Services
{

    /// <summary>
    /// Interface base de Services Domain
    /// </summary>
    public interface IDomainServiceBase<TEntity> : ICommonBase<TEntity>
         where TEntity : EntityBase<TEntity>
    {
    }

}
