using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreamAPI_CSharp
{
    class updateManagement
    {
        public static void CreamApiVersionScraper(/*string[] userAuthentication*/)
        {
            var creamApiThreadURL = "https://cs.rin.ru/forum/viewtopic.php?p=1180852#p1180852";
            WebGestion.GetPage(creamApiThreadURL);
        }


    }
}
