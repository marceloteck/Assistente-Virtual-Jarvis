using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InteligenciaArtificial.Forms.Comandos
{
    public partial class NovoComando : Form
    {
        public string link;
        public NovoComando(string name)
        {
            InitializeComponent();
            label5.Text = name;
            //link = Path.Combine(Application.StartupPath, "comandos/ComandsEmLote/" + name);
            link = AddComands1.FileDir(name);
        }

        private void NovoComando_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text     = textBox1.Text;
            text = text != "" ? text : "null";
            string resposta = textBox3.Text;
            resposta = resposta != "" ? resposta : "null";
            string executar = textBox2.Text;
            executar = executar != "" ? executar : "null";

            text = Regex.Replace(text, @"\r\n?|\n", "|");
            text = "\r\n[text="+ text + ";";

            resposta = Regex.Replace(resposta, @"\r\n?|\n", "|");
            resposta = "\r\nresposta=" + resposta + ";";

            executar = Regex.Replace(executar, @"\r\n?|\n", "|");
            executar = "\r\nexecutar=" + executar + ";]";

            string conteudo = text + resposta + executar;

            using (var stream = new FileStream(link, FileMode.Append))
            {
                using (var writer = new StreamWriter(stream))
                {
                    // Escreve o conteúdo no arquivo
                    writer.WriteLine(conteudo);
                    MessageBox.Show("O texto foi adicionado ao arquivo com sucesso!", "Confirmação");

                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
        }
    }
}
