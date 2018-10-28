﻿using System;
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
        public static async Task<string> GetPageAsync(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.CreateHttp(url);
            HttpWebResponse response;
            try
            {
                // The 3 following line is needed to bypass some check against botting on website (Error 403 if these line don't exist)
                request.Method = "GET";
                request.Accept = "text/html";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/6.0;)";
                response = (HttpWebResponse)await request.GetResponseAsync();
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

        public static async Task<List<List<string>>> getTableFromPagesAsync(string url, string xPath = "//table")
        {
            string sourcePage = await GetPageAsync(url);
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