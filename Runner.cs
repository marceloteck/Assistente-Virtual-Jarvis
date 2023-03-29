using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IA_JARVIS
{
    public class Runner
    {
       

        public static void WhatTimeIs(string userInput, string urlPath)
        {
            // Lê o arquivo de texto contendo as perguntas sobre as horas
            string[] questions = File.ReadAllLines(urlPath);

            // Verifica se a frase do usuário é correspondente a uma pergunta sobre as horas
            foreach (string question in questions)
            {
                if (userInput.ToLower().Contains(question.ToLower()))
                {
                    Speaker.Speak(DateTime.Now.ToShortTimeString());
                    break;
                }
            }

            //return false;
        }

        public static void WhatDateIs(string userInput, string urlPath)
        {
            // Lê o arquivo de texto contendo as perguntas sobre a DATA
            string[] questions = File.ReadAllLines(urlPath);

            // Verifica se a frase do usuário é correspondente a uma pergunta sobre a DATA
            foreach (string question in questions)
            {
                if (userInput.ToLower().Contains(question.ToLower()))
                {
                    Speaker.Speak(DateTime.Now.ToShortDateString());
                    break;
                }
            }

            //return false;
        }


    }
}
