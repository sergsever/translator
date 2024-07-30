namespace translateService.Services
{
	public interface ICachedTranslator
	{
		public Task<string> TranslateWithCache(string langfrom, string langto, string totranslate);
	}
}
