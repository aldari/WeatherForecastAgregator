using System.Collections.Generic;
using Services.Core.Entities;

namespace Services.Core
{
    public interface IGetCitiesService
    {
        IList<City> GetCities();
    }
}