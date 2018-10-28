using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreamAPI_CSharp
{
    public class gameList
    {
        private string gameAppID;
        private string gameName;

        public gameList(string strShortName, string strLongName)
        {
            this.gameAppID = strShortName;
            this.gameName = strLongName;
        }

        public string GameAppID
        {
            get
            {
                return gameAppID;
            }
        }

        public string GameName
        {
            get
            {
                return gameName;
            }
        }
    }
}