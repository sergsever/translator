namespace translateService.Services
{
	public interface ITranslate
	{
		public  Task<string> Translate(string lang, string text);
	}
}
