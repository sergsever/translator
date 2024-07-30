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
			string langfrom = "en";
			string langto = "ru";
			string totranslate = "understand";
			string result = await translator.Translate(langfrom, langto, totranslate);
			Assert.True(result == "понимать");
		}

	}
}