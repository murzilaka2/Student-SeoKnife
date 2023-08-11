using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace SeoKnife
{
    //Ошибки
    //На сайт http://blou.ru, бьет ошибку - Слишком много попыток автоматического перенаправления.

    public class SiteAnaliz
    {
        public static string Url { get; set; }
        public static bool IsSiteOkey { get; set; }
        private bool GoMore { get; set; }
        private static string Page { get; set; }
        private static bool flag;

        //Базовая информация
        public int H1 { get; set; }
        public int H2 { get; set; }
        public int H3 { get; set; }
        public int H4 { get; set; }
        public int H5 { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string Author { get; set; }
        public string Charset { get; set; }
        public string Robots { get; set; }
        public string RobotsOutput { get; set; }
        public string SiteMap { get; set; }
        public string SiteMapOutput { get; set; }
        public string Favicon { get; set; }
        public string FaviconURL { get; set; }
        public string Encoding { get; set; }
        public string PageSize { get; set; }
        public string WordsSum { get; set; }
        public string Page404 { get; set; }
        public int ImagesCount { get; set; }
        public int ImagesCountAlt { get; set; }
        public ArrayList ImagesAlt { get; set; }
        public int ImageAlt { get; set; }
        public int HtmlErros { get; set; }
        public int HtmlWarning { get; set; }
        public ArrayList HtmlErrosList { get; set; }
        public string Language { get; set; }
        public ArrayList AnalyticsSystems { get; set; }
        public string LinksFromFindSystems { get; set; }
        public string Info { get; set; }
        public string JavaScript { get; set; }

        public ArrayList Links { get; set; }
        public int LinksNum { get; set; }
        public ArrayList ExternalLinks { get; set; }
        public int ExternalLinksNum { get; set; }


        public string SiteScreenshot { get; set; }

        //Серверная информация
        public string Ip { get; set; }
        public string Host { get; set; }
        public string Whois { get; set; }
        public string Ssl { get; set; }

        //Поисковые системы
        public string AlexaRank { get; set; }
        public int Tiz { get; set; }
        public string TizPic { get; set; }
        public string YandexCatalog { get; set; }
        public string Ags { get; set; }
        public string YandexPages { get; set; }
        public string GooglePages { get; set; }
        public string GooglePagesLink { get; set; }
        public string SiteVisits { get; set; }

        //Мобильная информация
        public string ViewPort { get; set; }

        //Работа с Каптчей
        public string CaptchaUrl { get; set; }

        //Запуск анализа
        private void Analiz()
        {
            if (IsSiteOkey == true)
            {
                BasicInfo();
                GetAllLinks();
                GetEncoding();
                GetPageSize();
                ServerInfo();
                FindSystems();
                IndexedPages();
                HtmlErrors();
                Mobile();
            }
            else { }
        }

        //Определение количества проиндексированных страниц
        private void IndexedPages()
        {
            YandexPages = "https://yandex.ua/search/?text=host%3A" + HomePage().Substring(7) + "&lr=141";
            GooglePagesLink = "https://www.google.ru/?qws_rd=ssl#newwindow=1&q=site:" + HomePage().Substring(7);
        }
        //Парсинг капчи
        private void GetCapctha(string url)
        {
            try
            {
                string data = GetPageText(url, false);
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(data);
                HtmlNodeCollection Read;
                //Получаем ссылку на картинку капчи          
                Read = htmlDoc.DocumentNode.SelectNodes("//img[@class='image form__captcha']");
                foreach (var node in Read) { CaptchaUrl = node.GetAttributeValue("src", null); }
                //Загружаем нашу капчу по ссылке 
                CaptchaReturn();
            }
            catch  { }

        }
        public string CaptchaReturn()
        {
            return CaptchaUrl;
        }

        //Получение кодировки
        private void GetEncoding()
        {
            string data = Page;
            HtmlDocument htmlDoc = new HtmlDocument();
            HtmlNodeCollection Read;
            htmlDoc.LoadHtml(data);
            try
            {
                Read = htmlDoc.DocumentNode.SelectNodes("//meta[@charset]");
                foreach (var node in Read) { Encoding = node.Attributes["charset"].Value; }
            }
            catch 
            {
                try
                {
                    Read = htmlDoc.DocumentNode.SelectNodes("//meta");
                    foreach (var node in Read) { if (node.Attributes["content"].Value.Contains("charset")) { Encoding = node.Attributes["content"].Value; Encoding = Encoding.Substring(19); } }
                }
                catch { }
            }
        }
        //Ссылки на сайте
        private ArrayList GetAllLinks()
        {
            ExternalLinks = new ArrayList();
            Links = new ArrayList();
            try
            {
                string SiteCode = Page;
                Links.Clear();
                ExternalLinks.Clear();
                Match m;
                string HRefPattern = @"(?i)<\s*?a\s+[\S\s\x22\x27\x3d]*?href=[\x22\x27]?([^\s\x22\x27<>]+)[\x22\x27]?.*?>";
                m = Regex.Match(SiteCode, HRefPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                while (m.Success)
                {
                    Links.Add(m.Groups[1].Value);
                    if (m.Groups[1].Value.Contains("http") == true && m.Groups[1].Value.Contains(HomePage().Substring(7)) == false)
                    {
                        ExternalLinksNum++;
                        ExternalLinks.Add(m.Groups[1].Value);
                    }
                    m = m.NextMatch();
                    LinksNum++;
                }
            }
            catch  { }
            return Links;
        }
        //Базовая информация сайта
        private void BasicInfo()
        {
            string data = GetPageText(Url, false);
            Page = data;
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(data);
            HtmlNodeCollection Read;
            WebClient Client = new WebClient();
            //Title
            try
            {
                Title = "Нет";
                Read = htmlDoc.DocumentNode.SelectNodes("//title");
                foreach (var node in Read) { Title = node.InnerText; GoMore = false; break; }
                //OpenGraph Title
                if (GoMore == true)
                {
                    Read = htmlDoc.DocumentNode.SelectNodes("//meta[@property='og:title']");
                    foreach (var node in Read) { Title = node.InnerText; if (Title.Length < 1) { Title = "Нет"; } }
                }
            }
            catch  { }

            //Description
            try
            {
                HtmlNode mdnode = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name='description']");
                HtmlAttribute desc;
                Description = "Нет";
                if (mdnode != null)
                {
                    desc = mdnode.Attributes["content"];
                    Description = desc.Value;
                    if (Description.Length < 1) { Description = "Нет"; }
                }
                else
                {
                    //OpenGraph Description
                    mdnode = htmlDoc.DocumentNode.SelectSingleNode("//meta[@property='og:description']");
                    if (mdnode != null)
                    {
                        desc = mdnode.Attributes["content"];
                        Description = desc.Value;
                        if (Description.Length < 1) { Description = "Нет"; }
                    }
                }
            }
            catch  { }
            //Keywords
            try
            {
                HtmlNode mdnode = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name='keywords']");
                Keywords = "Нет";
                if (mdnode != null)
                {
                    HtmlAttribute desc;
                    desc = mdnode.Attributes["content"];
                    Keywords = desc.Value;
                    if (Keywords.Length < 1) { Keywords = "Нет"; }
                }
            }
            catch  { }
            //Author
            try
            {
                HtmlNode mdnode = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name='author']");
                if (mdnode != null)
                {
                    HtmlAttribute desc;
                    desc = mdnode.Attributes["content"];
                    Author = desc.Value;
                    if (Author.Length < 1) { Author = "Нет"; }
                }
            }
            catch  { }

            //Favicon
            bool IconCheck = false;
            try
            {
                Read = htmlDoc.DocumentNode.SelectNodes("//link[@rel='icon']");
                foreach (var node in Read)
                {
                    Favicon = "Да";
                    if (node.GetAttributeValue("href", null).Contains("http://") || (node.GetAttributeValue("href", null).Contains("https://")))
                    {
                        FaviconURL = node.GetAttributeValue("href", null);
                    }
                    else
                    {
                        FaviconURL = HomePage() + "/" + node.GetAttributeValue("href", null);
                        GetPageText(FaviconURL, false);
                        if (IsSiteOkey == false) { FaviconURL = null; Favicon = "Есть, но не доступна по ссылке."; }
                    }
                    IconCheck = true;
                };
            }
            catch { Favicon = "Нет"; }
            if (IconCheck == false)
            {
                try
                {
                    Read = htmlDoc.DocumentNode.SelectNodes("//link[@rel='shortcut icon']");
                    foreach (var node in Read) { Favicon = "Да"; FaviconURL = node.GetAttributeValue("href", null); };
                }
                catch { Favicon = "Нет"; }
            }
            if (FaviconURL != null)
            {
                if (FaviconURL.Contains("http") == false)
                {
                    FaviconURL = HomePage() + "/" + FaviconURL;
                }
            }
            //Robots.txt
            try
            {
                HttpWebRequest RobotsRequest = (HttpWebRequest)WebRequest.Create(new Uri(HomePage() + "/robots.txt"));
                RobotsRequest.Method = "GET";
                HttpWebResponse RobotsResponse = (HttpWebResponse)RobotsRequest.GetResponse();
                RobotsOutput = new StreamReader(RobotsResponse.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8")).ReadToEnd();
                Robots = "Да";
            }
            catch  { Robots = "Нет"; }
            //SiteMap.xml
            try
            {
                string SiteMapRequest = GetPageText(HomePage() + "/sitemap.xml", false);
                HtmlDocument SiteMapDoc = new HtmlDocument();
                SiteMapDoc.LoadHtml(SiteMapRequest);
                HtmlNodeCollection SiteMapRead = SiteMapDoc.DocumentNode.SelectNodes("//loc");
                foreach (var node in SiteMapRead)
                {
                    if (node.InnerHtml != null) { SiteMapOutput += node.InnerHtml + "\n"; }
                }
                SiteMap = "Да";
            }
            catch  { SiteMap = "Нет"; }
            //Headers
            try
            {
                Read = htmlDoc.DocumentNode.SelectNodes("//h1");
                foreach (var node in Read) { H1++; }
            }
            catch  { }
            try
            {
                Read = htmlDoc.DocumentNode.SelectNodes("//h2");
                foreach (var node in Read) { H2++; }
            }
            catch  { }
            try
            {
                Read = htmlDoc.DocumentNode.SelectNodes("//h3");
                foreach (var node in Read) { H3++; }
            }
            catch  { }
            try
            {
                Read = htmlDoc.DocumentNode.SelectNodes("//h4");
                foreach (var node in Read) { H4++; }
            }
            catch  { }
            try
            {
                Read = htmlDoc.DocumentNode.SelectNodes("//h5");
                foreach (var node in Read) { H5++; }
            }
            catch  { }
            //404 Page
            try
            {
                string page404 = HomePage() + "/fsdgfshdfsdbcsdjkfcy1456156100ewfcjksdafjkhgdsakfk00001111222333";
                using (Stream data2 = Client.OpenRead(page404))
                {
                    using (StreamReader reader = new StreamReader(data2)) { Page404 = "Код 404 не получен"; }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Удаленный сервер возвратил ошибку: (404) Не найден.")
                {
                    Page404 = "Получен код 404";
                }
                else
                {
                    Page404 = "Код 404 не получен";
                }
            }
            //Количество изображений
            try
            {
                ImagesAlt = new ArrayList();
                string imgatl = "";
                Read = htmlDoc.DocumentNode.SelectNodes("//img");
                foreach (var node in Read) { ImagesCount++; };
                foreach (var node in Read)
                {
                    imgatl = node.GetAttributeValue("alt", null);
                    if (imgatl != null)
                    {
                        if (imgatl.Length == 0)
                        {
                            imgatl = node.OuterHtml;
                            if (imgatl.Contains("alt")) { ImagesCountAlt++; ImagesAlt.Add(node.OuterHtml); }
                        }
                        else { }
                    }
                };
            }

            catch  { }
            Console.WriteLine(ImagesCount);
            Console.WriteLine(ImagesCountAlt);
            //Язык сайта
            try
            {
                Language = "Не установлен язык веб-сайта";
                Read = htmlDoc.DocumentNode.SelectNodes("//html");
                foreach (var node in Read) { if (node.GetAttributeValue("lang", null) != null) { Language = "Установлен язык - " + node.GetAttributeValue("lang", null); } }
            }
            catch  { }
            //Количество слов       
            try
            {
                string SiteWords = "";
                Read = htmlDoc.DocumentNode.SelectNodes("//html");
                foreach (var node in Read) { SiteWords = node.InnerText; }
                Regex regex = new Regex(@"<!--.*?-->", RegexOptions.ExplicitCapture);
                MatchCollection matches = regex.Matches(SiteWords);
                SiteWords = regex.Replace(SiteWords, "");
                SiteWords = SiteWords.Trim(new char[] { ',', '.' });
                string[] textArray = SiteWords.Split(new char[] { ' ' });
                textArray = textArray.Where(x => x != "").ToArray();
                textArray = textArray.Where(x => x != "\n").ToArray();
                WordsSum = textArray.Length.ToString();
            }
            catch  { }
            //Системы статистики
            AnalyticsSystems = new ArrayList();
            if (data.Contains("liveinternet.ru/click")) { AnalyticsSystems.Add("LiveInternet"); } else if (data.Contains("title='LiveInternet:")) { AnalyticsSystems.Add("LiveInternet"); }
            if (data.Contains("google-analytics.com")) { AnalyticsSystems.Add("Google Analytics"); }
            if (data.Contains("https://mc.yandex.ru/metrika/watch.js")) { AnalyticsSystems.Add("Yandex Metrica"); } else if (data.Contains("Ya.Metrika")) { AnalyticsSystems.Add("Yandex Metrica"); }
            //Скриншот сайта
            SiteScreenshot = "http://mini.s-shot.ru/1024x768/?" + HomePage().Substring(7);
            //Css и JavaScript
            string check = data;
            int pos = check.IndexOf("</head>") + "</head>".Length;
            if (pos >= "</head>".Length) { check = check.Replace(check.Substring(pos), ""); }
            if (check.Contains("<script")) { JavaScript = "В верхней части страницы найден JS"; }
            else
            {
                JavaScript = "В верхней части страницы не найден JS";
            }
        }
        //HTML ошибки
        private void HtmlErrors()
        {
            //Ошибки на сайте
            HtmlErrosList = new ArrayList();
            string data = GetPageText("https://validator.w3.org/nu/?doc=" + HomePage(), true);
            HtmlDocument htmlDoc = new HtmlDocument();
            HtmlNodeCollection Read;
            try { htmlDoc.LoadHtml(data); } catch  { HtmlErrosList.Add("Превышено количество обращений к сервису. Попробуйте через час."); }
            try
            {
                Read = htmlDoc.DocumentNode.SelectNodes("//li[@class='error']");
                foreach (var node in Read) { HtmlErros++; HtmlErrosList.Add(node.InnerText.ToString()); }
            }
            catch  { }
            try
            {
                Read = htmlDoc.DocumentNode.SelectNodes("//li[@class='info warning']");
                foreach (var node in Read) { HtmlWarning++; }
            }
            catch  { }
        }
        //Page Size
        private void GetPageSize()
        {
            int sz = Page.Length;
            int bytes = sz / 1000;
            PageSize = bytes.ToString();
        }
        //Server Info
        private void ServerInfo()
        {
            //IP и Хост
            try
            {
                Host = "Не определено";
                string url = HomePage();
                Ip = System.Net.Dns.GetHostEntry(url.Substring(7)).AddressList[0].ToString();
                Host = Dns.GetHostEntry(Ip).HostName;
            }
            catch  { }
            //Ssl сертификат

            //Whois
            string data = GetPageText("https://www.nic.ru/whois/?query=" + HomePage(), false);
            if (data != null)
            {
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(data);
                HtmlNodeCollection Read;
                try
                {
                    Read = htmlDoc.DocumentNode.SelectNodes("//div[@class='b-whois-info__info']");
                    foreach (var node in Read) { Whois = node.InnerText; break; }
                    if (Whois.Contains("&nbsp;"))
                    {
                        Whois = Whois.Replace("&nbsp;", "");
                    }
                    Whois = Whois.Remove(0, 226);
                    Whois = Whois.Remove(Whois.Length - 163, 163);
                }
                catch  { }

                //Ssl
                data = GetPageText(HomePage().Insert(4, "s"), false);
                try { htmlDoc.LoadHtml(data); Ssl = "Сайт доступен по HTTPS"; }
                catch  { Ssl = "Сайт не доступен по HTTPS"; }
            }

        }
        //Поисковые системы
        private void FindSystems()
        {
            string data = GetPageText("http://www.alexa.com/siteinfo/" + HomePage().Substring(7), false);
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(data);
            HtmlNodeCollection Read;
            //Alex Rank
            try
            {
                Read = htmlDoc.DocumentNode.SelectNodes("//strong[@class='metrics-data align-vmiddle']");
                foreach (var node in Read) { AlexaRank = node.InnerText.Substring(92); break; }
            }
            catch  { AlexaRank = "Ошибка"; }

            //Страниц в Google
            data = GetPageText("https://www.google.com.ua/search?q=site:" + HomePage(), true);
            htmlDoc.LoadHtml(data);
            try
            {
                Read = htmlDoc.DocumentNode.SelectNodes("//div[@id='resultStats']");
                foreach (var node in Read) { GooglePages = node.InnerText; }
                //int r = Convert.ToInt32(Regex.Replace(GooglePages, @"[^\d]+", ""));
                //GooglePages = r.ToString();

            }
            catch  { }

            //Посещаемость сайта
            try
            {
                SiteVisits = GetPageText("http://counter.yadro.ru/values?site=" + HomePage().Substring(7), false);
            }
            catch  { SiteVisits = null; }
            if (SiteVisits.Contains("LI_error") == false)
            {
                SiteVisits = SiteVisits.Replace("LI_site", "Сайт");
                SiteVisits = SiteVisits.Replace("LI_month_hit", "<br />Просмотров в месяц");
                SiteVisits = SiteVisits.Replace("LI_month_vis", "<br />Посетителей в месяц");
                SiteVisits = SiteVisits.Replace("LI_week_hit", "<br />Просмотров в неделю");
                SiteVisits = SiteVisits.Replace("LI_week_vis", "<br />Посетителей в неделю");
                SiteVisits = SiteVisits.Replace("LI_day_hit", "<br />Просмотров в день");
                SiteVisits = SiteVisits.Replace("LI_day_vis", "<br />Посетителей в месяц");
                SiteVisits = SiteVisits.Replace("LI_today_hit", "<br />Просмотров сегодня");
                SiteVisits = SiteVisits.Replace("LI_today_vis", "<br />Посетителей сегодня");
                SiteVisits = SiteVisits.Replace("LI_online_hit", "<br />Онлайн просмотров");
                SiteVisits = SiteVisits.Replace("LI_online_vis", "<br />Онлайн посетителей");
            }
            else { SiteVisits = null; }
            //Яндекс каталог           
            try
            {
                data = GetPageText("https://yandex.ua/yaca/?text=" + HomePage().Substring(7), false);
                htmlDoc.LoadHtml(data);
                Read = htmlDoc.DocumentNode.SelectNodes("//div[@class='info info_before-columns']");
                foreach (var node in Read) { YandexCatalog = node.InnerText; if (YandexCatalog == "Знайдено сайтів: 1") { YandexCatalog = "Да"; Ags = "Нет"; } else if (YandexCatalog == "Знайдено сайтів: 0") { YandexCatalog = "Нет"; } }
            }
            catch  { YandexCatalog = "Ошибка"; }
            //Тиц картинкой
            TizPic = "http://yandex.ru/cycounter?" + HomePage().Substring(7);
            //Тиц и Агс сайта
            Ags = "Нет";
            //string Uri = "https://yandex.ua/yaca/yca/cy/"+HomePage().Substring(7)+"?redircnt=1491460219.1&rdpass=1&ncrnd=6220";
            //try
            //{
            //    data = GetPageText(Uri);
            //    htmlDoc.LoadHtml(data);
            //    Read = htmlDoc.DocumentNode.SelectNodes("//div[@class='cy__not-described-cy']");
            //    foreach (var node in Read) { if (node.InnerText == "не определен") { Ags = "Обнаружен фильтр АГС"; } else { Ags = "Нет"; Tiz = Convert.ToInt32(node.InnerText.Remove(0, 32)); } }
            //}
            //catch {}

            //Ссылаются на сайт
            LinksFromFindSystems = "Яндекс : <span class=\"IndexPages\"><a href=\"https://yandex.ua/yandsearch?text=" + HomePage().Substring(7) + "\" target=\"_blank\">Посмотреть</a></span><br />";
            LinksFromFindSystems += "Google : <span class=\"IndexPages\"><a href=\"https://www.google.com.ua/search?hl=en&lr=&ie=UTF-8&q=link:" + HomePage().Substring(7) + "\" target=\"_blank\">Посмотреть</a></span><br />";
            LinksFromFindSystems += "Яндекс Блоги : <span class=\"IndexPages\"><a href=\"https://yandex.ru/blogs/rss/search?text=" + HomePage().Substring(7) + "\" target=\"_blank\">Посмотреть</a></span><br />";
            LinksFromFindSystems += "Google Картинки : <span class=\"IndexPages\"><a href=\"https://www.google.ru/search?hl=ru&source=imghp&q=" + HomePage().Substring(7) + "&gbv=2&tbm=isch&gws_rd=ssl\" target=\"_blank\">Посмотреть</a></span><br />";
            LinksFromFindSystems += "Яндекс Картинки : <span class=\"IndexPages\"><a href=\"https://yandex.ua/images/search?text=" + HomePage().Substring(7) + "&stype=image target =\"_blank\">Посмотреть</a></span><br />";
            //Дополнительная информация
            Info = "Google Кэш : <span class=\"IndexPages\"><a href=\"http://webcache.googleusercontent.com/search?q=cache:" + HomePage().Substring(7) + "\" target=\"_blank\">Посмотреть</a></span><br />";
            Info += "История сайта : <span class=\"IndexPages\"><a href=\"http://web.archive.org/web/*/" + HomePage().Substring(7) + "\" target=\"_blank\">Посмотреть</a></span><br />";
        }
        //Мобильные характеристики
        private void Mobile()
        {
            string data = Page;
            HtmlDocument htmlDoc = new HtmlDocument();
            HtmlNodeCollection Read;
            htmlDoc.LoadHtml(data);
            try
            {
                Read = htmlDoc.DocumentNode.SelectNodes("//meta[@name='viewport']");
                foreach (var node in Read) { ViewPort = "Сайт правильно отображается на всех устройствах"; }
            }
            catch 
            {
                ViewPort = "На ваших страницах не укзаан тег Viewport";
            }
            Console.WriteLine(ViewPort);
        }
        //Очистка Url
        public void CleanUrl(string url)
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
            GetPageText(Url, false);
            Analiz();
        }
        //Очистка до верхнего уровня
        private string HomePage()
        {
            string cleanurl = Url.Substring(7);
            if (cleanurl.Contains("/"))
            {
                string trimmed = cleanurl.Trim();
                cleanurl = "http://" + trimmed.Substring(0, trimmed.IndexOf('/'));
            }
            else
            {
                return "http://" + cleanurl;
            }
            return cleanurl;
        }

        //Получение страницы HTML
        private static string GetPageText(string url, bool flag)
        {
            try
            {
                WebClient client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0; Acoo Browser 1.98.744; .NET CLR 3.5.30729)");
                if (flag == false)
                {
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    var response = (HttpWebResponse)request.GetResponse();

                    using (Stream data = client.OpenRead(url))
                    {
                        if (response.CharacterSet == "windows-1251")
                        {
                            using (StreamReader reader = new StreamReader(data, encoding: System.Text.Encoding.GetEncoding("windows-1251")))
                            {
                                IsSiteOkey = true;
                                return reader.ReadToEnd();
                            }
                        }
                        else
                        {
                            using (StreamReader reader = new StreamReader(data, encoding: System.Text.Encoding.UTF8))
                            {
                                IsSiteOkey = true;
                                return reader.ReadToEnd();
                            }
                        }
                    }
                }
                else
                {
                    using (Stream data = client.OpenRead(url))
                    {
                        using (StreamReader reader = new StreamReader(data, encoding: System.Text.Encoding.UTF8))
                        {
                            IsSiteOkey = true;
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch  { IsSiteOkey = false; return "Error"; }
        }
    }
}