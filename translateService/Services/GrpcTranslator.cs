using Grpc.Core;

namespace translateService.Services
{
	public class GrpcTranslator : GrpcCachedTranslator.GrpcCachedTranslatorBase
	{
		private ICachedTranslator translator;

		public GrpcTranslator(ICachedTranslator translator)
		{
			this.translator = translator;
		}

		public override Task<TranslateResponse> Translate(TranslateRequest request, ServerCallContext context)
		{

			TranslateResponse response = new TranslateResponse();
			try
			{
				foreach (string totranslate in request.Text)
				{
					string translation =  translator.TranslateWithCache(request.Langfrom, request.Langto, totranslate).Result;
					response.Text.Add(translation);
					response.Lang = request.Langto;
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
