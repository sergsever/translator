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
			string langfrom = "";
			string langto = "";
			string text = "";
#if DEBUG
			if (args.Length != 3)
#else
lang =		if (args.length != 4)
#endif
			{
				Console.WriteLine("ugage: ConsoleClient <lang> <text to translate>\n");
			}
			else 
			{
#if DEBUG
				langfrom = args[0];
				langto = args[1];
				text = args[2];
#else
				langfrom = args[1];
				langto = args[2];
				text = args[3];
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
			string translation = client.Translate(langfrom, langto, text);
			Console.WriteLine(translation);
		}
	}
}