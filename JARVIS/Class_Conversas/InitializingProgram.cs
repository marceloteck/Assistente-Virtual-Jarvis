using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JARVIS
{
    /// <summary>
    /// Classe que cuida de algumas coisas da inicialização
    /// </summary>
    public static class InitializingProgram
    {
        public static void Start()
        {
            DateTime hour = DateTime.Now;

            if (hour.Hour >= 5 && hour.Hour < 12)
            {
                Speaker.SpeakRand("olá", "oi", "oi, bom dia", "olá, Estou pronto para ajudar!");
            }
            else if (hour.Hour >= 12 && hour.Hour < 18)
            {
               Speaker.SpeakRand("olá", "oi", "oi, boa tarde", "olá, Estou pronto para ajudar!");
            }
            else if (hour.Hour >= 18 && hour.Hour <= 23)
            {
               Speaker.SpeakRand("olá", "oi", "oi, boa noite", "olá, Estou pronto para ajudar!");
            }
            else if (hour.Hour >= 0 && hour.Hour < 5)
            {
               Speaker.SpeakRand("olá", "oi", "oi, bom dia", "olá, Estou pronto para ajudar!");
            }
            else
            {
               Speaker.SpeakRand("olá", "oi", "olá, Estou pronto para ajudar!");
            }
        }
    }
}
