using Medical.CrossCutting.Common.Entities;

namespace Medical.CrossCutting.Common.Services
{


    /// <summary>
    /// Services domain insterface
    /// </summary>
    public interface IDomainServiceBase<TEntity> : ICommonBase<TEntity>
         where TEntity : EntityBase<TEntity>
    {

    }

}
