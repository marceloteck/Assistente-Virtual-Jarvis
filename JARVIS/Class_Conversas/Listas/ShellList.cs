using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JARVIS.Class_Conversas.Listas
{
    public static class ShellList
    {
        public static IList<string> ShellListComands = new List<string>();
        public static IList<string> ShellRead = new List<string>();

        static ShellList()
        {
            string Shell = Path.Combine(Application.StartupPath, "comandos/ProcessosEProgramas/ShellComands.txt");
            configShellList.AddList(Shell, (List<string>)ShellListComands);
            AddIListS(Shell);
        }

        public static void AddIListS(string name)
        {
            using (StreamReader reader = new StreamReader(name))
            {
                string linha;
                while ((linha = reader.ReadLine()) != null)
                {
                    string[] parts = linha.Split('|');
                    string[] list_a = parts[0].Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string lineA in list_a)
                    {
                        ShellRead.Add(lineA + "|" + parts[1]);
                    }
                }
            }

        }
    }

    // CLASS DE CONFIGURAÇÕES RAPIDAS
    public static class configShellList
    {
        public static void AddList(string name, List<string> list)
        {
            using (StreamReader reader = new StreamReader(name))
            {
                string linha;
                while ((linha = reader.ReadLine()) != null)
                {
                    //string[] parts = linha.Split('|');
                    //list.Add(parts[0]);

                    string[] parts = linha.Split('|');
                    string[] list_a = parts[0].Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string lineA in list_a)
                    {
                        list.Add(lineA);
                    }

                }
            }
        }// end AddList

    }// end configShellList

}
