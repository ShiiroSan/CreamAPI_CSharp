using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CreamAPI_CSharp
{
    public partial class DLCLister : Form
    {
        public DLCLister(int appID)
        {
            InitializeComponent();
            richTextBox1.AppendText("Requesting DLC for AppID: ");
            richTextBox1.AppendText(appID.ToString(), Color.White);
            richTextBox1.AppendText(Environment.NewLine);

            WriteAndPatch(appID);
        }

        private void CheckEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appID">Appid of the game to get DLC of</param>
        /// <param name="statusCode">Can have different return value:
        /// - isok              means everything is cool man
        /// - tableDLCisnull    name speak by itself</param>
        /// <returns>A list of list of string containing DLC value and their names.</returns>
        private List<List<string>> GetDLCForSelectedAppIDAsync(int appID, ref string statusCode)
        {
            statusCode = "isok";
            string steamDBURLDlc = "https://steamdb.info/app/" + appID.ToString() + "/dlc/";
            List<List<string>> tableDLC = WebGestion.GetTableFromPages(steamDBURLDlc, "//*[@id=\"dlc\"]/div/table"); //07.07.19 changed //*[@id="dlc"]/table to //*[@id="dlc"]/div/table
            if (tableDLC == null)
            {
                statusCode = "tableDLCisnull";
            }
            return tableDLC;
        }

        private void WriteAndPatch(int appID)
        {
            richTextBox1.AppendText(Environment.NewLine);
            richTextBox1.AppendText("Searching DLC for: ");
            richTextBox1.AppendText(GameSelecForm.gameName, Color.White);
            richTextBox1.AppendText(Environment.NewLine);
            richTextBox1.AppendText("Accessing SteamDB.info with desired appid.");
            string steamDBURLDlc = "https://steamdb.info/app/" + appID.ToString() + "/dlc/";
            richTextBox1.AppendText(Environment.NewLine);
            richTextBox1.AppendText("URL read is: ");
            richTextBox1.AppendText(steamDBURLDlc);
            richTextBox1.AppendText(Environment.NewLine);
            richTextBox1.AppendText(Environment.NewLine);
            richTextBox1.AppendText("Gathering DLCs informations. This can take some time depending on your network connection and amount of DLC.");
            string statusCode = "";
            var tableDLC = GetDLCForSelectedAppIDAsync(appID, ref statusCode);
            if (statusCode == "tableDLCisnull")
            {
                richTextBox1.AppendText(Environment.NewLine);
                richTextBox1.AppendText(Environment.NewLine);
                richTextBox1.AppendText("There is NO DLC FOR THIS GAME, STOP BEING STUPID.", Color.Red);
                richTextBox1.AppendText(Environment.NewLine);
                richTextBox1.AppendText("Maybe you should close it and learn how to use your brain.", Color.Red);
                MessageBox.Show("No DLC was found.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ArrayList dlcList = new ArrayList();
                richTextBox1.AppendText(Environment.NewLine);
                richTextBox1.AppendText(Environment.NewLine);
                richTextBox1.AppendText("Number of DLC found: ");
                richTextBox1.AppendText(tableDLC.Count.ToString(), Color.White);
                richTextBox1.AppendText(Environment.NewLine);
                for (int i = 0; i < tableDLC.Count; i++)
                {
                    dlcList.Add(new gameList(tableDLC[i][0], tableDLC[i][1]));
                    richTextBox1.AppendText(Environment.NewLine);
                    richTextBox1.AppendText("DLC ID:    ");
                    richTextBox1.AppendText(tableDLC[i][0], Color.White);
                    richTextBox1.AppendText(Environment.NewLine);
                    richTextBox1.AppendText("DLC Name:  ");
                    richTextBox1.AppendText(tableDLC[i][1], Color.White);
                    richTextBox1.AppendText(Environment.NewLine);
                    //  }
                }

                if (tableDLC.Count >= 50)
                {
                    richTextBox1.AppendText(Environment.NewLine);
                    richTextBox1.AppendText("SteamDB.info have issue with 50+ DLCs, it's due to Steam API limitation. You should " +
                        "check it out on your own. More info will soon be found by looking on git page and README. But not know.", Color.Yellow);
                }
                bool justShowing = false;
                if (MessageBox.Show("Would you like to patch the game?", "Sure about patching?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    justShowing = false;
                }
                else
                {
                    justShowing = true;
                }

                if (!justShowing)
                {
                    richTextBox1.AppendText(Environment.NewLine);
                    richTextBox1.AppendText("The file will be saved on: ");
                    string gameDir;
                    byte steam_api = 0;
                    if (Program.noGame)
                    {
                        gameDir = Environment.CurrentDirectory.ToString() + "\\" + GameSelecForm.gameName + "\\";
                        if (!(Directory.Exists(gameDir)))
                        {
                            Directory.CreateDirectory(gameDir);
                        }
                    }
                    else
                    {
                        gameDir = Program.gameDir + "\\";
                        steam_api = GetSteamPresence(gameDir);
                        if (steam_api == 0)
                        {
                            OpenFileDialog getSteamApiDllFile = new OpenFileDialog
                            {
                                InitialDirectory = gameDir,
                                Filter = "steam_api(64).dll |steam_api.dll; steam_api64.dll"
                            };
                            if (getSteamApiDllFile.ShowDialog() == DialogResult.OK)
                            {
                                gameDir = getSteamApiDllFile.FileName.Replace(getSteamApiDllFile.SafeFileName, "");
                            }
                            else
                            {
                                MessageBox.Show("You're dumb!", "Stupid men ...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Application.Exit();
                            }
                            steam_api = GetSteamPresence(gameDir);
                        }
                    }
                    richTextBox1.AppendText(gameDir, Color.Yellow);
                    string capiFilesDir = Environment.CurrentDirectory + "\\CreamAPI_" + Program.capiVersion + "\\";
                    bool isCrackAlreadyPresent = false;
                    if (File.Exists(gameDir + "cream_api.ini"))
                    {
                        isCrackAlreadyPresent = true;
                    }
                    if (!Program.noGame)
                    {
                        if (!isCrackAlreadyPresent)
                        {
                            if (steam_api == 1)
                            {
                                File.Move(gameDir + "steam_api.dll", gameDir + "steam_api_o.dll");
                            }

                            if (steam_api == 2)
                            {
                                File.Move(gameDir + "steam_api64.dll", gameDir + "steam_api64_o.dll");
                            }

                            if (steam_api == 3)
                            {
                                File.Move(gameDir + "steam_api.dll", gameDir + "steam_api_o.dll");
                                File.Move(gameDir + "steam_api64.dll", gameDir + "steam_api64_o.dll");
                            }
                        }
                    }
                    if (!isCrackAlreadyPresent)
                    {
                        if (steam_api == 1)
                        {
                            File.Copy(capiFilesDir + "steam_api.dll", gameDir + "steam_api.dll");
                        }

                        if (steam_api == 2)
                        {
                            File.Copy(capiFilesDir + "steam_api64.dll", gameDir + "steam_api64.dll");
                        }

                        if (steam_api == 3)
                        {
                            File.Copy(capiFilesDir + "steam_api.dll", gameDir + "steam_api.dll");
                            File.Copy(capiFilesDir + "steam_api64.dll", gameDir + "steam_api64.dll");
                        }
                        File.Copy(capiFilesDir + "cream_api.ini", gameDir + "cream_api.ini");
                    }
                    CApiFileGest cApiFileGest = new CApiFileGest();
                    cApiFileGest.CApiIniConfig(gameDir + "cream_api.ini");
                    cApiFileGest.CApiDlcWriter(gameDir + "cream_api.ini", appID, tableDLC);
                    richTextBox1.AppendText(Environment.NewLine);
                    richTextBox1.AppendText(Environment.NewLine);

                    string strPathXML = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + "\\patchedCApiGames.xml";
                    GamePatchSaveMgmt saveMgmt = new GamePatchSaveMgmt(strPathXML);

                    List<string> lsStrreadedGame = saveMgmt.ReadAppIDXml(appID.ToString());
                    List<string> lsStrGameInfo = new List<string>
                    {
                        appID.ToString(),
                        GameSelecForm.gameName,
                        gameDir.ToString(),
                        tableDLC.Count.ToString()
                    };
                    if (lsStrreadedGame.Count == 1)
                    {
                        if (lsStrreadedGame[0] == "no_file")
                        {
                            XDocument doc = new XDocument(
                                    new XElement("crackedgame")
                                );
                            doc.Save(strPathXML);
                            bool boolWrite = saveMgmt.WriteToXML(lsStrGameInfo);
                        }
                        if (lsStrreadedGame[0] == "not_found")
                        {
                            bool boolWrite = saveMgmt.WriteToXML(lsStrGameInfo);
                        }
                    }
                    else
                    {
                        bool hasInfoChange = false;
                        for (int i = 0; i < lsStrreadedGame.Count; i++)
                        {
                            if (lsStrGameInfo[i] != lsStrreadedGame[i])
                            {
                                hasInfoChange = true;
                                break;
                            }
                        }
                        if (hasInfoChange)
                        {
                            bool bgitre = saveMgmt.UpdateXMLElement(lsStrGameInfo);
                            bgitre = saveMgmt.WriteToXML(lsStrGameInfo);
                        }
                    }

                    richTextBox1.AppendText("The game is now patched!", Color.Gray);
                    richTextBox1.AppendText(Environment.NewLine);
                    richTextBox1.AppendText("You can run it directly from Steam, or via exe's file.\n", Color.Gray);
                    richTextBox1.AppendText("Keep in mind this patch isn't universal, so it might not work\n", Color.Gray);
                    richTextBox1.AppendText("With various game.\n" +
                        "Have fun.", Color.Gray);
                    richTextBox1.AppendText(Environment.NewLine);
                    richTextBox1.AppendText("BTw, press {enter} to leave.", Color.Gray);
                    Task.Factory.StartNew(() => richTextBox1.KeyPress += new KeyPressEventHandler(CheckEnter)).Wait(TimeSpan.FromSeconds(25.0));
                }
            }
        }

        private byte GetSteamPresence(string gamePath)
        {
            ///<summary>
            ///This whole part is here to be able to know
            ///which version of SteamAPI is present (if it's x64 or not, and
            ///both or only one.
            ///
            ///This allow to copy only required file. And allow me to get easier
            ///the presence of steam_api.dll
            ///
            /// If output value is 1: only steam_api.dll
            /// Output value is 2: only steam_api64.dll
            /// Output value is 3: there is both
            /// 0 : nothing
            ///</summary>
            bool steamapidllExist = File.Exists(gamePath + "\\steam_api.dll");
            bool steamapi64dllExist = File.Exists(gamePath + "\\steam_api64.dll");

            byte steamApiPresent = 0;
            if (steamapidllExist)
            {
                steamApiPresent += 1;
            }

            if (steamapi64dllExist)
            {
                steamApiPresent += 2;
            }

            return steamApiPresent;
        }
    }
}