using System.Web.Mvc;

namespace ToBeSeen.Controllers
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