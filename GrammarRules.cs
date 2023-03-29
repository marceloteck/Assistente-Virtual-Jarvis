using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace IA_JARVIS
{
    public class GrammarRules
    {
        public static IList<string> ComandVoice = new List<string>();
        public static IList<string> shellCommands = new List<string>();
        public static IList<string> ActionC = new List<string>();

        public static IList<string> IaStopListening = new List<string>();
        public static IList<string> IaStartListening = new List<string>();

        public static IList<string> WhatTimeIS = new List<string>();
        public static IList<string> WhatDateIs = new List<string>();

        static GrammarRules()
        {
            // Carregar comandos de voz
            string voiceCommandsPath = Path.Combine(Application.StartupPath, "Data_Base/voice.txt");
            LoadFromFile(voiceCommandsPath, ComandVoice);

            // Carregar lista de comandos shell
            string shellCommandsPath = Path.Combine(Application.StartupPath, "Data_Base/shellComands.txt");
            LoadFromFile(shellCommandsPath, shellCommands);

            // Carregar lista de ações em C#
            string actionCPath = Path.Combine(Application.StartupPath, "Data_Base/ActionC.txt");
            LoadFromFile(actionCPath, ActionC);
            
            // Carregar lista de ações em C#
            string IaStopListeningPath = Path.Combine(Application.StartupPath, "Data_Base/fixo/IaStopListening.txt");
            LoadFromFile(IaStopListeningPath, IaStopListening); 
            
            // Carregar lista de ações em C#
            string IaStartListeningPath = Path.Combine(Application.StartupPath, "Data_Base/fixo/IaStartListening.txt");
            LoadFromFile(IaStartListeningPath, IaStartListening);

            // Carregar lista de ações em C#
            string WhatTimeISPath = Path.Combine(Application.StartupPath, "Data_Base/fixo/WhatTimeIS.txt");
            LoadFromFile(WhatTimeISPath, WhatTimeIS);

            // Carregar lista de ações em C#
            string WhatDateIsPath = Path.Combine(Application.StartupPath, "Data_Base/fixo/WhatDateIs.txt");
            LoadFromFile(WhatDateIsPath, WhatDateIs);
        }

        public static void LoadFromFile(string filePath, IList<string> targetList)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] arrLine = line.Split('|');
                        targetList.Add(arrLine[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar arquivo {filePath}: " + ex.Message);
            }
        }
    }
}
