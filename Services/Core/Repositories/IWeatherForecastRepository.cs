using Services.Core.Entities;

namespace Services.Core.Repositories
{
    public interface IWeatherForecastRepository: IRepository<WeatherForecast, int>
    {
    }
}
