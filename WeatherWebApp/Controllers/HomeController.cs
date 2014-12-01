using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using Services.Core;
using Services.Core.Entities;
using WeatherWebApp.Models;

namespace WeatherWebApp.Controllers
{
	public class HomeController : Controller
	{
	    private IGetCitiesService _citiesService;
		public HomeController(IGetCitiesService getCitiesService)
		{
		    _citiesService = getCitiesService;
		}

		public ActionResult Index(int? id = null)
		{
		    Mapper.CreateMap<City, CityModel>();
            var cities = Mapper.Map<IList<City>, List<CityModel>>(_citiesService.GetCities());
		    

            var model = new CitiesModel
            {
                Cities = cities
            };

		    if (id.HasValue)
		    {
                model.SelectedCity = cities.Single(x => x.Id == id).Name;
		    }
            
            return View(model);
		}
	}
}