using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace translateService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AboutController : ControllerBase
	{
		private readonly string info = "Для перевода используется Yandex Translate API. Ранее запрошенные переводы кэшируютя в MS SQL Server базу данных(в текущей реализации)." +
			"В дальнейшем может использоваться любая БД.";
		public AboutController() { }
		[HttpGet]
		public string Get()
		{
			return info;
		}
}	
}
