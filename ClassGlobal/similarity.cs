using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteligenciaArtificial.ClassGlobal
{
    public static class similarity
    {
		// função de alta eficencia
		public static double JaroWinkler(string s1, string s2, double p = 0.1)
		{
			// Calculando a distância Jaro
			int n1 = s1.Length;
			int n2 = s2.Length;
			int maxDistance = (int)Math.Floor(Math.Max(n1, n2) / 2.0) - 1;

			bool[] s1Matches = new bool[n1];
			bool[] s2Matches = new bool[n2];

			int matches = 0;
			int transpositions = 0;

			for (int i = 0; i < n1; i++)
			{
				int start = Math.Max(0, i - maxDistance);
				int end = Math.Min(i + maxDistance + 1, n2);

				for (int j = start; j < end; j++)
				{
					if (s1Matches[i] || s2Matches[j])
						continue;

					if (s1[i] != s2[j])
						continue;

					matches++;
					s1Matches[i] = true;
					s2Matches[j] = true;
					break;
				}
			}

			if (matches == 0)
				return 0.0;

			// Calculando a distância Winkler
			int prefix = 0;
			for (int i = 0; i < Math.Min(4, n1); i++)
			{
                if (i >= n2 || s1[i] != s2[i])
                    break;
                prefix++;
			}

			double winkler = ((double)matches / n1 + (double)matches / n2 + (double)(matches - transpositions / 2) / matches) / 3;
			winkler += prefix * p * (1 - winkler);

			return winkler;
		}
/*
A função recebe duas strings s1 e s2, e opcionalmente um parâmetro p para ajustar o peso do prefixo comum. O valor padrão para p é 0.1. A função retorna um valor entre 0 e 1, indicando o quão similar as duas strings são.

Para utilizá-la na sua aplicação, basta chamar a função passando as strings que deseja comparar e verificar se o valor retornado é maior do que um limite que você definir, indicando que as strings são suficientemente similares.

Note que a eficiência da função pode ser afetada pelo tamanho das strings que está comparando, e pela quantidade de comparações que precisa fazer. Por isso, é importante escolher um valor adequado para o parâmetro maxDistance na comparação Jaro, que limita a quantidade de comparações a serem feitas.
*/



        public static int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];
        }
        public static bool IsPhraseSimilar(string phrase1, string phrase2)
        {
            var words1 = phrase1.Split(new char[] { ' ', ',', '.', '?', '!', ':', ';' }, StringSplitOptions.RemoveEmptyEntries);
            var words2 = phrase2.Split(new char[] { ' ', ',', '.', '?', '!', ':', ';' }, StringSplitOptions.RemoveEmptyEntries);

            int similarCount = 0;

            foreach (var w1 in words1)
            {
                foreach (var w2 in words2)
                {
                    if (IsSimilar(w1, w2))
                    {
                        similarCount++;
                        break;
                    }
                }
            }

            double similarity = (double)similarCount / Math.Max(words1.Length, words2.Length);
            return similarity >= 0.5; // considera similar se houver pelo menos 50% de palavras semelhantes
        }

        public static bool IsSimilar(string word1, string word2)
        {
            if (string.IsNullOrEmpty(word1) || string.IsNullOrEmpty(word2))
                return false;

            if (word1.Equals(word2, StringComparison.InvariantCultureIgnoreCase))
                return true;

            int lengthDiff = Math.Abs(word1.Length - word2.Length);
            if (lengthDiff > 1)
                return false;

            if (lengthDiff == 1)
            {
                if (word1.StartsWith(word2, StringComparison.InvariantCultureIgnoreCase)
                    || word2.StartsWith(word1, StringComparison.InvariantCultureIgnoreCase))
                    return true;

                return false;
            }

            int differenceCount = 0;
            for (int i = 0; i < word1.Length; i++)
            {
                if (word1[i] != word2[i])
                {
                    differenceCount++;
                    if (differenceCount > 1)
                        return false;
                }
            }

            return true;
        }

    }
}
