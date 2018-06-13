using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using IniParser;
using IniParser.Model;
using Microsoft.Win32;

namespace CreamAPI_CSharp
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string caFileConfig = "caconfig.ini";
            bool bConfigShow;
            if (File.Exists(caFileConfig))
            {
                var iniParser = new FileIniDataParser();
                IniData data = iniParser.ReadFile(caFileConfig);
                string sLangCfg = data["default"]["language"];
                if (sLangCfg == "")
                {
                    sLangCfg = "English";
                }
                string sExtraProtecBypass = data["default"]["extraProtectionBypass"];
                if (sExtraProtecBypass == "")
                {
                    sExtraProtecBypass = "False";
                }
                bool bExtraProtecBypass = bool.Parse(sExtraProtecBypass);
                string sOfflineMode = data["default"]["offlineMode"];
                if (sOfflineMode == "")
                {
                    sOfflineMode = "False";
                }
                bool bOfflineMode = bool.Parse(sOfflineMode);
                string sUserdataFolder = data["default"]["userdataFolder"];
                if (sUserdataFolder == "")
                {
                    sUserdataFolder = "False";
                }
                bool bUserdataFolder = bool.Parse(sUserdataFolder);
                string sLowViolence = data["default"]["lowviolence"];
                if (sLowViolence == "")
                {
                    sLowViolence = "False";
                }
                bool bLowViolence = bool.Parse(sLowViolence);
                string sWrapperMode = data["default"]["wrapperMode"];
                if (sWrapperMode == "")
                {
                    sWrapperMode = "False";
                }
                bool bWrapperMode = bool.Parse(sWrapperMode);
                Console.WriteLine(sLangCfg);
                bConfigShow = false;
            }
            else
        	{
                //Show Config gui
                bConfigShow = true;
            }

            #region Get Steam Path
            var sBaseDirSteam = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Valve\\Steam\\", "SteamPath", "C:\\Program Files(x86)\\Steam");
            //var sConfSteamFile = File.Open(sBaseDirSteam + "\\config\\config.vdf",FileMode.Open);
            //var sConfFileRead = sConfSteamFile.Read();
            //getSteamPath with Regex
            string gameDir = "";
            #endregion Get Steam Path
            bool noGame;
            if (gameDir == "")
            {
                noGame = true;
            }
            else
            {
                noGame = false;
            }
            if (!noGame)
            {
                //Split game path to find name

            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new searchGameForm());

        }


    }
}
