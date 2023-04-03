using JARVIS.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JARVIS
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            CommandList listDeComandos = new CommandList();
            listDeComandos.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            ProcessList processList = new ProcessList();
            processList.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Microfone micc = new Microfone();
            micc.Show();
        }
    }
}
