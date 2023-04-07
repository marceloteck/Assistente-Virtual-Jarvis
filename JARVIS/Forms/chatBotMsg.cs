using JARVIS.Reconhecimento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JARVIS.Forms
{
    public partial class chatBotMsg : Form
    {
        public chatBotMsg()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Rec rec = new Rec();
            string txtbox =  textBox1.Text;
            rec.reconhecimentoChat(txtbox);
            txtbox = "";
        }
    }
}
