using System.Collections.Generic;
using System.Linq;
using Services.Core.Repositories;
using Services.Core.Services.Dto;

namespace Services.Core.Services
{
    public class AverageDataService : IAverageDataService
    {
        private readonly IWeatherForecastRepository _forecastRepository;

        public AverageDataService(IWeatherForecastRepository forecastRepository)
        {
            _forecastRepository = forecastRepository;
        }

        [UnitOfWork]
        public virtual IList<AvgForecastDto> GetAverageDataForOneCity(int cityId)
        {
            var numberGroups =
                from n in _forecastRepository.GetAll().Where(c => c.City.Id == cityId)
                group n by n.Date into g
                select new AvgForecastDto
                {
                    Date = g.Key, 
                    AvgTemperature = (int)g.Average(x=>x.DayTemperature), 
                    AvgHumidity = (int)g.Average(x=>x.Humidity)
                };
            return numberGroups.ToList();
        }
    }
}
