using JARVIS.Class_Conversas;
using JARVIS.Class_Conversas.Listas;
using System;
using System.Diagnostics; // namespace usado
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

            if (proc.StartsWith("abrir") || proc.StartsWith("abra") || proc.StartsWith("abre"))
            {
                proc = proc.Replace("abrir", ""); // remove o comando
                proc = proc.Replace("abra", ""); // remove o comando
                proc = proc.Replace("abre", ""); // remove o comando
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
            else if (proc.StartsWith("fechar") || proc.StartsWith("feche"))
            {
                proc = proc.Replace("fechar", "");
                proc = proc.Replace("feche", "");
                proc = proc.Trim();

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
