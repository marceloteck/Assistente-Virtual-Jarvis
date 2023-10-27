using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Speech.Synthesis.TtsEngine;

namespace InteligenciaArtificial.ClassGlobal
{
    public static class Actions
    {
        public static DateTime time = DateTime.Now;
		#region HORAS e DIAS
        public static void Horas()
        {
            
            string es;
            string hr;

            if (time.Hour == 1)
            {
                es = "é "; hr = " hora ";
            }
            else
            {
                es = "São "; hr = " horas ";
            }

            if (time.Hour <= 11 && time.Hour > 5)
            {
                Speaker.Speak(es + time.Hour.ToString() + hr + " e " + time.Minute.ToString() + " minutos da manhã");
            }
            else if (time.Hour >= 12 && time.Hour < 18)
            {
                int h = time.Hour - 12;
                Speaker.Speak(es + h.ToString() + hr + " e " + time.Minute.ToString() + " minutos da tarde");
            }
            else if (time.Hour > 18 && time.Hour < 24)
            {
                int h = time.Hour - 12;
                Speaker.Speak(es + h + hr + " e " + time.Minute.ToString() + "minutos da noite");
            }
            else
            {
                Speaker.Speak(es + time.Hour.ToString() + hr + time.Minute.ToString() + " minutos");
            }
        }
		

	public static void Data()
    {
		Speaker.Speak(DateTime.Now.ToShortDateString());
	}
	public static void Dia()
    {
		string dia = DateTime.Today.Day.ToString() == "1" ? "primeiro" : DateTime.Today.Day.ToString();
                Speaker.SpeakRand("hoje é " + DateTime.Today.ToString("dddd") + "Dia " + dia, "Senhor hoje é " + DateTime.Today.ToString("dddd") + "Dia " + dia, "Dia "+ dia + " no " + DateTime.Today.ToString("dddd"));
	}
	public static void Semana()
    {
		Speaker.Speak("hoje é " + DateTime.Today.ToString("dddd"));
	}
	public static void Mes()
    {
		// vamos usar switch para pegar o nome do mes
                string month = "";
                int n = time.Month;
                switch (n)
                {
                    case 1:
                        month = "janeiro";
                        break;
                    case 2:
                        month = "fevereiro";
                        break;
                    case 3:
                        month = "março";
                        break;
                    case 4:
                        month = "abril";
                        break;
                    case 5:
                        month = "maio";
                        break;
                    case 6:
                        month = "junho";
                        break;
                    case 7:
                        month = "julho";
                        break;
                    case 8:
                        month = "agosto";
                        break;
                    case 9:
                        month = "setembro";
                        break;
                    case 10:
                        month = "outubro";
                        break;
                    case 11:
                        month = "novembro";
                        break;
                    case 12:
                        month = "dezembro";
                        break;
                }
                Speaker.Speak("estamos no mês de " + month);
	}

	public static void Ano()
    {
		Speaker.Speak(DateTime.Today.ToString("yyyy"));
	}
	#endregion

	public static void fecharTudo()
    {
		Process[] processlist = Process.GetProcesses();
		foreach(Process process in processlist){
			if (process.ProcessName != "InteligenciaArtificial.exe" && process.ProcessName != "WOMicClient.exe") {
                    try
                    {
                        process.Kill();
                    }
                    catch(Exception ex)
                    {
                        //MessageBox.Show("Erro no fecharTudo: " + ex);
                    }
                
			}
		}
		//Speaker.Speak("Fechando todos os processos");
	}

    }


    public static class Automacao
    {
        public static void Automatic(string speech)
        {
            switch (speech)
            {
                case "CloseNavegator":
                    SendKeys.SendWait("^w");
                    break;
                case "OpenNavegator":
                    SendKeys.SendWait("^t");
                    break;
                default:
                    // código para lidar com comandos desconhecidos
                    break;
            }
        }
    }

    #region TECLADO
    public class Keyboard
    {
        private static Dictionary<string, Keys> _teclas = new Dictionary<string, Keys>
    {
        {"espaço", Keys.Space},
        {"enter", Keys.Enter},
        {"tab", Keys.Tab},
        {"backspace", Keys.Back},
        {"voltar", Keys.Back},
        {"delete", Keys.Delete},
        {"deletar", Keys.Delete},
        {"apagar", Keys.Delete},
        {"esc", Keys.Escape},
        {"cancelar", Keys.Escape},
        {"f1", Keys.F1},
        {"f2", Keys.F2},
        {"f3", Keys.F3},
        {"f4", Keys.F4},
        {"f5", Keys.F5},
        {"f6", Keys.F6},
        {"f7", Keys.F7},
        {"f8", Keys.F8},
        {"f9", Keys.F9},
        {"f10", Keys.F10},
        {"f11", Keys.F11},
        {"f12", Keys.F12},
        {"shift", Keys.ShiftKey},
        {"ctrl", Keys.ControlKey},
        {"alt", Keys.Menu},
        {"a", Keys.A},
        {"b", Keys.B},
        {"c", Keys.C},
        {"d", Keys.D},
        {"e", Keys.E},
        {"f", Keys.F},
        {"g", Keys.G},
        {"h", Keys.H},
        {"i", Keys.I},
        {"j", Keys.J},
        {"k", Keys.K},
        {"l", Keys.L},
        {"m", Keys.M},
        {"n", Keys.N},
        {"o", Keys.O},
        {"p", Keys.P},
        {"q", Keys.Q},
        {"r", Keys.R},
        {"s", Keys.S},
        {"t", Keys.T},
        {"u", Keys.U},
        {"v", Keys.V},
        {"w", Keys.W},
        {"x", Keys.X},
        {"y", Keys.Y},
        {"z", Keys.Z}
    };


        public static void Press(string comando)
        {
            string[] teclas = comando.Split('+');
            List<Keys> listaTeclas = new List<Keys>();

            foreach (string tecla in teclas)
            {
                if (_teclas.ContainsKey(tecla.ToLower()))
                {
                    try
                    {
                        SendKeys.SendWait("" + _teclas[tecla.ToLower()]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Tecla não encontrada erro na linha 204 class Keyboard: " + ex);
                    }

                }
                else
                {
                    MessageBox.Show("Tecla não reconhecida: " + tecla);

                }
            }



        }
    }
    #endregion

}
