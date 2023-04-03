using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS.RedeNeural
{
    public static class NeuralNetwork
    {
        private static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data/sentences.txt");
        
        
        
        // salvar diretamente nessa lista o conteudo de todos os arquivos txts mandar aqui pra class
        // e para essa lista
        private static readonly List<string> sentences = new List<string>();

        static NeuralNetwork()
        {
            LoadSentences();
        }

        private static void LoadSentences()
        {
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        sentences.Add(line.ToLower());
                    }
                }
            }
        }

        public static string ProcessInput(string input)
        {
            var inputLowerCase = input.ToLower();
            var maxSimilarity = 0.0;
            var mostSimilarSentence = string.Empty;

            foreach (var sentence in sentences)
            {
                var similarity = CalculateSimilarity(inputLowerCase, sentence);
                if (similarity > maxSimilarity)
                {
                    maxSimilarity = similarity;
                    mostSimilarSentence = sentence;
                }
            }

            // You can define a threshold here to determine if the input is similar enough to one of the sentences
            if (maxSimilarity >= 0.8)
            {
                return mostSimilarSentence;
            }
            else
            {
                return "Desculpe, não entendi o que você quis dizer.";
            }
        }

        private static double CalculateSimilarity(string input1, string input2)
        {
            var intersection = 0.0;
            var union = 0.0;

            var words1 = input1.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
            var words2 = input2.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);

            foreach (var word1 in words1)
            {
                foreach (var word2 in words2)
                {
                    if (word1 == word2)
                    {
                        intersection++;
                        break;
                    }
                }

                union++;
            }

            foreach (var word2 in words2)
            {
                union++;
            }

            return intersection / union;
        }

        public static void AppendToSentences(string sentence)
        {
            using (var writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(sentence.ToLower());
            }

            sentences.Add(sentence.ToLower());
        }
    }
}

/** COMO USAR
 string input = "Qual é a previsão do tempo para hoje?";
string output = NeuralNetwork.ProcessInput(input);
Console.WriteLine(output);

*/