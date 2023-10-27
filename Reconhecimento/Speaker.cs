using System.Speech.Synthesis; // classe usada
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteligenciaArtificial
{
    public class Speaker
    {
        // é preciso ser static, porque vamos usar-lo dentro de um método estático

        private static Random rnd = new Random();
        private static SpeechSynthesizer sp = new SpeechSynthesizer(); // instanciando o sintetizador
        //private static string user = Environment.UserName; // mostrar nome do usuario do PC
        public static void Speak(string text) // falar o texto passado
        {
            speaking = true;
            sp.SpeakAsyncCancelAll();
            sp.SpeakAsync(text); // método async
        }

        public static void SpeakSync(string text) // fala na mesma thread
        {
            sp.Speak(text);
        }

        // Controlar o volume do sintetizador
        public static void VolumeUp()
        {
            sp.Volume += 10;
        }

        public static void VolumeDown()
        {
            sp.Volume -= 10;
        }

        public static void SetVolume(int i)
        {
            sp.Volume = i;
        }

        private static bool speaking = false;
        public static void ResumeOrPause()
        {
            if (speaking == false)
            {
                sp.Resume();
                speaking = true;
            }
            else
            {
                sp.Pause();
                speaking = false;
            }
        }
        public static void SpeakException(string ex)
        {
            Speak(ex);
        }

        public static void SpeakRand(params string[] texts)
        {
            Speak(texts[rnd.Next(0, texts.Length)]);
        }
        public static void SetVoice(string voice) // setar voz do sintetizador
        {
            sp.SelectVoice(voice); // definir voz
        }

        public static void StopSpeak()
        {
            sp.SpeakAsyncCancelAll();// para de falar
        }
    }
}
