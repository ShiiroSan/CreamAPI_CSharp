using IniParser;
using IniParser.Model;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CreamAPI_CSharp
{
    internal class CApiFileGest
    {
        public void CApiDlcWriter(string capiIniFilePath, int appID, List<List<string>> DLCList)
        {
            IniFile CapiINI = new IniFile(capiIniFilePath);
            CapiINI.Write("appid", appID.ToString(), "steam");
            for (int i = 0; i < DLCList.Count; i++)
            {
                CapiINI.Write(DLCList[i][0], DLCList[i][1], "dlc");
            }
        }

        public void CApiIniConfig(string capiIniFilePath)
        {
            FileStream fileHandle = File.Open(capiIniFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            string caFileConfig = "caconfig.ini";
            if (File.Exists("caconfig.ini"))
            {
                FileIniDataParser iniParser = new FileIniDataParser();
                IniData data = iniParser.ReadFile(caFileConfig);
                string sLangCfg = data["default"]["language"];
                if (sLangCfg == "")
                {
                    sLangCfg = "English";
                }
                else
                {
                    uncommentLine(capiIniFilePath, ";language");
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
                else
                {
                    uncommentLine(capiIniFilePath, ";lowviolence");
                }
                bool bLowViolence = bool.Parse(sLowViolence);
                string sWrapperMode = data["default"]["wrapperMode"];
                if (sWrapperMode == "")
                {
                    sWrapperMode = "False";
                }
                bool bWrapperMode = bool.Parse(sWrapperMode);

                fileHandle.Close();
                FileIniDataParser capiParser = new FileIniDataParser();
                IniData capiData = capiParser.ReadFile(capiIniFilePath);
                if (!(sLangCfg == "English"))
                {
                    capiData["steam"]["language"] = sLangCfg.ToLower();
                }

                if (!(sExtraProtecBypass == "False"))
                {
                    capiData["steam"]["extraprotection"] = sExtraProtecBypass.ToLower();
                }

                if (!(sLowViolence == "False"))
                {
                    capiData["steam"]["lowviolence"] = sLowViolence.ToLower();
                }

                if (!(sOfflineMode == "False"))
                {
                    capiData["steam"]["forceoffline"] = sOfflineMode.ToLower();
                }

                capiParser.WriteFile(capiIniFilePath, capiData);
            }
        }

        private void uncommentLine(string file, string toUncomment)
        {
            FileStream fileHandle = File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            RegexOptions options = RegexOptions.Multiline;

            Regex regex = new Regex(toUncomment, options);
            string result = regex.Replace(fileHandle.ToString(), toUncomment.Remove(0, 1));
            File.WriteAllText(file, result);
        }
    }
}