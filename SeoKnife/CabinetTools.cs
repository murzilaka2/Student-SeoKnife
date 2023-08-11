using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace SeoKnife
{
    public class CabinetTools
    {
        public string Url { get; set; }

        private string CleanUrl(string url)
        {
            string CleanUrl = "";
            if (url.Contains("https://"))
            {
                CleanUrl = url.Substring(8);
                Url = "http://" + CleanUrl;
            }
            else if (url.Contains("http://") == false && url.Contains("https://") == false)
            {
                Url = "http://" + url;
            }
            else
            {
                Url = url;
            }
            return Url;
        }

        public void GetImages(string url)
        {
            url = CleanUrl(url);
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\ClientsImages";
            try
            {
                WebClient client = new WebClient();
                string data;
                using (Stream ImagesName = client.OpenRead(url))
                {
                    using (StreamReader reader = new StreamReader(ImagesName)) { data = reader.ReadToEnd(); }
                }
                Regex regex = new Regex(@"\<img.+?src=\""(?<imgsrc>.+?)\"".+?\>", RegexOptions.ExplicitCapture);
                MatchCollection matches = regex.Matches(data);
                var imagesUrl = matches.Cast<Match>().Select(m => m.Groups["imgsrc"].Value);
                Regex regexImg = new Regex(@"\/(?<name>[^\/.]+?\.\w+$)", RegexOptions.ExplicitCapture);
                foreach (var s in imagesUrl)
                {
                    Match match = regexImg.Match(s);
                    if (match.Success)
                    {
                        string name = match.Groups["name"].Value;
                        if (s.Contains("http://") || s.Contains("https://"))
                        {
                            client.DownloadFile(s, path + "\\" + name);
                        }
                        else
                        {
                            client.DownloadFile(url + "/" + s, path + "\\" + name);
                        }
                    }
                }
            }
            catch { }
        }
    }
}