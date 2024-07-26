namespace translateService.Services
{
	public interface CachedTranslator
	{
		public Task<string> TranslateWithCache(string lang, string totranslate);
	}
}
