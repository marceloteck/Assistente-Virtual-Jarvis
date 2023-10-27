using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InteligenciaArtificial.Forms.Comandos
{
    public partial class FileNewText : Form
    {  
        public FileNewText()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nomeArquivo = textBox1.Text;

            // Cria o arquivo .txt com o nome do TextBox
            string caminhoArquivo = AddComands1.FileDir(nomeArquivo + ".txt");
            File.Create(caminhoArquivo).Dispose();

            // Adiciona o nome do arquivo ao arquivo ComandsLote.txt
            string caminhoComandsLote = AddComands1.FileDir("ComandsLote.txt");
            using (StreamWriter sw = File.AppendText(caminhoComandsLote))
            {
                sw.WriteLine("\r" + nomeArquivo + ".txt");
            }

            MessageBox.Show("Arquivo criado com sucesso!");
        }


    }
}
