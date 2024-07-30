using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
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
		/*text может содержать несколько строк, разделенных '\n'("\n\r")*/
		[HttpGet]
		public async Task<string>  Get(string langfrom, string langto, string text)
		{
			List<string> translations = new List<string>();

			try
			{
				if (text.Contains('\n'))
				{
					string[] strings = text.Split('\n');

					foreach (string totranslate in strings)
					{
						string translation = await translater.TranslateWithCache(langfrom, langto, totranslate);
						if (translation != null)
						{
							translations.Add(translation);
						}
					}

				}
				else
				{
					string onetranslation = await translater.TranslateWithCache(langfrom, langto, text);
					if (onetranslation != null)
					{
						//						translations.Add(onetranslation);
						return onetranslation;
					}
				}
			}
			catch (Exception ex)
			{
				translations.Add(ex.Message);
			}
			string json = JsonConvert.SerializeObject(translations);

//			byte[] bytes = Encoding.Default.GetBytes(json);
//			json = Encoding.UTF8.GetString(bytes);

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
