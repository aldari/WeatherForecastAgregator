using System;
using System.Collections.Generic;
using Services.Dto;

namespace Services
{
    public interface IForecastService
    {
        IEnumerable<ForecastDto> ForecastData(String city);
    }
}
