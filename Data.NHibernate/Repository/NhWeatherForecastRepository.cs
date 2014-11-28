using Services.Core.Entities;
using Services.Core.Repositories;

namespace Data.NHibernate.Repository
{
    public class NhWeatherForecastRepository : NhRepositoryBase<WeatherForecast, int>, IWeatherForecastRepository
    {

    }
}