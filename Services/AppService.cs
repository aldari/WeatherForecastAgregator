using System.Collections.Generic;
using System.Linq;
using Services.Core.Entities;
using Services.Core.Repositories;

namespace Services
{
    public class AppService
    {
        private readonly ICityRepository _cityRepository;

        public AppService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public IList<City> GetCities()
        {
            return _cityRepository.GetAll().ToList();
        }
    }
}
