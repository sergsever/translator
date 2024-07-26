using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
	internal interface ITranslator
	{
		public string Translate(string lang, string text);
	}
}
