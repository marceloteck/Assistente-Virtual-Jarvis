using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InteligenciaArtificial.Files
{
    public static class Arquivos
    {
        public static string config         = Path.Combine(Application.StartupPath, "config/config.ns");
        public static string IgnorSpeech    = Path.Combine(Application.StartupPath, "config/IgnorSpeech.ns");
        public static string text           = Path.Combine(Application.StartupPath, "config/text.ns");

        public static string frases         = Path.Combine(Application.StartupPath, "comandos/frases.txt");
        public static string WordsFiles     = Path.Combine(Application.StartupPath, "comandos/WordsFiles.txt");
        public static string memoriaString  = Path.Combine(Application.StartupPath, "comandos/memoriaString.txt");
    }
}
