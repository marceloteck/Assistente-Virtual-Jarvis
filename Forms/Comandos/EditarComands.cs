using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace InteligenciaArtificial.Forms.Comandos
{
    public partial class EditarComands : Form
    {
        public string link;
        public EditarComands(string name)
        {
            InitializeComponent();
            //link = Path.Combine(Application.StartupPath, "comandos/ComandsEmLote/" + name);
            link = AddComands1.FileDir(name);

            string texto = File.ReadAllText(link);
            textBox1.Text = texto;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("notepad++.exe", link);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao abrir: " + ex.Message);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string conteudo = textBox1.Text;
                File.WriteAllText(link, conteudo);
                MessageBox.Show("O texto foi adicionado ao arquivo com sucesso!", "Confirmação");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar o arquivo: " + ex.Message);
            }
        }
    }
}
