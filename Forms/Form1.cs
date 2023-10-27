using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Speech.Recognition;
using NAudio.Wave;
using NAudio.Utils;
using System.IO;
using InteligenciaArtificial.Reconhecimento;
using InteligenciaArtificial.ClassGlobal;
using InteligenciaArtificial.Forms;
using System.Reflection.Emit;
using InteligenciaArtificial.Files;
using NAudio;
using System.Collections;
using System.CodeDom.Compiler;
using System.Drawing.Drawing2D;

namespace InteligenciaArtificial
{
    public partial class Form1 : Form
    {
        public static SpeechRecognitionEngine Engine; // declarando o reconhecedor
        public bool speechRecognitionActived = false; // booleana do reconhecimento
        public static bool isIAListening = false;
        public WaveIn _waveIn;
        public static bool conect = true;


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
            catch(Exception ex)
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

                label3.Text = speech + " " + confidence;
                //ChangeBackgroundColor();

                if (confidence >= 0.4)
                {
                    Rec.reconhecimento(speech); // chama as funções do reconhecimento

                    if (LoteComands.StartSpeeck == true)
                    {
                        //RcLabel.Text = "" + confidence;
                        label3.ForeColor = Color.DarkGreen; // mostrar o que foi reconhecido

                    }
                    else
                    {
                        label3.Text = "Inativo"; // mostrar o que foi reconhecido
                        label3.ForeColor = Color.DarkRed;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Aconteceu um erro no meu sistema: " + ex);
            }
        }
        #endregion



        #region sistema interno do código
        public Form1()
        {
            InitializeComponent();
            IniciarRelogio();
            ExibirDataCompleta();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSpeechRecognition(); // para fazer o reconhecimento
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
           // PhraseGenerator.GenerateNewPhrases();
        }
        #endregion

        #region Itens do formulario
        // quando clicar na pictureBox1
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (speechRecognitionActived == true) // se o reconhecimento de voz estiver ativo
            {
                Engine.RecognizeAsyncCancel(); // para o reconhecimento
                speechRecognitionActived = false; // altera o valor da booleana
                Speaker.Speak("reconhecimento de voz desativado"); // diz algo
            }
            else if (speechRecognitionActived == false)
            {
                Engine.RecognizeAsync(RecognizeMode.Multiple);
                speechRecognitionActived = true;
                Speaker.Speak("reconhecimento de voz ativado");
            }
        }

        #endregion

        #region MENU
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChatNativo chatt = new ChatNativo();
            chatt.Show();
        }
        private void mENUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menu Menu = new menu();
            Menu.Show();
        }
        #endregion


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

     
        
    }
}
