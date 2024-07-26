using translateService.Data;

namespace translateService.Services
{
	public class CachedYtranslator : YTranslator, CachedTranslator
	{
		private TranslateContext dbcontext;
		public CachedYtranslator(TranslateContext dbcontext) 
		{
			this.dbcontext = dbcontext;
		}

		public async Task<string> TranslateWithCache(string lang, string totranslate )
		{
			string? translation = null;
			if (dbcontext.Translations.Where(t => t.Text == totranslate && t.Lang == lang).FirstOrDefault() != null)
			{
				translation = dbcontext.Translations.Where(t => t.Lang == lang && t.Text == totranslate).FirstOrDefault().Translate;
				if (translation != null)
				{
					return translation;
				}
				else { }
			}
			else
			{ }
				
				translation = await base.Translate(lang, totranslate);
				if (translation != null)
				{

					Translation trans = new Translation() { Lang = lang, Text = totranslate, Translate = translation };
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

