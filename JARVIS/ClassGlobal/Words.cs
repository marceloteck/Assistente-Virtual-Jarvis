using JARVIS.RedeNeural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS.ClassGlobal
{
    public static class Words
    {
        public static bool validadWords(string Speech, string text)
        {
            // VERIFICAÇÃO DE PALAVRAS
            string[] arrStr = Speech.Split(' ');
            bool vlor = false;
            foreach (string txtString in arrStr)
            {
                int openLetter = similarity.LevenshteinDistance(text, txtString);
                if (openLetter >= 0 && openLetter <= 3)
                {
                    vlor = true;
                }
            }
            return vlor;
            // VALIDAÇÃO DE PALAVRAS
        }
    }
}
