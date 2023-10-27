using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteligenciaArtificial.ClassGlobal
{
    public static class Words
    {

	
	public static string RemontarFrase(string speech, IList<string> ilist)
	{
		var palavras = speech.Split(' ');
		var frase = "";

		foreach (var palavra in palavras)
		{
			var maxSimilaridade = 0.0;
			var palavraMaisSimilar = "";

			foreach (var palavraLista in ilist)
			{
				var similaridade = similarity.JaroWinkler(palavra, palavraLista);

				if (similaridade >= 0.6 && similaridade > maxSimilaridade)
				{
					maxSimilaridade = similaridade;
					palavraMaisSimilar = palavraLista;
				}
			}

			if (!string.IsNullOrEmpty(palavraMaisSimilar))
			{
				frase += palavraMaisSimilar + " ";
			}
			else
			{
				frase += palavra + " ";
			}
		}

		var similaridadeFrase = similarity.JaroWinkler(frase.Trim(), speech);

		if (similaridadeFrase >= 0.6)
		{
			return frase.Trim();
		}
		else
		{
			return null;
		}
	}

    }
}
