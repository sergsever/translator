using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
	 interface ITranslator
	{
		string Translate(string langfrom, string langto, string text);
	}
}
