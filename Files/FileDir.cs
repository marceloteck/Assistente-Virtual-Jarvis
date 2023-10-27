using InteligenciaArtificial.ClassGlobal;
using NAudio.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace InteligenciaArtificial.Files
{
    public static class FileDir
    {
 
        public static string[] ContentConfig(string pathfile, string variavel)
        {
            string[] result = null;
            using (StreamReader reader = new StreamReader(pathfile))
            {
                
                string input = reader.ReadToEnd();
                string pattern = $@"{variavel}={{(.*?)}}";

                Match match = Regex.Match(input, pattern);
                if (match.Success)
                {
                    string content = match.Groups[1].Value;
                    result = content.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                                             .Select(x => x.Trim())
                                             .ToArray();
                }

                return result;
            }
        }
        public static string ContentString(string pathfile, string variavel)
        {
            using (StreamReader reader = new StreamReader(pathfile))
            {
                string conteud = reader.ReadToEnd();
                string[] cont = conteud.Split(new string[] { variavel }, StringSplitOptions.RemoveEmptyEntries);
                string[] cont1 = cont[0].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                string input = cont1[0];

                // Encontra o índice de abertura e fechamento das chaves
                int startIndex = input.IndexOf("{") + 1;
                int endIndex = input.IndexOf("}");

                // Extrai o conteúdo dentro das chaves
                string content = input.Substring(startIndex, endIndex - startIndex);

                return content;
            }
        }
    }
}
