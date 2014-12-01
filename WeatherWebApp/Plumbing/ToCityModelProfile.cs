using AutoMapper;
using Services.Core.Entities;
using WeatherWebApp.Models;

namespace WeatherWebApp.Plumbing
{
    public class ToCityModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<City, CityModel>();
        }
    }
}