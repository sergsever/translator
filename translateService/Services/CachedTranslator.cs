using translateService.Data;

namespace translateService.Services
{
	public class Cachedranslator : ICachedTranslator
	{
		private TranslateContext dbcontext;
		private ITranslate translator;
		public Cachedranslator(TranslateContext dbcontext, ITranslate translator) 
		{
			this.dbcontext = dbcontext;
			this.translator = translator;
		}

		public async Task<string> TranslateWithCache(string langfrom, string langto, string totranslate )
		{
			string? translation = null;
			if (dbcontext.Translations.Where(t => t.Text == totranslate && t.Lang == langto).FirstOrDefault() != null)
			{
				translation = dbcontext.Translations.Where(t => t.Lang == langto && t.Text == totranslate).FirstOrDefault().Translate;
				if (translation != null)
				{
					return translation;
				}
				else { }
			}
			else
			{ }
				
				translation = await translator.Translate(langfrom,langto, totranslate);
				if (translation != null)
				{

					Translation trans = new Translation() { Lang = langto, Text = totranslate, Translate = translation };
					dbcontext.Translations.Add(trans);
					dbcontext.SaveChanges();
					return translation;
				}
				else
				{
					return string.Empty;
				}
			}
		}
	}

