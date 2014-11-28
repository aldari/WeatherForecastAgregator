using System.Collections.Generic;
using Services.Core.Services.Dto;

namespace Services.Core
{
    public interface IAverageDataService
    {
        IList<AvgForecastDto> GetAverageDataForOneCity(int cityId);
    }
}