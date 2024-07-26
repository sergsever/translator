using ConsoleClient;
using Microsoft.Extensions.Configuration;

namespace TranslateConsoleClient
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json");
			var conf = builder.Build();
			string access = conf["AccessType"];
			string lang = "";
			string text = "";
#if DEBUG
			if (args.Length != 2)
#else
lang =		if (args.length != 3)
#endif
			{
				Console.WriteLine("ugage: ConsoleClient <lang> <text to translate>\n");
			}
			else 
			{
#if DEBUG
				lang = args[0];
				text = args[1];
#else
				lang = args[1];
				text = args[2];

#endif
			}
			ITranslator? client = null;
			if (access == "grpc")
			{
				client = new GrpcTranslateClient();
			}
			else
			{
				client = new HttpTranslateClient();
			}
			string translation = client.Translate(lang, text);
			Console.WriteLine(translation);
		}
	}
}