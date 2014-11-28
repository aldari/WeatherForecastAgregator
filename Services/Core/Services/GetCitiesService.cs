using System.Collections.Generic;
using System.Linq;
using Services.Core.Entities;
using Services.Core.Repositories;

namespace Services.Core.Services
{
    public class GetCitiesService: IGetCitiesService
    {
        private readonly ICityRepository _cityRepository;

        public GetCitiesService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [UnitOfWork]
        public IList<City> GetCities()
        {
            return _cityRepository.GetAll().ToList();
        }
    }
}
