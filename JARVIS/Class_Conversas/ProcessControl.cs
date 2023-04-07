using JARVIS.Class_Conversas;
using JARVIS.Class_Conversas.Listas;
using JARVIS.RedeNeural;
using System;
using System.Diagnostics; // namespace usado
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;

namespace JARVIS
{
	/// <summary>
	/// Description of ProcessControl.
	/// </summary>
	public class ProcessControl
	{
		public ProcessControl()
		{
		}
        public static void WinComds(string proc)
        {
            foreach (string programa in cProcessWin.FileRead)
            {
                string[] partes = programa.Split('|');
                if (partes[0].Equals(proc, StringComparison.OrdinalIgnoreCase))
                {
                    Process.Start(partes[1]);
                    break;
                }
            }
        }
        public static void WebComds(string proc)
        {
            foreach (string programa in WebList.WebRead)
            {
                string[] partes = programa.Split('|');
                if (partes[0].Equals(proc, StringComparison.OrdinalIgnoreCase))
                {
                    Process.Start(partes[1]);
                    break;
                }
            }
        }

        public static void OpenOrClose(string proc)
        {
            // VERIFICAÇÃO DE PALAVRAS
            string[] TextArr = InternoComands.Exec.ToArray();
            string[] arrStr = proc.Split(' ');

            bool openF = false;
            bool fecharF = false;
            int openLetter = similarity.LevenshteinDistance("abri", arrStr[0]);
            int fecharLetter = similarity.LevenshteinDistance("fecha", arrStr[0]);
            // VERIFICAÇÃO DE PALAVRAS

            // VALIDAÇÃO DE PALAVRAS
            if (openLetter >= 0 && openLetter <= 3)
                {
                    openF = true; 
                }
                else if(fecharLetter >= 0 && fecharLetter <= 3)
                {
                    fecharF = true;
                }
            // VALIDAÇÃO DE PALAVRAS

            // if (proc.StartsWith("abrir"))
            if (openF == true)
                 {

                foreach (string palavra in TextArr)
                {
                    proc = proc.Replace(palavra, ""); // remove a palavra
                }
                proc = proc.Trim(); // remove espaços m branco


                Speaker.SpeakOpenningProcess(proc);

                    if (cProcessWin.ProgramasWin.Any(x => x == proc))
                    {
                        WinComds(proc);
                    }
                    else if (WebList.ComandsWEB.Any(x => x == proc))
                    {
                        WebComds(proc);
                    }
                    else
                    {
                        ShellComands.ShellSpeech(proc);
                    }
                }
                else if (fecharF == true)
                {

                    foreach (string palavra in TextArr)
                    {
                        proc = proc.Replace(palavra, ""); // remove a palavra
                    }
                    proc = proc.Trim(); // remove espaços m branco



                if (cProcessWin.ProgramasWin.Any(x => x == proc))
                    {
                        foreach (string programa in cProcessWin.FileRead)
                        {
                            string[] partes = programa.Split('|');
                            if (partes[0].Equals(proc, StringComparison.OrdinalIgnoreCase))
                            {
                                CloseProcess(partes[1], proc);
                                break;
                            }
                        }

                    }
                }


            







        }
        private static void CloseProcess(string cmd, string proc)
		{
            try // vamos usar try/catch
            {
                Process[] p = Process.GetProcessesByName(cmd);
                if (p[0] != null) // se o processo não for nulo
                {
                    Speaker.SayWithStatus("fechando o " + proc);
                    p[0].Kill();
                }
                else // se for nulo
                {
                    Speaker.Speak("desculpe, mas o " + proc + " não estar aberto");
                }
            }
            catch (Exception ex) // jarvis vai nos ajudar no debug
            {
                Speaker.Speak("senhor, não consigo fechar o "+ proc + " o erro é: " + ex.Message);
            }
		}

        /// <summary>
        /// Faz algo sobre a lista de processos
        /// </summary>
        public static void ProcessesRunning()
        {
            try
            {
                Process[] procs = Process.GetProcesses(); // pega todos os processos
                Speaker.Speak("obtendo lista de processos");
                foreach (Process proc in procs) // percorrer os processos
                {
                    if (proc.MainWindowTitle != "") // se a janela tiver título
                    {
                        Speaker.Speak(proc.MainWindowTitle + " está usando " + Convert.ToString(proc.PagedMemorySize64 / 1024 / 1024) + " mega baites");
                    }
                }
            }
            catch (Exception ex)
            {
                Speaker.Speak("ocorreu um erro " + ex.Message); // fala o erro
            }
        }
	}
}
