using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Services.Core;
using Services.Core.Entities;
using Services.Core.Services.Dto;
using WeatherWebApp.Models;

namespace WeatherWebApp.Controllers
{
	public class HomeController : Controller
	{
	    private readonly IGetCitiesService _citiesService;
	    private readonly IAverageDataService _averageDataService;
		public HomeController(IGetCitiesService getCitiesService, IAverageDataService averageDataService)
		{
		    _citiesService = getCitiesService;
		    _averageDataService = averageDataService;
		}

	    public ActionResult Index(int? id = null)
		{
            var cities = Mapper.Map<IList<City>, List<CityModel>>(_citiesService.GetCities());

            var model = new CitiesModel
            {
                Cities = cities
            };

		    if (id.HasValue)
		    {
                model.SelectedCity = cities.Single(x => x.Id == id).Name;
                var forecasts = Mapper.Map<IList<AvgForecastDto>, List<ForecastModel>>(_averageDataService.GetAverageDataForOneCity(id.Value));
		        model.Forecasts = forecasts;
		    }
            
            return View(model);
		}
	}
}