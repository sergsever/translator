namespace translateService.Services
{
	public interface ICachedTranslator
	{
		Task<string> TranslateWithCache(string langfrom, string langto, string totranslate);
	}
}
