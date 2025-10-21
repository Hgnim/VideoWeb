using Microsoft.AspNetCore.Mvc;

namespace VideoWeb.Controllers {
	public class StartPageController : Controller {
		public IActionResult Index() {
			return View();
		}
	}
}
