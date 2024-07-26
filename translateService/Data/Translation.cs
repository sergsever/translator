using System.ComponentModel.DataAnnotations;

namespace translateService.Data
{
	public class Translation
	{
		[Key]
		public string Text { get; set; }
		public string Lang { get; set; }
		public string Translate { get; set; }
	}
}
