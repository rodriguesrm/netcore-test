using Medical.CrossCutting.Common.Entities;

namespace Medical.CrossCutting.Common.Services
{

    /// <summary>
    /// Generic repository interface
    /// </summary>
    public interface IRepositoryBase<TEntity> : ICommonBase<TEntity>
         where TEntity : EntityBase<TEntity>
    {

    }

}
