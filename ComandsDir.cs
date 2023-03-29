using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IA_JARVIS
{
    public class ComandsDir
    {
        //private string voiceFilePath = Path.Combine(Application.StartupPath, "Data_Base/voice.txt");

        public static void IsQuestion(string userInput, string urlPath)
        {
            // Lê o arquivo de texto contendo as perguntas sobre as horas
            string[] questions = File.ReadAllLines(urlPath);

            // Verifica se a frase do usuário é correspondente a uma pergunta sobre as horas
            foreach (string question in questions)
            {
                if (userInput.ToLower().Contains(question.ToLower()))
                {
                    //return true;
                }
            }

            //return false;
        }
    }
}



/* public static void CarregarComandosVoz()
       {
           string path = Path.Combine(Application.StartupPath, "Data_Base/voice.txt"); // substitua pelo caminho do seu arquivo
           try
           {
               using (StreamReader sr = new StreamReader(path))
               {
                   string line;
                   while ((line = sr.ReadLine()) != null)
                   {

                   }
               }
           }
           catch (Exception ex)
           {
               MessageBox.Show("Erro ao carregar comandos de voz: " + ex.Message);
           }
       }*/
