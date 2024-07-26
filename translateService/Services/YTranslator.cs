using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace translateService.Services
{
	public class YTranslator 
	{
		private string BaseUrl;
		private string Token;
		private HttpClient http;


		public YTranslator()
		{
			var builder = new ConfigurationBuilder();
			IConfigurationRoot conf = builder.AddJsonFile("appsettings.json").Build();
			this.BaseUrl = conf["Yandex:baseurl"];
			this.Token = conf["Yandex:token"];
			this.http = new HttpClient();

		}

		protected async Task<string> Translate(string lang, string text)
		{
			string result = "";

			YRequest request = new YRequest() { targetLanguageCode = lang, texts = text };
/*
			HttpRequestMessage message = new HttpRequestMessage()
			{
				RequestUri = new Uri(this.BaseUrl),
				Method = HttpMethod.Post,
				Content = new StringContent(JsonConvert.SerializeObject(request))

			};
			message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.Token);
*/
			http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.Token);
			http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			StringContent content = new StringContent(JsonConvert.SerializeObject(request));

			var response = await http.PostAsync(BaseUrl, content);
/*
			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				throw new Exception("Status code is " + response.StatusCode);
			}
*/
			string resp = await response.Content.ReadAsStringAsync();
			if (resp != null)
			{
			
				Debug.WriteLine("resp: " + resp);
				//				resp.Remove('[');
				//				resp.Remove(']');
				YResponse? answer = JsonConvert.DeserializeObject <YResponse> (resp);
				if (answer != null)
				{
					result = answer.translations.First().text;
				}
//				result = answer.text;
			}

		

			return result;
		}

	}
}
