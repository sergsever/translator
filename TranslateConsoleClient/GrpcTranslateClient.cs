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
		public string Translate(string langfrom, string langto, string text)
		{
			string translation = "";
			var chanel = GrpcChannel.ForAddress("https://localhost:7043");
			var client = new GrpcCachedTranslator.GrpcCachedTranslatorClient(chanel);
			TranslateRequest request = new TranslateRequest();
			request.Langfrom = langfrom;
			request.Langto = langto;
			request.Text.Add(text);

			TranslateResponse response = client.Translate(request);

			{
				translation = response.Text.FirstOrDefault();
			}

			return translation;
		}
	}
}
