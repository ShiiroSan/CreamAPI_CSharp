using IniParser;
using IniParser.Model;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CreamAPI_CSharp
{
    internal class CApiFileGest
    {
        private FileIniDataParser capiParser = new FileIniDataParser();

        public void CApiDlcWriter(string capiIniFilePath, int appID, List<List<string>> DLCList)
        {
            capiParser.Parser.Configuration.CommentString = ";";
            IniData data = capiParser.ReadFile(capiIniFilePath);
            data["steam"]["appid"] = appID.ToString();
            //IniFile CapiINI = new IniFile(capiIniFilePath);
            //CapiINI.Write("appid", appID.ToString(), "steam");
            for (int i = 0; i < DLCList.Count; i++)
            {
                data["dlc"][DLCList[i][0]] = DLCList[i][1];
                //CapiINI.Write(DLCList[i][0], DLCList[i][1], "dlc");
            }
            capiParser.WriteFile(capiIniFilePath, data);
        }

        public void CApiIniConfig(string capiIniFilePath)
        {
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
                //bool bWrapperMode = bool.Parse(sWrapperMode);

                //fileHandle.Close();
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
            else
            {
                uncommentLine(capiIniFilePath, ";language");
                IniData capiData = capiParser.ReadFile(capiIniFilePath);
                capiData["steam"]["language"] = Regex.Match(System.Globalization.CultureInfo.CurrentCulture.EnglishName.ToLower(), @"([a-z]+)\s").Value;
                capiParser.WriteFile(capiIniFilePath, capiData);
            }
        }

        private void uncommentLine(string file, string toUncomment)
        {
            string oFileRead = File.ReadAllText(file);
            RegexOptions options = RegexOptions.Multiline;

            Regex regex = new Regex(toUncomment, options);
            string result = regex.Replace(oFileRead, toUncomment.Remove(0, 1));
            File.WriteAllText(file, result);
        }
    }
}