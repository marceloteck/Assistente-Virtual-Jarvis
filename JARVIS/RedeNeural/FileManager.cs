using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS.RedeNeural
{
    public static class FileManager
    {
        public static void CombineFiles(string folderPath)
        {
            string outputFile = "data/sentences.txt";

            // Abre ou cria o arquivo sentences.txt
            StreamWriter writer = new StreamWriter(outputFile, true);

            // Obtém todos os arquivos .txt na pasta especificada
            string[] files = Directory.GetFiles(folderPath, "*.txt");

            // Percorre cada arquivo e adiciona o seu conteúdo ao arquivo sentences.txt
            foreach (string file in files)
            {
                StreamReader reader = new StreamReader(file);
                string content = reader.ReadToEnd();
                writer.WriteLine(content);
                reader.Close();
            }

            // Fecha o arquivo sentences.txt
            writer.Close();
        }
    }
}

/* COMO USAR
 FileManager.CombineFiles("C:\\caminho\\para\\pasta\\de\\arquivos");
*/