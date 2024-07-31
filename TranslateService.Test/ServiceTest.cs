using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using translateService.Data;
using translateService.Services;

namespace TranslateService.Test
{
	public class ServiceTest
	{
		public  ServiceTest()
		{

		}

		[Fact]
		public async Task TranslateTestAsync()
		{
			IOptions<YOptions> options = Options.Create<YOptions>(new YOptions());
			ConfigurationBuilder cbuilder = new ConfigurationBuilder ();
			var conf = cbuilder.AddJsonFile("appsettings.json").Build();
			options.Value.BaseUrl = "https://translate.api.cloud.yandex.net/translate/v2/translate";
			options.Value.Token = "secret";
			ITranslate translator = new YTranslator(options);
			string langfrom = "en";
			string langto = "ru";
			string totranslate = "understand";
			string result = await translator.Translate(langfrom, langto, totranslate);
			Assert.True(result != null && result.Length != 0);
		}

	}
}