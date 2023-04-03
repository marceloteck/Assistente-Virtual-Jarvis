using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JARVIS.Class_Conversas.Listas
{
    public static class cProcessWin
    {
        public static IList<string> ProgramasWin = new List<string>();
        public static IList<string> FileRead = new List<string>();

        static cProcessWin()
        {
            string ProgramasWindows = Path.Combine(Application.StartupPath, "comandos/ProcessosEProgramas/Arquivos.txt");
            configList.AddList(ProgramasWindows, (List<string>)ProgramasWin);
         
            using (StreamReader reader = new StreamReader(ProgramasWindows))
            {
                string linha;
                while ((linha = reader.ReadLine()) != null)
                {
                    FileRead.Add(linha);
                }
            }
        }
    }
}
