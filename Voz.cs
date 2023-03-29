using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Speech.Recognition; // namespace de reconhecimento
using System.IO;
using System.Text.RegularExpressions;

namespace IA_JARVIS
{
    public partial class Voz : Form
    {
        private SpeechRecognitionEngine engine; // engine de reconhecimento
        private bool isIAListening = true;

        public Voz()
        {
        
            InitializeComponent();
           
        }

        private void LoadSpeech()
        {
            try
            {
                Speaker.Speak("Estou carregando meu núcleo!");  /// FALA

                engine = new SpeechRecognitionEngine(); // instancia
                engine.SetInputToDefaultAudioDevice(); // microfone


                Choices c_commandsOfSystem = new Choices();

                //string[] words = { "abrir", "rodar" };
                // chamando COMANDOS 
                c_commandsOfSystem.Add(GrammarRules.ComandVoice.ToArray()); 
                c_commandsOfSystem.Add(GrammarRules.ActionC.ToArray()); 
                c_commandsOfSystem.Add(GrammarRules.shellCommands.ToArray()); 
                c_commandsOfSystem.Add(GrammarRules.IaStopListening.ToArray()); 
                c_commandsOfSystem.Add(GrammarRules.IaStartListening.ToArray()); 
                c_commandsOfSystem.Add(GrammarRules.WhatTimeIS.ToArray()); 
                c_commandsOfSystem.Add(GrammarRules.WhatDateIs.ToArray()); 
               // c_commandsOfSystem.Add(words); 
                // END CHAMANDO COMANDOS 


                GrammarBuilder gb_commandsOfSystem = new GrammarBuilder();
                gb_commandsOfSystem.Append(c_commandsOfSystem);

                Grammar g_commandsOfSystem = new Grammar(gb_commandsOfSystem);
                g_commandsOfSystem.Name = "sys";

                engine.LoadGrammar(g_commandsOfSystem); // carregar a gramatica

                engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(rec);
                engine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(audioLevel);
                engine.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(rej);

                engine.RecognizeAsync(RecognizeMode.Multiple); // Inicia o reconhecimento





            /*    string input = "Que horas são neste momento?";
                string[] padroes = {
            @"que horas são\??",
            @"(você pode me dizer|pode me informar|me informe|você pode me falar) (as )?horas\??",
            @"(qual é a hora atual|qual é a hora agora|que horas estamos)\??",
            @"(me diga|pode me dizer|você pode me informar|pode me falar) (as )?horas agora( mesmo)?\??"
        };

                foreach (string padrao in padroes)
                {
                    if (Regex.IsMatch(input, padrao, RegexOptions.IgnoreCase))
                    {
                        MessageBox.Show($"O pedido principal é: {padrao}");
                        break;
                    }
                }*/











            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro no LoadSpeech: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSpeech();
            //ComandsDir.CarregarComandosVoz();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            // chama sua função aqui
            SistemaCarregado();
        }
        private void SistemaCarregado()
        {
            Speaker.Speak("Meus sistemas já estão carregados. Estou a sua disposição!");  /// FALA
        }


        

        private void rec(object s, SpeechRecognizedEventArgs e) /// COMANDO QUANDO FOR RECONHECIDO
        {

            if (e.Result != null)
            {
                var resultTxt = e.Result;
                string speech = resultTxt.Text; // string Reconhecida
                float conf = resultTxt.Confidence;

                //MessageBox.Show(speech);

                this.label1.Text = speech;

                if (conf > 0.35f)
                {
               

                   if (GrammarRules.IaStopListening.Any(x => x == speech))
                   {
                       isIAListening = false;
                   }
                   else if (GrammarRules.IaStartListening.Any(x => x == speech))
                   {
                       isIAListening = true;
                   }

                   if (isIAListening == true)
                   {
                       switch (e.Result.Grammar.Name)
                       {
                           case "sys":
                               // se o speech == "que horas são" 
                               if (GrammarRules.WhatTimeIS.Any(x => x == speech))
                               {
                                   Runner.WhatTimeIs(speech, Path.Combine(Application.StartupPath, "Data_Base/fixo/WhatTimeIS.txt"));
                               }
                               else if (GrammarRules.WhatDateIs.Any(x => x == speech))
                               {
                                   Runner.WhatDateIs(speech, Path.Combine(Application.StartupPath, "Data_Base/fixo/WhatDateIs.txt"));
                               }
                               else if (GrammarRules.ComandVoice.Any(x => x == speech))
                               {
                                    ComandsDir.IsQuestion(speech, Path.Combine(Application.StartupPath, "Data_Base/voice.txt"));
                                }
                                else if (GrammarRules.shellCommands.Any(x => x == speech))
                               {
                                  // Runner.WhatDateIs();
                               }
                               else if (GrammarRules.ActionC.Any(x => x == speech))
                               {
                                  // Runner.WhatDateIs();
                               }
                               break;
                       }
                   }

           }






            }
            else
            {

            }



           

        }

        private void audioLevel(object s, AudioLevelUpdatedEventArgs e)
        {
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = e.AudioLevel;
        }

        private void rej(object s, SpeechRecognitionRejectedEventArgs e)
        {
            //this.label2.ForeColor = Color.Red;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           Chat chat = new Chat();
           chat.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
