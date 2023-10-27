using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.IO;

namespace InteligenciaArtificial.Forms.Comandos
{
    public partial class EditarComandosLine : Form
    {
        public string link;
        public EditarComandosLine(string name)
        {
            InitializeComponent();
            txtLines(name);
        }
        public void txtLines(string name)
        {
            link = AddComands1.FileDir(name);

            string texto = File.ReadAllText(link);
            listBox1.Text = texto;
        }
    }
}
