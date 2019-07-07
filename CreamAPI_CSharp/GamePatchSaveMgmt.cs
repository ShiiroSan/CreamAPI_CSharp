using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace CreamAPI_CSharp
{
    internal class GamePatchSaveMgmt
    {
        public string XMLPath;

        public GamePatchSaveMgmt(string XMLPath)
        {
            this.XMLPath = XMLPath;
        }

        public bool WriteToXML(List<string> gameInfo)
        {
            string path = XMLPath;
            if (!File.Exists(path))
            {
                //Console.WriteLine("Fichier introuvable!");
                return false;
            }
            XDocument doc = XDocument.Load(path);
            /*
             * appid
             *      game name
             *      game path
             */
            string strAppID, strGameName, strGamePath, strDlcNum;
            strAppID = gameInfo[0].ToString();
            strGameName = gameInfo[1];
            strGamePath = gameInfo[2];
            strDlcNum = gameInfo[3];
            doc.Element("crackedgame").Add(new XElement("appid_" + strAppID,
                   new XElement("gamename", strGameName),
                   new XElement("gamepath", strGamePath),
                   new XElement("dlcnum", strDlcNum)
                ));
            doc.Save(path);
            return true;
        }

        public List<string> ReadAppIDXml(string appid)
        {
            List<string> lsStrAppidGameInfo = new List<string>();
            string path = XMLPath;
            List<string> lsStrAppidGameInfo1 = lsStrAppidGameInfo;
            if (!File.Exists(path))
            {
                //Console.WriteLine("Fichier introuvable!");
                lsStrAppidGameInfo1.Add("no_file");
                return lsStrAppidGameInfo1;
            }
            XDocument doc = XDocument.Load(path);

            XElement prnt = doc.Element("crackedgame").Element("appid_" + appid);
            if (prnt == null)
            {
                lsStrAppidGameInfo1.Add("not_found");
                return lsStrAppidGameInfo1; //l'élément existe pas, donc on le notifie au programme.
            }
            else
            {
                lsStrAppidGameInfo1.Add(prnt.Name.ToString());
                foreach (XNode node in prnt.Nodes())
                {
                    lsStrAppidGameInfo1.Add(Regex.Match(node.ToString(), @"<[a-z]+>(.*)<\/[a-z]+>").Groups[1].Value);
                }
            }
            return lsStrAppidGameInfo1;
        }

        public bool UpdateXMLElement(List<string> gameInfo)
        {
            string path = XMLPath;
            if (!File.Exists(path))
            {
                //Console.WriteLine("Fichier introuvable!");
                return false;
            }
            XDocument doc = XDocument.Load(path);
            doc.Element("crackedgame").Element("appid_" + gameInfo[0]).Remove();
            doc.Save(path);
            return true;
        }
    }
}