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
    public partial class TelaPreta : Form
    {
        public TelaPreta()
        {
            InitializeComponent();
            IniciarRelogio();
        }

        //RELOGIO
        private void AtualizarRelogio()
        {
            // Atualiza o label com o horário atual
            Hora_TelaPreta.Text = DateTime.Now.ToString("HH:mm");

        }

        // Define um temporizador para atualizar o relógio a cada segundo
        private Timer timerRelogio;

        public void IniciarRelogio()
        {
            // Cria um novo temporizador com intervalo de 1 segundo
            timerRelogio = new Timer();
            timerRelogio.Interval = 1000;

            // Define o evento a ser disparado a cada intervalo de tempo
            timerRelogio.Tick += (sender, e) =>
            {
                AtualizarRelogio();
            };

            // Inicia o temporizador
            timerRelogio.Start();
        }

        public void PararRelogio()
        {
            // Para o temporizador
            timerRelogio.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
