using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using InteligenciaArtificial.Forms;
using System.IO;
using InteligenciaArtificial.Reconhecimento;
using Microsoft.Speech.Recognition;
using NAudio.Wave;
using System.Reflection.Emit;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace InteligenciaArtificial.Forms
{
    public partial class BarradeTarefas : Form
    {
        public static SpeechRecognitionEngine Engine; // declarando o reconhecedor
        public bool speechRecognitionActived = false; // booleana do reconhecimento
        public static bool isIAListening = false;
        public WaveIn _waveIn;
        public static bool conect = true;


        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        public BarradeTarefas()
        {
            InitializeComponent();
            ExibirDataCompleta();
            IniciarRelogio();
            tExtBoxCC1.Padding = new Padding(15);

        }
        private void BarradeTarefas_Activated(object sender, EventArgs e)
        {
            tExtBoxCC1.Focus();
            this.Opacity = 0.95;
        }
        private void BarradeTarefas_Deactivate(object sender, EventArgs e)
        {
            this.Opacity = 0.4;
        }
        private void BarradeTarefas_Load(object sender, EventArgs e)
        {
            alturaForm();
            LoadSpeechRecognition(); // para fazer o reconhecimento
        }


        #region RECONHECIMENTO GERAL
        #region reconhecimento
        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            Engine.SetInputToWaveStream(new WaveFileReader(new MemoryStream(e.Buffer)));
        }
        private void LoadSpeechRecognition() // fazer o que é preciso para o reconhecimento de voz
        {
            #region NAudio
            _waveIn = new WaveIn();
            _waveIn.WaveFormat = new WaveFormat(16000, 1); // taxa de amostragem e número de canais
            _waveIn.DataAvailable += OnDataAvailable;
            #endregion

            try
            {
                Engine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("pt-BR")); // instanciando  o reconhecedor passando a cultura da engine
                Engine.SetInputToDefaultAudioDevice(); // definindo o microfone como entrada de aúdio
                Grammatic(); // gramatica
                EngineEvents(); // carrega os eventos

            }
            catch (Exception ex) // deu mal
            {
                string es = ex.Message;
                msgErroRec(es);
                conect = false;
            }
        }
        public void Grammatic()
        {
            try
            {
                GrammarBuilder grammarbg = new GrammarBuilder();
                grammarbg.Append(new Choices(ComandsLotesG.ListLote.ToArray())); // comando
                Grammar gGramar = new Grammar(grammarbg);
                gGramar.Name = "RecIA";


                Choices choicesIA = new Choices();
                choicesIA.Add(ComandsLotesG.ListLote.ToArray());
                GrammarBuilder grammarbgIA = new GrammarBuilder();
                grammarbgIA.Append(new Choices(ComandsLotesG.ChoicesArrList.ToArray())); // comando
                grammarbgIA.Append(choicesIA);
                Grammar gGramarIA = new Grammar(grammarbgIA);
                gGramarIA.Name = "RecIA2";

                Engine.LoadGrammarAsync(gGramar); // carregar gramática
                Engine.LoadGrammarAsync(gGramarIA); // carregar gramática

            }
            catch (Exception ex)
            {
                string es = ex.Message;
                msgErroRec(es);
                conect = false;
            }

        }
        public void EngineEvents()
        {
            try
            {
                Engine.RequestRecognizerUpdate();
                Engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(reconhecido); // evento do reconhecimento
                Engine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(audioElevou); // quando o aúdio é elevadoEngine
                Engine.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(rejeitado); // quando o reconhecimento de voz falhou
                Engine.SpeechDetected += new EventHandler<SpeechDetectedEventArgs>(vozDetectada); // alguma voz foi detectada
                Engine.LoadGrammarCompleted += new EventHandler<LoadGrammarCompletedEventArgs>(loaded); // gramática carregada

                Engine.RecognizeAsync(RecognizeMode.Multiple); // iniciar o reconhecimento async e múltiplo
            }
            catch (Exception ex)
            {
                string es = ex.Message;
                msgErroRec(es);
                conect = false;
            }
        }

        public static void msgErroRec(string ex)
        {
            MessageBox.Show("Não consigo fazer reconhecimento de voz devido a esse erro:" + ex);
        }
        #endregion

        #region reconhecimento ativo
        private void loaded(object sender, LoadGrammarCompletedEventArgs e)
        {
            // Gramatica carregada
            // Speaker.Speak("Minha Memória já está carregada");
        }
        private void vozDetectada(object sender, SpeechDetectedEventArgs e)
        {
            // voz detectada
            pictureBox1.Visible = true; // mostrar a pictureBox1
            pictureBox1.Image = (Bitmap)Bitmap.FromFile("Pictures\\ok.png"); // carreganda a imagem
        }
        private void rejeitado(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            //rejeitado
            pictureBox1.Image = (Bitmap)Bitmap.FromFile("Pictures\\fall.png"); // imagem do erro, carregamos ela
        }
        private void audioElevou(object sender, AudioLevelUpdatedEventArgs e)
        {
            progressBar1.Value = e.AudioLevel; // setando o valor da progressBar igual ao valor do aúdio em percento
        }
        private void reconhecido(object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
                // reconhecido
                var EResult = e.Result;
                string speech = EResult.Text; // texto que foi reconhecido
                double confidence = Math.Round(EResult.Confidence, 2); // variável para a confiança
                string EGramar = e.Result.Grammar.Name;

                label2.Text = confidence * 100 + "%: " + speech;
                //ChangeBackgroundColor();

                if (confidence >= 0.4)
                {
                    Rec.reconhecimento(speech); // chama as funções do reconhecimento

                    if (LoteComands.StartSpeeck == true)
                    {
                        //RcLabel.Text = "" + confidence;
                        label2.ForeColor = Color.White; // mostrar o que foi reconhecido

                    }
                    else
                    {
                        label2.Text = "Inativo"; // mostrar o que foi reconhecido
                        label2.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Aconteceu um erro no meu sistema: " + ex);
            }
        }
        #endregion
        #endregion FINAL DO RECONHECIMENTO


        #region ENVIO DE TEXTO
        public void EnviarTxt()
        {
            if (tExtBoxCC1.Text != "")
            {
                //string msg = "Você: " + tExtBoxCC1.Text.Trim() + "\r\n";
                string padraoLine = @"\r?\n";
                string novoTexto = Regex.Replace(tExtBoxCC1.Text.Trim(), padraoLine, "");
                Rec.chatIa(novoTexto);
                

                tExtBoxCC1.Clear();
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
        #endregion




        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BarradeTarefas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void alturaForm()
        {
            // Define a posição do formulário no centro da tela
            this.StartPosition = FormStartPosition.CenterScreen;

            // Obtém a altura da barra de tarefas
            int taskBarHeight = Screen.PrimaryScreen.Bounds.Height - Screen.PrimaryScreen.WorkingArea.Height;

            // Define a posição vertical do formulário
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) - taskBarHeight / 2;
        }

        //RELOGIO
        private void AtualizarRelogio()
        {
            // Atualiza o label com o horário atual
            relogioTx.Text = DateTime.Now.ToString("HH:mm");
            
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


        private void ExibirDataCompleta()
        {
            // Obtem a data atual
            DateTime dataAtual = DateTime.Now;

            // Formata a data no formato desejado
            string dataFormatada = dataAtual.ToString("D");

            // Define o texto do Label como a data formatada
            DataTx.Text = dataFormatada;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TelaPreta tella = new TelaPreta();
            tella.Show();
        }

        private void chatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChatNativo chatt = new ChatNativo();
            chatt.Show();
        }

        private void mENUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menu Menu = new menu();
            Menu.Show();
        }

        private void tExtBoxCC1_TextChanged(object sender, EventArgs e)
        {
            tExtBoxCC1.BackColor = Color.Transparent;
            tExtBoxCC1.ForeColor = Color.Black;
        }

        private void minimizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
