using System;
using NAudio.Wave;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Diagnostics;

namespace JARVIS.Forms
{
    public partial class Microfone : Form
    {
        private UdpClient client;
        private bool isConnected;

        public Microfone()
        {
            InitializeComponent();
            isConnected = false;
            labelStatus.Text = "Desconectado";
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                try
                {
                    client = new UdpClient();
                    client.Connect(textBoxIP.Text, int.Parse(textBoxPort.Text));
                    isConnected = true;
                    labelStatus.Text = "Conectando...";
                    client.BeginReceive(new AsyncCallback(ReceiveAudio), null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao conectar: " + ex.Message);
                    isConnected = false;
                    labelStatus.Text = "Desconectado";
                }
            }
            else
            {
                isConnected = false;
                client.Close();
                labelStatus.Text = "Desconectado";
            }
        }

        private void ReceiveAudio(IAsyncResult ar)
        {
            if (isConnected)
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] buffer = client.EndReceive(ar, ref endPoint);

                // Processa o buffer de áudio recebido
                // ...

                // Envia o buffer de áudio processado para o Wo Mic
                SendAudio(buffer, textBoxIP.Text, int.Parse(textBoxPort.Text));

                // Inicia uma nova chamada para receber mais áudio
                client.BeginReceive(new AsyncCallback(ReceiveAudio), null);
            }
        }



        public void SendAudio(byte[] audioData, string ip, int port)
        {
            UdpClient udpClient = new UdpClient();
            try
            {
                udpClient.Connect(IPAddress.Parse(ip), port);
                udpClient.Send(audioData, audioData.Length);
            }
            catch (Exception ex)
            {
                // Handle exception here
            }
            finally
            {
                udpClient.Close();
            }
        }

    }
}