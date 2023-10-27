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
    public partial class ListadeComandos : Form
    {
        public ListadeComandos()
        {
            InitializeComponent();

            foreach (string arr in ComandsLotesG.ListLote)
            {
                textBox1.Text += arr + "\r\n";
            }
        }
    }
}
