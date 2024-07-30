using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleClient
{
	public class HttpTranslateClient : ITranslator
	{
		private const string baseurl = "https://localhost:7043/api/Translate";
		private HttpClient http;
		public HttpTranslateClient() 
		{
			http = new HttpClient();
		}

		public string Translate(string langfrom, string langto, string text)
		{
			string result = "";
			string parameters = string.Format("?langfrom={0}&langto={1}&text={2}", langfrom, langto, text);
			string url = baseurl + parameters;
			var resp = http.GetAsync(url).Result;
			if (resp != null)
			{
				string json = resp.Content.ReadAsStringAsync().Result;
				if (!string.IsNullOrEmpty(json))
				{
					result = json;
					}
			}
			return result;

		}
	}
}
