using Medical.Domain.Entities;
using Medical.Domain.Services;

namespace Medical.Infra.Data.Repositories
{

    /// <summary>
    /// AppClient respository object
    /// </summary>
    public class AppClientRepository : RepositoryBase<AppClient>, IAppClientRepository
    {

        ///<inheritdoc/>
        public AppClientRepository(MedicalDbContext ctx) : base(ctx) { }
    }
}
