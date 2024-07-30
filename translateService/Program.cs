using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Server;
using translateService.Data;
using translateService.Services;

namespace translateService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			WebApplication app = null;

			// Add services to the container.
			var cbuilder = new ConfigurationBuilder();
			string access = cbuilder.AddJsonFile("appsettings.json").Build()["access"];
			builder.Services.Configure<YOptions>(builder.Configuration.GetSection("Yandex"));
			builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
			builder.Services.AddSingleton<TranslateContext>();
			builder.Services.AddSingleton<ITranslate, YTranslator>();
			builder.Services.AddSingleton<Cachedranslator>();

//			if (access == "grpc")
//			{
				builder.Services.AddGrpc();
				builder.Services.AddCodeFirstGrpc(config =>
				{
					config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
				});

//				app = builder.Build();
//				app.MapGrpcService<GrpcYTranslator>();
//				app.Run();

//			}
	//		else
//			{
//				builder.Services.AddRazorPages();
				builder.Services.AddControllers();

				 app = builder.Build();
			app.MapGrpcService<GrpcTranslator>();

			app.MapControllers();
				var scope = app.Services.CreateScope();

				TranslateContext dbcontext = scope.ServiceProvider.GetRequiredService<TranslateContext>();
				dbcontext.Database.EnsureCreated();
				app.Run();

//			}




			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
			}
//			if (access != "grpc")
//			{ }
			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.UseStaticFiles();

		}
			/*
						app.UseEndpoints(endpoints =>
						{
							endpoints.MapGrpcService<GrpcTranslator>();
						});
			*/
		}
	}
