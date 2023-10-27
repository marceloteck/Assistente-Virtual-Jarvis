using InteligenciaArtificial.ClassGlobal;
using InteligenciaArtificial.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Speech;
using System.Speech.Recognition;
using System.Speech.AudioFormat;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.CodeDom.Compiler;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Policy;
using InteligenciaArtificial.Forms.Comandos;
using System.Text.RegularExpressions;

namespace InteligenciaArtificial.Reconhecimento
{
    public class Rec
    {
        public static void reconhecimento(string speech)
        {
			try
			{
				LoteComands.Executar(speech);
			}
			catch(Exception ex)
			{
				MessageBox.Show("Aconteceu um erro nos comandos: " + ex);
			}
            
        }//reconhecimento

        public static void chatIa(string speech)
        {
			try
			{
				LoteComands.Executar(speech, true);
			}
			catch(Exception ex)
			{
				MessageBox.Show("Aconteceu um erro nos comandos: " + ex);
			}
        }
    }


    public static class LoteComands
    {
        public static bool Verific(string speech, string text)
        {
            if (text.Normalize(NormalizationForm.FormD)
                .Equals(speech.Normalize(NormalizationForm.FormD),
                 StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            else { return false; }
        }
        public static bool VerificArr(string speech, string[] text)
        {
            if (text.Any(x => x.Normalize(NormalizationForm.FormD)
                .Equals(speech.Normalize(NormalizationForm.FormD),
                 StringComparison.InvariantCultureIgnoreCase)))
            {
                return true;
            }
            else { return false; }
        }

        public static bool StartSpeeck = false;
		public static string[] resposta = null;
		public static void Executar(string speech, bool ChatIo = false)
		{

			//string[] TextArr = ComandsLotesG.ChoicesArrList.ToArray();
			string[] TextArr = ComandsLotesG.ChoicesArrList.Select(s => s.ToLowerInvariant()).ToArray();


			foreach (string palavra in TextArr)
			{
				speech = speech.Replace(palavra, ""); // remove a palavra
			}
			speech = speech.ToLower().Trim(); // remove espaços m branco

			if (VerificArr(speech, ComandsLotesG.ListLote.ToArray()))
			{
				foreach (string ListComands in ComandsLotesG.ComandsEmLote)
				{
					try
					{
                        string[] info = ListComands.Split(';');
                        string[] text = info[0].Split(new string[] { "text=" }, StringSplitOptions.RemoveEmptyEntries);
                        resposta = info[1].Split(new string[] { "resposta=" }, StringSplitOptions.RemoveEmptyEntries);
                        string[] executar = info[2].Split(new string[] { "executar=" }, StringSplitOptions.RemoveEmptyEntries);

                        text = text[0].Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string textP in text)
						{
							string texts = textP.ToLower().Trim();
							if (Verific(speech, texts))
							{
								// EXECUTAR
								if (executar != null || resposta != null)
								{
                                    executar = executar[0].Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
									resposta = resposta[0].Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);


                                    foreach (string partes in executar)
									{

                                        #region código antigo if else
                                        if (executar.Any(x => x.Equals("ActiveSpeech", StringComparison.InvariantCultureIgnoreCase)))
										{
											StartSpeeck = true;
											Speaker.SpeakRand(resposta);
										}
										else if (executar.Any(x => x.Equals("InativeSpeech", StringComparison.InvariantCultureIgnoreCase)) && StartSpeeck == true)
										{
											StartSpeeck = false;
											Speaker.StopSpeak();
											Speaker.SpeakRand(resposta);
										}
										else if (StartSpeeck == true || ChatIo == true)
										{
											#region funcitons
											if (executar.Any(x => x.Equals("HorasSpeech", StringComparison.InvariantCultureIgnoreCase)))
											{
												Actions.Horas();
											}
											else if (executar.Any(x => x.Equals("DataSpeech", StringComparison.InvariantCultureIgnoreCase)))
											{
												Actions.Data();
											}
											else if (executar.Any(x => x.Equals("DiaSpeech", StringComparison.InvariantCultureIgnoreCase)))
											{
												Actions.Dia();
											}
											else if (executar.Any(x => x.Equals("SemanaSpeech", StringComparison.InvariantCultureIgnoreCase)))
											{
												Actions.Semana();
											}
											else if (executar.Any(x => x.Equals("MesSpeech", StringComparison.InvariantCultureIgnoreCase)))
											{
												Actions.Mes();
											}
											else if (executar.Any(x => x.Equals("AnoSpeech", StringComparison.InvariantCultureIgnoreCase)))
											{
												Actions.Ano();
											}
											else if (executar.Any(x => x.Equals("AddComands", StringComparison.InvariantCultureIgnoreCase)))
											{
												AddComands1 addc = new AddComands1();
												addc.Show();
                                                Speaker.SpeakRand(resposta);

                                            }
											else if (executar.Any(x => x.Equals("navigator", StringComparison.InvariantCultureIgnoreCase)))
											{
												Speaker.SpeakRand(resposta);
												Automacao.Automatic(executar[1]);
												break;
											}
											else if (executar.Any(x => x.Equals("StopProcess", StringComparison.InvariantCultureIgnoreCase)))
											{

												if (Confirmar("Deseja realmente fechar todos os programas do Windows?"))
												{
													Actions.fecharTudo();
													Speaker.SpeakRand(resposta);
												}

											}
											else if (partes.Contains("interpolar:"))
											{
												string Execut = partes.Replace("interpolar:", "");
												try
												{
                                                    Process.Start($"{Execut}");
                                                    Speaker.SpeakRand(resposta);
                                                }
												catch(Exception ex) 
												{
													MessageBox.Show("Erro aqui no interpolar: " + ex.Message);
												}
												
											}
											else if (partes.Contains("chrome.exe:"))
											{
												string Execut = partes.Replace("chrome.exe:", "");
                                                try
												{
													Process.Start("chrome.exe", $"{Execut}");
                                                    //Speaker.SpeakRand(resposta);
                                                }
												catch(Exception ex) 
												{
													MessageBox.Show("Erro aqui no chrome.exe: " + ex.Message);
												}
												
											}
											else
											{
												if (resposta != null) { Speaker.SpeakRand(resposta); } // RESPOSTA
												if (executar != null)
												{
													try
													{
														Process.Start(partes);
													}
													catch (Exception e)
													{
														Speaker.SpeakRand("Devido a um erro não conseguir abrir o processo: " + e.Message);
													}
												} // EXECUTAR
											}
											#endregion
										} // if
										
                                        #endregion






                                    } // foreach partes
                                }
								//break;
							}
						}

					}
					catch (Exception ex)
					{
						MessageBox.Show("Aconteceu um erro dentro do bloco de Comandos: " + ex);
					}
				}
			}
		}


        public static bool Confirmar(string mensagem)
        {
            DialogResult resultado = MessageBox.Show(mensagem, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
