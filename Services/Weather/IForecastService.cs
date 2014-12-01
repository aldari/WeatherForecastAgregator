using System;
using Services.Weather.Dto;

namespace Services.Weather
{
    public interface IForecastService
    {
        ForecastResponseDto ForecastData(String city);
    }
}
