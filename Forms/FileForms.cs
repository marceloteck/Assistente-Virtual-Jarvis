using InteligenciaArtificial.Forms.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteligenciaArtificial.Forms
{
    public class FileForms
    {
        public static void ChatNativo()
        {
            ChatNativo chatnativo = new ChatNativo();
            chatnativo.Show();
        }
        public static void ListaDeComandos()
        {
            ListadeComandos ListComds = new ListadeComandos();
            ListComds.Show();
        }
        public static void AddComands1()
        {
            AddComands1 AddComands = new AddComands1();
            AddComands.Show();
        }
    }
}
