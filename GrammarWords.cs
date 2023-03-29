using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace IA_JARVIS
{
    public class GrammarWords
    {
        private List<string> wordsList;

        public GrammarWords()
        {
            // Lê o arquivo com as palavras e armazena em uma lista
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data_Base", "words.txt");
            wordsList = File.ReadAllLines(path).ToList();
        }

        public void Recognize(string speech)
        {
            // Cria um reconhecedor de voz
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();

            // Adiciona as palavras à gramática
            Choices words = new Choices(wordsList.ToArray());
            GrammarBuilder grammarBuilder = new GrammarBuilder(words);
            Grammar grammar = new Grammar(grammarBuilder);

            // Adiciona a gramática ao reconhecedor
            recognizer.LoadGrammar(grammar);

            // Define o input do reconhecedor como a entrada de voz
            recognizer.SetInputToDefaultAudioDevice();

            // Reconhece a entrada de voz
            RecognitionResult result = recognizer.Recognize();

            // Cria uma frase com as palavras reconhecidas
            string[] recognizedWords = result.Text.Split(' ');
            List<string> phraseWords = new List<string>();
            foreach (string recognizedWord in recognizedWords)
            {
                if (wordsList.Contains(recognizedWord))
                {
                    phraseWords.Add(recognizedWord);
                }
            }
            string phrase = string.Join(" ", phraseWords);

            // Exibe a frase reconhecida
            Console.WriteLine(phrase);
        }
    }
}
