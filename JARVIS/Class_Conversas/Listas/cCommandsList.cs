using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.LinkLabel;

namespace JARVIS.Class_Conversas.Listas
{
    //CLASS DE DATA E HORAS
    public static class DataHora
    {
        public static IList<string> QueHoras     = new List<string>();
        public static IList<string> QueData      = new List<string>();
        public static IList<string> QueDia       = new List<string>();
        public static IList<string> QueDiaSemana = new List<string>();
        public static IList<string> QueMes       = new List<string>();
        public static IList<string> QueAno       = new List<string>();

        static DataHora()
        {
            string Horas    = Path.Combine(Application.StartupPath, "comandos/DataeHora/Horas.txt");
            string data     = Path.Combine(Application.StartupPath, "comandos/DataeHora/data.txt");
            string dia      = Path.Combine(Application.StartupPath, "comandos/DataeHora/dia.txt");
            string semana   = Path.Combine(Application.StartupPath, "comandos/DataeHora/semana.txt");
            string mes      = Path.Combine(Application.StartupPath, "comandos/DataeHora/mes.txt");
            string ano      = Path.Combine(Application.StartupPath, "comandos/DataeHora/ano.txt");

            configList.AddList(Horas,  (List<string>)QueHoras);
            configList.AddList(data,   (List<string>)QueData);
            configList.AddList(dia,    (List<string>)QueDia);
            configList.AddList(semana, (List<string>)QueDiaSemana);
            configList.AddList(mes,    (List<string>)QueMes);
            configList.AddList(ano,    (List<string>)QueAno);
        }
        

    }
    //CLASS DE COMANDOS INTERNOS NA IA
    public static class InternoComands
    {
        public static IList<string> PareFalar   = new List<string>();
        public static IList<string> LsdeComands = new List<string>();
        public static IList<string> ProcessosDetalhes = new List<string>();

        static InternoComands()
        {
            string StopF            = Path.Combine(Application.StartupPath, "comandos/interno/StopSpeak.txt");
            string AddComands       = Path.Combine(Application.StartupPath, "comandos/interno/AddComands.txt");
            string DetlhProcessos   = Path.Combine(Application.StartupPath, "comandos/interno/DetlhProcessos.txt");

            configList.AddList(StopF,       (List<string>)PareFalar);
            configList.AddList(AddComands,  (List<string>)LsdeComands);
            configList.AddList(DetlhProcessos,  (List<string>)ProcessosDetalhes);

        }
    }   
    
    public static class DiversosComands
    {
        public static IList<string> ComandsEmLote   = new List<string>();

        static DiversosComands()
        {
            string arquivo = Path.Combine(Application.StartupPath, "comandos/ComandsEmLote/ComandsLote.txt");

            // Lista para armazenar as linhas
            //List<string> linhas = new List<string>();

            // Lê o arquivo principal linha por linha
            using (StreamReader sr = new StreamReader(arquivo))
            {
                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    // Verifica se a linha contém um link para outro arquivo
                    if (linha.Contains(".txt"))
                    {
                        string link = linha.Substring(linha.IndexOf(".txt"));

                        link = Path.Combine(Application.StartupPath, "comandos/ComandsEmLote/" + link);
                        // Lê o arquivo vinculado ao link
                        using (StreamReader sr2 = new StreamReader(link))
                        {
                            string linha2;
                            while ((linha2 = sr2.ReadLine()) != null)
                            {
                                // Adiciona cada linha na lista
                                ComandsEmLote.Add(linha2);
                            }
                        }
                    }
                }
            }

        }
    } // end DiversosComands










    // CLASS DE CONFIGURAÇÕES RAPIDAS
    public static class configList
    {
        public static void AddList(string name, List<string> list)
        {
            using (StreamReader reader = new StreamReader(name))
            {
                string linha;
                while ((linha = reader.ReadLine()) != null)
                {
                   // string[] parts = linha.Split('|');
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

    }
}


