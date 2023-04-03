using JARVIS.Class_Conversas.Listas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS.Class_Conversas
{
    public static class ShellComands
    {
        public static void ShellSpeech(string speech)
        {
            if (ShellList.ShellListComands.Any(x => x == speech))
            {
                foreach (string programa in ShellList.ShellRead)
                {
                    string[] partes = programa.Split('|');
                    if (partes[0].Equals(speech, StringComparison.OrdinalIgnoreCase))
                    {
                        Process.Start(partes[1]);
                        break;
                    }
                }

            }
        }
    }
}
