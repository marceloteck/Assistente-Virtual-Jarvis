using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using JARVIS.Class_Conversas.Listas;
using System.Speech.Recognition;

namespace JARVIS
{
    /// <summary>
    /// Classe que vai falar algo passando uma frase
    /// </summary>
    public class Conversation
    {
        public static void SaySomethingFor(string phrase)// método que vai falar algo
        {
            switch (phrase) // switch, método mais rápido que if-else para muitas comparações
            {
                case "bom dia":
                    DateTime time = DateTime.Now; // pega as horas
                    if(time.Hour >= 5 && time.Hour < 12) // for de manhã
                    {
                    	Speaker.Speak("olá senhor, desejo que você tenha um bom dia!");
                    	break;
                    }
                    else if(time.Hour >= 12 && time.Hour < 18) // se for de tarde
                    {
                    	Speaker.Speak("olá, boa tarde");
                    	break;
                    }
                    else if(time.Hour >= 18 && time.Hour < 24) // se for e noite
                    {
                    	Speaker.Speak("oi, boa noite, senhor");
                    	break;
                    }
                    break;
            }

            if (InternoComands.ConversaSpeech.Any(x => x == phrase))
            {
                foreach (string programa in InternoComands.ConversaReader)
                {
                    string[] partes = programa.Split('|');
                    string[] coms = partes[0].Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string comds in coms)
                    {
                        if (comds.Equals(phrase, StringComparison.OrdinalIgnoreCase))
                        {
                            string[] Fll = partes[1].Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries);
                            Speaker.SpeakRand(Fll);

                            break;
                        }
                    }
                    



                }

            }



        }




        public static void AddIListS(string name, List<string> list)
        {
            using (StreamReader reader = new StreamReader(name))
            {
                string linha;
                while ((linha = reader.ReadLine()) != null)
                {
                    string[] parts = linha.Split('|');
                    string[] list_a = parts[0].Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string lineA in list_a)
                    {
                        list.Add(lineA + "|" + parts[1]);
                    }
                }
            }

        }






    }
}

