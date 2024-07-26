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
			ITranslate translator = new YTranslator();
			string lang = "ru";
			string totranslate = "understand";
			string result = await translator.TranslateAsync(lang, totranslate);
			Assert.True(result == "понимать");
		}

	}
}