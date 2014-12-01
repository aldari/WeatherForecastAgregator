using System.Web.Mvc;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using WeatherWebApp.Models;

namespace WeatherWebApp.Controllers
{
	public class HomeController : Controller
	{
		public HomeController()
		{
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Index()
		{
			ViewBag.Message = "Welcome to ToBeSeen WebSite!";

			return View();
		}
	}
}