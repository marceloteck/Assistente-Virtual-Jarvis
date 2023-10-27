using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using InteligenciaArtificial.Files;

namespace InteligenciaArtificial.Forms.Comandos
{
    public partial class AddComands1 : Form
    {
        public AddComands1()
        {
            InitializeComponent();

            /*string arquivo = FileDir("ComandsLote.txt");
            // Lê o arquivo principal linha por linha
            using (StreamReader sr = new StreamReader(arquivo))
            {
                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    // Verifica se a linha contém um link para outro arquivo
                    if (linha.Contains(".txt"))
                    {
                        // Lê o arquivo vinculado ao link
                        listBox1.Items.Add(linha.Trim());
                    }
                }
            }*/

            ContudLote(FileDir("ComandsLote.txt"), listBox1);
        }

        private void ContudLote(string caminhoArquivo, ListBox listBox)
        {
            // Cria um objeto FileSystemWatcher para monitorar alterações no arquivo
            FileSystemWatcher watcher = new FileSystemWatcher(Path.GetDirectoryName(caminhoArquivo), Path.GetFileName(caminhoArquivo));

            // Configura o objeto watcher para notificar alterações no arquivo
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.EnableRaisingEvents = true;

            // Evento disparado quando o arquivo é alterado
            watcher.Changed += (sender, e) =>
            {
                // Limpa os itens do listBox
                listBox.Items.Clear();

                // Lê o arquivo novamente e adiciona os links no listBox
                using (StreamReader sr = new StreamReader(caminhoArquivo))
                {
                    string linha;
                    while ((linha = sr.ReadLine()) != null)
                    {
                        if (linha.Contains(".txt"))
                        {
                            listBox.Items.Add(linha.Trim());
                        }
                    }
                }
            };

            // Lê o arquivo inicialmente e adiciona os links no listBox
            using (StreamReader sr = new StreamReader(caminhoArquivo))
            {
                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    if (linha.Contains(".txt"))
                    {
                        listBox.Items.Add(linha.Trim());
                    }
                }
            }
        }



        public static string FileDir( string DirF )
        {
            return Path.Combine(Application.StartupPath, "comandos/ComandsEmLote/" + DirF);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //novo
                string nomeArquivoSelecionado = listBox1.SelectedItem.ToString();
                if (nomeArquivoSelecionado != null)
                {
                    NovoComando formAbrir = new NovoComando(nomeArquivoSelecionado);
                    formAbrir.Show();
                }
                else
                {
                    MessageBox.Show("É preciso selecionar uma linha!");
                }
            }
            catch
            {
                MessageBox.Show("É preciso selecionar uma linha!");
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            // editar
            try
            {
                string EditComands = listBox1.SelectedItem.ToString();

                if (EditComands != null)
                {
                    EditarComands formAbrir = new EditarComands(EditComands);
                    formAbrir.Show();
                }
                else
                {
                    MessageBox.Show("É preciso selecionar uma linha!");
                }

            }
            catch
            {
                MessageBox.Show("É preciso selecionar uma linha!");
            }
        }
        private static string delet = null;
        private void button2_Click(object sender, EventArgs e)
        {
            // excluir
            try
            {
                delet = listBox1.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("É preciso selecionar uma linha! ERRO: " + ex.Message);
            }
            if (delet != null)
            {
                // Mensagem de confirmação para o usuário
                string mensagem = "Tem certeza que deseja excluir o arquivo " + FileDir(delet) + "?";

                // Exibe a mensagem de confirmação para o usuário e verifica se ele clicou em "OK"
                if (MessageBox.Show(mensagem, "Confirmação de exclusão", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
			    {
				    try
				    {
                        // fazer uma cópia
                        string texto = File.ReadAllText(FileDir( delet ));
					    System.IO.File.WriteAllText(FileDir( "backup_" + delet ), texto);
                        // Exclui o arquivo
					    File.Delete(FileDir( delet ));
                        RemoveLinha(FileDir("ComandsLote.txt"), delet);

                        // Exibe mensagem de sucesso
                        MessageBox.Show("Arquivo excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
				    }
				    catch (Exception ex)
				    {
					    // Exibe mensagem de erro em caso de falha na exclusão do arquivo
					    MessageBox.Show("Erro ao excluir o arquivo: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
				    }
			    }
            }

        }
        public static void RemoveLinha(string arquivo, string texto)
        {
            // Lê todas as linhas do arquivo
            List<string> linhas = File.ReadAllLines(arquivo).ToList();

            // Percorre as linhas e verifica se cada uma contém o texto a ser removido
            for (int i = 0; i < linhas.Count; i++)
            {
                if (linhas[i].Contains(texto))
                {
                    // Remove a linha se contiver o texto
                    linhas.RemoveAt(i);

                    // Atualiza o contador para compensar a remoção da linha
                    i--;
                }
            }

            // Sobrescreve o arquivo com as linhas atualizadas
            File.WriteAllLines(arquivo, linhas);
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            FileNewText NewTxt = new FileNewText();
            NewTxt.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
