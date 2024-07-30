using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using translateService.Data;
using translateService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace translateService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TranslateController : Controller
	{
		private ICachedTranslator translater;
		public TranslateController(ICachedTranslator translater)
		{
			this.translater = translater;
		}
		[HttpGet]
		public async Task<string>  Get(string langfrom, string langto, string text)
		{
			string translation = "";

			try
			{
				{
					translation = await translater.TranslateWithCache(langfrom, langto, text);
					if (translation != null)
					{
						return translation;
					}
				}
			}
			catch (Exception ex)
			{
				translation = ex.Message;
			}
			string json = JsonConvert.SerializeObject(translation);

			return json;
		}

		// GET api/<TranslateController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<TranslateController>
		[HttpPost]
		public void Post([FromBody] YRequest request)
		{
		}

		// PUT api/<TranslateController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<TranslateController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
