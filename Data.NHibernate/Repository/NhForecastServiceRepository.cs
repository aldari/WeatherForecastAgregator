using Services.Core.Entities;
using Services.Core.Repositories;

namespace Data.NHibernate.Repository
{
    public class NhForecastServiceRepository : NhRepositoryBase<ForecastServiceEntity, int>, IForecastServiceEntityRepository
    {

    }
}