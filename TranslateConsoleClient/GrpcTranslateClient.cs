using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.Client;
using translateService.Services;

namespace ConsoleClient
{
	public class GrpcTranslateClient : ITranslator
	{
		public GrpcTranslateClient() { }
		public string Translate(string lang, string text)
		{
			string translation = "";
			var chanel = GrpcChannel.ForAddress("https://localhost:7043");
			var client = new GrpcCachedYTranslator.GrpcCachedYTranslatorClient(chanel);
			TranslateRequest request = new TranslateRequest();
			request.Lang = lang;
			if (text.Contains("\n\r"))
			{
				string[] strings = text.Split("\n\r");
				foreach(string s in strings)
				{
					request.Text.Add(s);
				}

			}
			else
			{
				request.Text.Add(text);
			}

			TranslateResponse response = client.TranslateWithCache(request);

			foreach(string trans in response.Text)
			{
				translation += translation.Length == 0 ? trans : "\n" + trans;
			}

			return translation;
		}
	}
}
