using System.Globalization;
using AutoMapper;
using Services.Core.Services.Dto;
using WeatherWebApp.Models;

namespace WeatherWebApp.Plumbing
{
    public class ToForecastModelProfile : Profile
    {
        protected override void Configure()
        {
            DateTimeFormatInfo fmt = (new CultureInfo("ru-RU")).DateTimeFormat;
            CreateMap<AvgForecastDto, ForecastModel>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("d MMMM, ddd ", fmt)))
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.AvgHumidity))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.AvgTemperature));
        }
    }
}