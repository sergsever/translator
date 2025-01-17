﻿using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using translateService.Data;

namespace translateService.Services
{
	public class YTranslator : ITranslate
	{
		private readonly IOptions<YOptions> options;
		private string BaseUrl;
		private string Token;
		private HttpClient http;


		public YTranslator(IOptions<YOptions> options)
		{
			this.http = new HttpClient();
			this.options = options;
			this.BaseUrl = this.options.Value.BaseUrl; ;
			this.Token = this.options.Value.Token;

		}

		public async Task<string> Translate(string langfrom, string langto, string text)
	{
		string result = "";

			try
			{ 
			YRequest request = new YRequest() { targetLanguageCode = langto, texts = text };
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

			HttpResponseMessage response = await http.PostAsync(BaseUrl, content);
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
				YResponse? answer = JsonConvert.DeserializeObject<YResponse>(resp);
				if (answer != null)
				{
					result = answer.translations.First().text;
				}
			}
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}
//				result = answer.text;
			

		

			return result;
		}

	}
}
