using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InteligenciaArtificial.Forms
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // lista de comandos
            FileForms.ListaDeComandos();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileForms.AddComands1();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChatNativo chatt = new ChatNativo();
            chatt.Show();
            this.Close();
        }

       
    }
}
