using System.Linq;
using Services.Core.Entities;
using Services.Core.Repositories;
using Services.Weather;

namespace Services.Core.Services
{
    public class UpdateDataForCitiesThroughEachService : IUpdateDataForCitiesThroughEachService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IWeatherForecastRepository _forecastRepository;
        private readonly IForecastService[] _services;
        private readonly IIdentifier _identifier;

        public UpdateDataForCitiesThroughEachService(ICityRepository cityRepository,
            IWeatherForecastRepository forecastRepository,
            IForecastService[] services, IIdentifier identifier)
        {
            _cityRepository = cityRepository;
            _forecastRepository = forecastRepository;
            _services = services;
            _identifier = identifier;
        }

        [UnitOfWork]
        public void Execute()
        {            
            foreach (var city in _cityRepository.GetAll())
                foreach (var service in _services)
                {
                    ProceedCity(service, city);
                }
        }

        public void ProceedCity(IForecastService service, City city)
        {
            var result = service.ForecastData(city.Name);
            var serviceId = _identifier.IdentifierFor(service.GetType());

            if (!result.Success)
                return;
            var data = _forecastRepository
                .GetAll()
                .Where(x => x.City.Id == city.Id && x.Service.Id == serviceId);
            foreach (var item in data)
                _forecastRepository.Delete(item.Id);
            foreach (var dto in result.Items)
            {
                _forecastRepository.Insert(new WeatherForecast
                {
                    City = city,
                    Date = dto.Date,
                    Humidity = dto.Humidity,
                    DayTemperature = dto.MaxTemperature,
                    Service = new ForecastServiceEntity{Id = serviceId}
                });
            }
        }
    }
}
