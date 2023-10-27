using Microsoft.Speech.Recognition;
using System;
using InteligenciaArtificial.ClassGlobal;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using InteligenciaArtificial.Files;

namespace InteligenciaArtificial.Reconhecimento
{
    public class ActiveComands
    {
		public static bool nameOn = false;
        public static bool ActiveONOFF(string speech)
        {
            bool result_nameOn      = VerificarValorVariavel("nameOn", speech, Arquivos.config);
            bool result_offtag      = VerificarValorVariavel("offtag", speech, Arquivos.IgnorSpeech);
            bool result_offSpeaker  = VerificarValorVariavel("offSpeaker", speech, Arquivos.IgnorSpeech);
			
			//if(result_nameOn == true && nameOn == false)
			if(ActiveComands.VerificarSemelhanca(speech, "jarvis") && nameOn == false)
            {
				nameOn = true;
            }
            else if (result_offSpeaker == true)
            {
                Speaker.StopSpeak();
                Speaker.SpeakRand(GetOptionsFromTextFile(Arquivos.IgnorSpeech, "resposta_offSpeaker"));
            }
            else if(nameOn == true)
			{
				if(result_offtag == true){
					nameOn = false;
					Speaker.SpeakRand(GetOptionsFromTextFile(Arquivos.IgnorSpeech, "resposta_offtag"));
				}
			}

			return nameOn;
        }

        public static bool VerificarSemelhanca(string frase, string speech)
        {
            string[] palavras = frase.Split(' '); // Divide a frase em palavras
            foreach (string palavra in palavras)
            {
                if (string.Compare(palavra, speech, true) == 0) // Compara as palavras, ignorando maiúsculas e minúsculas
                {
                    return true; // Se encontrar uma correspondência, retorna true
                }
            }
            return false; // Se não encontrar correspondência, retorna false
        }

        // verificar valores das variaveis // APAGAR FUNÇÃO CONTEM ERROS ###########
        public static bool VerificarValorVariavel(string nomeVariavel, string speech, string caminhoArquivo)
        {
            string conteudoArquivo = File.ReadAllText(caminhoArquivo);

            string valorVariavel = conteudoArquivo.Substring(conteudoArquivo.IndexOf(nomeVariavel + "={") + nomeVariavel.Length + 2);
            valorVariavel = valorVariavel.Substring(0, valorVariavel.IndexOf("}"));

            var alternativas = valorVariavel.Split('|');
            foreach (var alternativa in alternativas)
            {
                string alternativaTratada = RemoverPalavrasDesnecessarias(alternativa.Trim().ToLower());
                string speechTratado = RemoverPalavrasDesnecessarias(speech.Trim().ToLower());

                var fala = speechTratado.Split(' ');

                foreach (string falacao in fala)
                {
                    double semelhanca = JaroWinklerDistance(alternativaTratada, falacao);
                    if (semelhanca >= 0.6)
                    {
                        return true;
                       
                    }
                    break;
                }
            }
            return false;
        }// END VerificarValorVariavel

        public static string RemoverPalavrasDesnecessarias(string frase)
        {
            string[] palavrasDesnecessarias = { "o", "a", "os", "as", "um", "uns", "uma", "umas", "de", "da", "do", "das", "dos", "para", "por", "em", "na", "no", "nas", "nos", "com" };
            string[] palavras = frase.Split(' ');
            string novaFrase = "";

            foreach (string palavra in palavras)
            {
                if (!palavrasDesnecessarias.Contains(palavra))
                {
                    novaFrase += palavra + " ";
                }
            }

            return novaFrase.Trim();
        }

        public static double JaroWinklerDistance(string s1, string s2)
        {
            return similarity.JaroWinkler(s1, s2);
        } // final da comparação de palavras

        public static string[] GetOptionsFromTextFile(string filePath, string nomeVariavel)
		{
            string conteudoArquivo = File.ReadAllText(filePath);

            string valorVariavel = conteudoArquivo.Substring(conteudoArquivo.IndexOf(nomeVariavel + "={") + nomeVariavel.Length + 2);
            valorVariavel = valorVariavel.Substring(0, valorVariavel.IndexOf("}"));

            var alternativas = valorVariavel.Split('|');
            return alternativas;

        } // END GetOptionsFromTextFile
		
		
    }
}
