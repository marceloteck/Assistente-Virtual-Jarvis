using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using InteligenciaArtificial.ClassGlobal;
using System.Web;
using System.Text.RegularExpressions;
using System.Globalization;


namespace InteligenciaArtificial
{
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
                    list.Add(linha);
                    
                }
            }
        }// end AddList
    }
    // CLASS COMANDOS EM LOTE
    public static class ComandsLotesG
    {
        public static IList<string> ComandsEmLote = new List<string>();
        public static IList<string> ChoicesArrList = new List<string>();
        public static IList<string> ListLote = new List<string>();

        static ComandsLotesG()
        {
            string ChoicesString = Path.Combine(Application.StartupPath, "comandos/ComandsEmLote/chave.txt");
            configList.AddList(ChoicesString, (List<string>)ChoicesArrList);


            string arquivo = Path.Combine(Application.StartupPath, "comandos/ComandsEmLote/ComandsLote.txt");
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

                        link = Path.Combine(Application.StartupPath, "comandos/ComandsEmLote/" + linha);
                        // Lê o arquivo vinculado ao link
						string texto = File.ReadAllText(link);
						
						string padrao = @"\[(.*?)\]";
						MatchCollection matches = Regex.Matches(texto, padrao, RegexOptions.Singleline);

						foreach (Match match in matches)
						{
							string conteudo = match.Groups[1].Value;
							string padraoLine = @"\r?\n";
							string novoTexto = Regex.Replace(conteudo, padraoLine, "");
                            novoTexto = novoTexto.Trim();

                            ComandsEmLote.Add(novoTexto);
						}
                    }
                }
            } // end using sr

            // armazenar o text na list
            ComandsEmLote = ComandsEmLote.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            foreach (string ListComands in ComandsEmLote)
            {
                string[] comands = ListComands.Split(';');
                comands = comands.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
                foreach (string cmd in comands)
                {
                    string[] info = cmd.Split('=');

                    switch (info[0])
                    {
                        case "text":
                            string[] text = info[1].Split('|');
                            // faça algo com as informações de texto
                            text = text.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
                            foreach (string Linha in text)
                            { 
                                string lin = Linha.Trim();
                                string padraoLine = @"\r?\n";
                                string novoTexto = Regex.Replace(lin, padraoLine, "");
                                ListLote.Add(novoTexto);
                            }
                            break;
                    }
                }
            }

        } // end static ComandsLotesG
    } // end ComandsLotesG

}