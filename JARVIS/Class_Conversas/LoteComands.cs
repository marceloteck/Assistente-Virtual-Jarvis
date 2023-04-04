using JARVIS.Class_Conversas.Listas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace JARVIS.Class_Conversas
{
    public static class LoteComands
    {
        public static void Executar(string speech)
        {

            string[] TextArr = ComandsLotesG.ChoicesArrList.ToArray();

            if (speech.StartsWith("ativar") || speech.StartsWith("execultar") || TextArr.StartsWith(speech))
            {
                string[] LisRR = ComandsLotesG.ChoicesArrList.ToArray();
                foreach (string palavra in LisRR)
                {
                    speech = speech.Replace(palavra, ""); // remove a palavra
                }
                speech = speech.Trim(); // remove espaços m branco

                if (ComandsLotesG.ListLote.Any(x => x == speech))
                {
                    foreach (string ListComands in ComandsLotesG.ComandsEmLote)
                    {
                        string[] info = ListComands.Split(';');
                        string[] text = info[0].Split('=');
                        string[] resposta = info[1].Split('=');
                        string[] executar = info[2].Split('=');

                        text = text[1].Split('|');
                        foreach (string textP in text)
                        {
                            if (textP.Equals(speech, StringComparison.OrdinalIgnoreCase))
                            {
                                // RESPOSTA
                                resposta = resposta[1].Split('|');
                                Speaker.SpeakRand(resposta);

                                // EXECUTAR
                                executar = executar[1].Split('|');
                                foreach (string partes in executar)
                                {
                                    Process.Start(partes);
                                }
                                break;
                            }
                        }    
                    }
                }
            }
        }
    }
}
