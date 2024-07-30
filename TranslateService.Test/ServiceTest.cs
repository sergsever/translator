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
			IOptions<YOptions> options;
			ConfigurationBuilder cbuilder = new ConfigurationBuilder ();
			var conf = cbuilder.AddJsonFile("appsettings.json").Build();
			options.Value.BaseUrl = conf["Yandex:baseurl"];
			options.Value.Token = conf["token"];
			ITranslate translator = new YTranslator(options);
			string langfrom = "en";
			string langto = "ru";
			string totranslate = "understand";
			string result = await translator.Translate(langfrom, langto, totranslate);
			Assert.True(result == "понимать");
		}

	}
}