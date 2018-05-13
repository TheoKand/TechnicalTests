using System.Web.Mvc;
using MvcTest.WebApplication.Models;
using MvcTest.WebApplication.Services;

namespace MvcTest.WebApplication.Controllers {
	public class HomeController : Controller {
		private readonly IClock clock;

		public HomeController(IClock clock) {
			this.clock = clock;
		}

		public ActionResult Index() {
			return View();
		}

		public ActionResult Welcome(string name) {
			var viewData = new WelcomeViewData() {
				Name = "Eddie",
				Date = clock.UtcNow.ToString()
			};
			return (View(viewData));
		}
	}
}
