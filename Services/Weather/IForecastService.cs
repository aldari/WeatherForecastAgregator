using System;
using System.Collections.Generic;
using Services.Weather.Dto;

namespace Services.Weather
{
    public interface IForecastService
    {
        IEnumerable<ForecastDto> ForecastData(String city);
    }
}
