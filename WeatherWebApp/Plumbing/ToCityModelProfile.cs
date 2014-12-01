using AutoMapper;
using Services.Core.Entities;
using WeatherWebApp.Models;

namespace WeatherWebApp.Plumbing
{
    public class ToCityModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<City, CityModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NativeName));
        }
    }
}