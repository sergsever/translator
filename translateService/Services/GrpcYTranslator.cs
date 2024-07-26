using Grpc.Core;

namespace translateService.Services
{
	public class GrpcYTranslator : GrpcCachedYTranslator.GrpcCachedYTranslatorBase
	{
		private CachedYtranslator translator;

		public GrpcYTranslator(CachedYtranslator translator)
		{
			this.translator = translator;
		}

		public override Task<TranslateResponse> TranslateWithCache(TranslateRequest request, ServerCallContext context)
		{

			TranslateResponse response = new TranslateResponse();
			try
			{
				foreach (string totranslate in request.Text)
				{
					string translation =  translator.TranslateWithCache(request.Lang, totranslate).Result;
					response.Text.Add(translation);
					response.Lang = request.Lang;
				}
			}
			catch(Exception ex)
			{
				response.Text.Add(ex.Message);
			}
			return Task.FromResult(response);
		}
	}
}
