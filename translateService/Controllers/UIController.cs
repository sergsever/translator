using Microsoft.AspNetCore.Mvc;

namespace translateService.Controllers
{
	[Route("/[controller]/[action]")]
	public class UIController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
