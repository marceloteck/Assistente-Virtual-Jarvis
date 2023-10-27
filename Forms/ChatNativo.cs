using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using InteligenciaArtificial.Reconhecimento;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace InteligenciaArtificial
{
    public partial class ChatNativo : Form
    {
        public ChatNativo()
        {
            InitializeComponent();
            this.AcceptButton = button1;
        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            chatText.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnviarTxt();
        }
        public void EnviarTxt()
        {
            if (chatText.Text != "")
            {
                string msg = "Você: " + chatText.Text.Trim() + "\r\n";
                chatHistoric.AppendText(msg);

                string padraoLine = @"\r?\n";
                string novoTexto = Regex.Replace(chatText.Text.Trim(), padraoLine, "");
                if (novoTexto == "clear" || novoTexto == "limpar")
                {
                    chatHistoric.Clear();
                }
                else
                {
                    Rec.chatIa(novoTexto);
                }
                
                chatText.Clear();
            }
        }

        private void chatText_KeyDown(object sender, KeyEventArgs e)
        {
            // Verifica se a tecla pressionada é o Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Verifica se a tecla Shift também foi pressionada
                if (!e.Shift)
                {
                    // Cancela a ação padrão do Enter
                    e.SuppressKeyPress = true;
                    EnviarTxt();
                }
            }
        }

        private void chatHistoric_TextChanged(object sender, EventArgs e)
        {
            chatText.Focus();
        }
    }
}
