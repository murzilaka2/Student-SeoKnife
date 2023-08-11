using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace SeoKnife
{
    public class KeyWords
    {
        public string Text { get; set; }
        public int Position { get; set; }
        public int Page { get; set; }

        public int GetKeys(string KeyWord, string SiteUrl, int ViewDepth, string Region)
        {
            Position = 1;
            bool fo = false;
            for (int i = 0; i < ViewDepth; i++)
            {
                    string data = GetPageText("https://yandex.ua/search/xml?user=enykoruna1&key=03.118078436:e56aef625d3b6b57515577c3a77887a0&query=" + KeyWord + "&page=" + Page);
                    if (data!=null)
                    {
                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(data);
                    HtmlNodeCollection Read = htmlDoc.DocumentNode.SelectNodes("//url");
                    foreach (var node in Read) { Text = node.InnerHtml; if (Text.Contains(SiteUrl)) { fo = true; break; } else { Position++; } }
                    if (fo == true) { break; }
                    else
                    {
                        Page++;
                    }
                }else
                {
                    return 0;
                }
            }
            return Position;
        }
        private static string GetPageText(string url)
        {
            try
            {
                WebClient client = new WebClient();
                using (Stream data = client.OpenRead(url))
                {
                    using (StreamReader reader = new StreamReader(data))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch { return null; }
        }    
    }
}