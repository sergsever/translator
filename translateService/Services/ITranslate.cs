namespace translateService.Services
{
	public interface ITranslate
	{
		public  Task<string> Translate(string langfrom, string langto, string text);
	}
}
