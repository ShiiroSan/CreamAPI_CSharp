﻿using Microsoft.Win32;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CreamAPI_CSharp
{
    internal class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        ///
        public static bool noGame = false;

        public static string gameDir = "";
        public static string capiVersion = "v3.4.1.0";

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //DLCLister dlcLister = new DLCLister(440);
            //dlcLister.ShowDialog();
            string caFileConfig = "caconfig.ini";
#pragma warning disable CS0219 // La variable est assignée mais sa valeur n'est jamais utilisée
            bool bConfigShow;
#pragma warning restore CS0219 // La variable est assignée mais sa valeur n'est jamais utilisée
            if (File.Exists(caFileConfig))
            {
                bConfigShow = false;
            }
            else
            {
                //Show Config gui
                bConfigShow = true;
            }

            searchForm searchForm = new searchForm();
            if (MessageBox.Show("Do you want to use an installed game?", "", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                gameDir = "";
                noGame = true;
            }
            else
            {
                #region Get Steam Path

                object sBaseDirSteam = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Valve\\Steam\\", "SteamPath", "C:\\Program Files(x86)\\Steam");
                string sConfSteamFile = File.ReadAllText(sBaseDirSteam + "\\config\\config.vdf");
                string sregGetDir = "(?:BaseInstallFolder_1\"\t+\"(.*)\")";
                string line9clear = Regex.Match(sConfSteamFile, sregGetDir).Groups[1].ToString();
                line9clear = line9clear.Replace(@"\\", @"\");
                string defaultGameFolder = line9clear + "\\SteamApps\\common\\";
                Console.WriteLine(defaultGameFolder);
                string dummyFileName = "This game!";
                SaveFileDialog sf = new SaveFileDialog
                {
                    InitialDirectory = defaultGameFolder,
                    FileName = dummyFileName,
                    Title = "Select the game you desire to crack"
                };
                DialogResult result = sf.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string savePath = Path.GetDirectoryName(sf.FileName);
                    if (savePath != "" || savePath != defaultGameFolder)

                    {
                        gameDir = savePath;
                        string[] ArrfullDirWGameName = gameDir.Split("\\".ToCharArray());
                        string guessGameName = ArrfullDirWGameName[ArrfullDirWGameName.Length - 1];
                        searchForm.setGameNameInputText(guessGameName);
                        noGame = false;
                    }
                }
                else //si on sélectionne pas le jeu, on applique la procédure du noGame
                {
                    gameDir = "";
                    noGame = true;
                }

                #endregion Get Steam Path
            }

            searchForm.ShowDialog();
            searchForm.WindowState = FormWindowState.Normal;
            searchForm.SelectNextControl(searchForm.ActiveControl, true, true, true, true);
            searchForm.Activate();
        }
    }
}