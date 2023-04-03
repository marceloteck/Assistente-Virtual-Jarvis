using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JARVIS.Class_Conversas.Listas
{
    public static class WebList
    {
        public static IList<string> ComandsWEB = new List<string>();
        public static IList<string> WebRead = new List<string>();

        static WebList()
        {
            string Web = Path.Combine(Application.StartupPath, "comandos/ComandsWeb/Web.txt");
            configList.AddList(Web, (List<string>)ComandsWEB);

            using (StreamReader reader = new StreamReader(Web))
            {
                string linha;
                while ((linha = reader.ReadLine()) != null)
                {
                    //WebRead.Add(linha);

                    string[] parts = linha.Split('|');
                    string[] list_a = parts[0].Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string lineA in list_a)
                    {
                        WebRead.Add(lineA + "|" + parts[1]);
                    }

                }
            }
        }
    }
}
