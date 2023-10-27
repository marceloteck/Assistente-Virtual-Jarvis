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
    public partial class Testes : Form
    {
        public Testes()
        {
            InitializeComponent();
            foreach (string arr in ComandsLotesG.ComandsEmLote)
            {
                textBox1.Text += arr + "\r\n";
            }
        }
    }
}
