namespace translateService.Services
{
	public interface ITranslate
	{
		Task<string> Translate(string langfrom, string langto, string text);
	}
}
