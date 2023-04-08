using AForge.Vision.Motion;
using JARVIS.Class_Conversas;
using JARVIS.Class_Conversas.Listas;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS.Reconhecimento
{
    public class Rec
    {
        public void reconhecimento(string speech, string EGramar)
        {
            Form1 form = new Form1();

            switch (EGramar) // vamos usar o nome da gramática para executar as ações
            { 
                case "Chats": // caso for uma conversa
                    Conversation.SaySomethingFor(speech); // vamos usar a classe que faz algo sobre
                    break;

                case "Dumme":
                    if (DummeIn.InStartingConversation.Any(x => x == speech))
                    {
                        int randIndex = Form1.rnd.Next(0, DummeOut.OutStartingConversation.Count);
                        Speaker.Speak(DummeOut.OutStartingConversation[randIndex]);
                    }
                    else if (DummeIn.InQuestionForDumme.Any(x => x == speech))
                    {
                        int randIndex = Form1.rnd.Next(0, DummeOut.OutQuestionForDumme.Count);
                        Speaker.Speak(DummeOut.OutQuestionForDumme[randIndex]);
                    }
                    else if (DummeIn.InDoWork.Any(x => x == speech))
                    {
                        int randIndex = Form1.rnd.Next(0, DummeOut.OutDoWork.Count);
                        Speaker.Speak(DummeOut.OutDoWork[randIndex]);
                    }
                    else if (DummeIn.InDummeStatus.Any(x => x == speech))
                    {
                        int randIndex = Form1.rnd.Next(0, DummeOut.OutDummeStatus.Count);
                        Speaker.Speak(DummeOut.OutDummeStatus[randIndex]);
                    }
                    else if (DummeIn.InJarvis.Any(x => x == speech))
                    {
                        int randIndex = Form1.rnd.Next(0, DummeOut.OutJarvis.Count);
                        Speaker.Speak(DummeOut.OutJarvis[randIndex]);
                    }
                    break;
                case "Commands":
                    switch (speech)
                    {
                        case "quais são as notícias":
                            form.newsFromG1 = G1FeedNews.GetNews();
                            Speaker.Speak("Já carreguei as notícias");
                            break;
                        case "próxima notícia":
                            Speaker.Speak("Título da notícia.. " + form.newsFromG1[form.newsIndex].Title
                                + " .. " + form.newsFromG1[form.newsIndex].Text);
                            form.newsIndex++;
                            break;
                    }
                    if (speech == "até mais jarvis")
                    {
                        form.ExitNow(); // chama oo método
                    }
                    else if (speech == "minimizar a janela principal")
                    {
                        form.MinimizeWindow(); // minimizar
                    }
                    else if (speech == "mostrar janela principal")
                    {
                        form.BackWindowToNormal(); // mostrar janela principal
                    }
                    else
                    {
                        Commands.Execute(speech);
                    }
                    break;
                case "Jokes":
                    break;
                case "Calculations":
                    Calculations.DoCalculation(speech);
                    break;
                case "Process":
                    ProcessControl.OpenOrClose(speech);
                    break;
                case "Control":
                    form.motionDetection = new MotionDetection();
                    form.motionDetection.Show();
                    break;
                case "ComandsLote":
                    LoteComands.Executar(speech);
                    break;
                default: // caso padrão
                    Speaker.Speak(AIML.GetOutputChat(speech)); // pegar resposta
                    break;
            }
        }

        public static string txtConvert(string speech)
        {
            // Converter para minúsculas
            string textoMinusculo = speech.ToLower();
            // Remover acentos e caracteres especiais
            speech = RemoverAcentos(textoMinusculo);
            return speech;
        }


        public void reconhecimentoChat(string speech)
        {
            Form1 form = new Form1();

            
          speech = txtConvert(speech);

          /*  bool containsSimilarSpeech = false;

            foreach (string startingSpeech in DummeIn.InStartingConversation)
            {
                if (startingSpeech.IndexOf(speech, StringComparison.OrdinalIgnoreCase | StringComparison.CurrentCulture) >= 0)
                {
                    containsSimilarSpeech = true;
                    break;
                }
            }*/




            if (DummeIn.InStartingConversation.Any(x => x == speech))
                    {
                        int randIndex = Form1.rnd.Next(0, DummeOut.OutStartingConversation.Count);
                        Speaker.Speak(DummeOut.OutStartingConversation[randIndex]);
                    }
                    else if (DummeIn.InQuestionForDumme.Any(x => x == speech))
                    {
                        int randIndex = Form1.rnd.Next(0, DummeOut.OutQuestionForDumme.Count);
                        Speaker.Speak(DummeOut.OutQuestionForDumme[randIndex]);
                    }
                    else if (DummeIn.InDoWork.Any(x => x == speech))
                    {
                        int randIndex = Form1.rnd.Next(0, DummeOut.OutDoWork.Count);
                        Speaker.Speak(DummeOut.OutDoWork[randIndex]);
                    }
                    else if (DummeIn.InDummeStatus.Any(x => x == speech))
                    {
                        int randIndex = Form1.rnd.Next(0, DummeOut.OutDummeStatus.Count);
                        Speaker.Speak(DummeOut.OutDummeStatus[randIndex]);
                    }
                    else if (DummeIn.InJarvis.Any(x => x == speech))
                    {
                        int randIndex = Form1.rnd.Next(0, DummeOut.OutJarvis.Count);
                        Speaker.Speak(DummeOut.OutJarvis[randIndex]);
                    }
                    else if (InternoComands.Exec.Any(x => x == speech))
                    {
                       ProcessControl.OpenOrClose(speech);
                    }
                    else if (ComandsLotesG.ChoicesArrList.Any(x => x == speech))
                    {
                       LoteComands.Executar(speech);
                    }
                    else
                    {
                        Commands.Execute(speech);
                    }

    
              
            
        }

        public static string RemoverAcentos(string texto)
        {
            string textoNormalizado = texto.Normalize(NormalizationForm.FormD);
            StringBuilder textoSemAcentos = new StringBuilder();

            for (int i = 0; i < textoNormalizado.Length; i++)
            {
                UnicodeCategory categoria = CharUnicodeInfo.GetUnicodeCategory(textoNormalizado[i]);
                if (categoria != UnicodeCategory.NonSpacingMark)
                {
                    textoSemAcentos.Append(textoNormalizado[i]);
                }
            }

            return textoSemAcentos.ToString();
        }





    }
}
