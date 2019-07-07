using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreamAPI_CSharp
{
    public class WebGestion
    {
        public static string GetPage(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.CreateHttp(url);
            HttpWebResponse response;
            try
            {
                // The 3 following line is needed to bypass some check against botting on website (Error 403 if these line don't exist)
                request.Method = "GET";
                request.Accept = "text/html";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.20 Safari/537.36"; //CHANGE CA SI JAMAIS CA PLANTE SANS RAISON --"
                response = (HttpWebResponse)request.GetResponse();
                string sourcePage;

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return sourcePage = reader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                HttpWebResponse n = (HttpWebResponse)e.Response;
                MessageBox.Show(e.ToString()); //ADD THIS STRING FOR DEBUGGING, TO SEE IF THERE IS AN EXCEPTION.
                if (n.StatusCode == HttpStatusCode.NotFound)
                {
                    return "";
                }
            }
            return "";
        }

        public static List<List<string>> GetTableFromPages(string url, string xPath = "//table")
        {
            string sourcePage = GetPage(url);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(sourcePage);
            List<List<string>> table = null;
            try
            {
                table = doc.DocumentNode.SelectNodes(xPath)
                   .Descendants("tr")
                   .Skip(1)
                   .Where(tr => tr.Elements("td").Count() > 1)
                   .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                   .ToList();
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}