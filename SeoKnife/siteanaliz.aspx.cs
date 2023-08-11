using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeoKnife
{
    public partial class siteanaliz : System.Web.UI.Page
    {
        SiteAnaliz Analize = new SiteAnaliz();
        private int Valuation { get; set; }
        private void Visibility(bool value)
        {
            Subject.Visible = value;
            Visitors.Visible = value;
            Watches.Visible = value;
            Tiz.Visible = value;
            Get_Cost.Visible = value;
            CheckBoxGoole.Visible = value;
            CheckBoxYandex.Visible = value;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Если зашли с смартфона, то скрываем некоторые вещи.
            IsMobileCheck();
            PriceButton.Visible = false;
            //Оценка стоимости сайта
            Cost_Block.CssClass = "myBlock";
            Visibility(false);

        }
        private void IsMobileCheck()
        {
            if (Request.Browser.IsMobileDevice)
            {
                MobileContent.Visible = false;
            }
            else
            {
                MobileContent.Visible = true;
            }
        }
        public void Analiz()
        {
            Analize.CleanUrl(SiteUrlText.Text);
        }       
        protected void SiteUrlButton_Click(object sender, EventArgs e)
        {
            Analiz();
            if (SiteAnaliz.IsSiteOkey == true) { AddElements(); }
            else { ErrorM(); }
        }
        private void AddElements()
        {
            string BadValue = "<span style=\"color:red; float:right;\">&#10006;</span>&nbsp;";
            string GoodValue = "<span style=\"color:green; float:right;\">&#10004;</span>&nbsp;";
            string WriteValue = "";
            //Заголовочник
            HeadBlock.Visible = true;
            //Адрес сайта
            if (Analize.Title == "Нет") { WriteValue = BadValue; } else { WriteValue = GoodValue; }
            if (Analize.FaviconURL != null)
            {
                HeaderContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\"><img src=\"" + Analize.FaviconURL + "\" alt=\"favicon\" width=\"15px\">&nbsp;" + Analize.Title + "</span></div> <div class=\"divinfocontent\"><span> <img src=\"" + Analize.SiteScreenshot + "\" alt=\"Скриншот Сайта\" width=\"40%\"></span></div>" });
            }
            else { HeaderContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">" + Analize.Title + "</span></div> <div class=\"divinfocontent\"><span> <img src=\"" + Analize.SiteScreenshot + "\" alt=\"Скриншот Сайта\" width=\"40%\"></span></div>" }); }

            //Базовая Информация Заголовок
            BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divbigheader\">Базовая Информация</div>" });
            //Языка сайта
            if (Analize.Language == "Не установлен язык веб-сайта") { WriteValue = BadValue; } else { WriteValue = GoodValue; Valuation += 5; }
            BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Язык</span></div><div class=\"divinfocontent\"><span>" + Analize.Language + "</span>" + WriteValue + "</div>" });
            //Заголовок
            if (Analize.Title == "Нет") { WriteValue = BadValue; } else { WriteValue = GoodValue; Valuation += 5; }
            BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Заголовок</span></div><div class=\"divinfocontent\"><span>" + Analize.Title + "</span>" + WriteValue + "</div>" });
            //Описание
            if (Analize.Description == "Нет") { WriteValue = BadValue; } else { WriteValue = GoodValue; Valuation += 5; }
            BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Описание</span></div><div class=\"divinfocontent\"><span>" + Analize.Description + "</span>" + WriteValue + "</div>" });
            //Ключевые слова
            if (Analize.Keywords == "Нет") { WriteValue = BadValue; } else { WriteValue = GoodValue; Valuation += 5; }
            BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Ключевые слова</span></div><div class=\"divinfocontent\"><span>" + Analize.Keywords + "</span>" + WriteValue + "</div>" });
            //Автор
            if (Analize.Author != null)
            {
                Valuation += 5;
                BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Автор</span></div><div class=\"divinfocontent\"><span>" + Analize.Author + "</span>" + GoodValue + "</div>" });
            }
            //Кодировка
            if (Analize.Encoding != null)
            {
                Valuation += 5;
                BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Кодировка</span></div><div class=\"divinfocontent\"><span>" + Analize.Encoding + "</span>" + GoodValue + "</div>" });
            }
            //JavaScript
            if (Analize.JavaScript != null)
            {
                if (Analize.JavaScript == "В верхней части страницы не найден JS") { WriteValue = GoodValue; Valuation += 5; }
                else { WriteValue = BadValue; }
                BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">JavaScript</span></div><div class=\"divinfocontent\"><span>" + Analize.JavaScript + "</span>" + WriteValue + "</div>" });
            }
            //Robots.txt
            if (Analize.Robots == "Нет") { WriteValue = BadValue; } else { WriteValue = GoodValue; Valuation += 5; }
            BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Robots.txt</span></div><div class=\"DivInfoContentRobots\"><span>" + Analize.Robots + "</span>" + WriteValue + "</div>" });
            if (Analize.RobotsOutput != null)
            {
                TextBox box = new TextBox();
                box.TextMode = TextBoxMode.MultiLine;
                box.ID = "HtmlErrorsText";
                box.CssClass = "AspTextBox";
                box.Text = Analize.RobotsOutput;
                BasicContent.Controls.Add(new Literal() { Text = "<div class=\"erroblock3\">" });
                BasicContent.Controls.Add(box);
                BasicContent.Controls.Add(new Literal() { Text = "</div>" });
            }
            //SiteMap
            if (Analize.SiteMap == "Нет") { WriteValue = BadValue; } else { WriteValue = GoodValue; Valuation += 5; }
            BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">XML Карта</span></div><div class=\"DivInfoContentSiteMap\"><span>" + Analize.SiteMap + "</span>" + WriteValue + "</div>" });
            if (Analize.SiteMapOutput != null)
            {
                TextBox box = new TextBox();
                box.TextMode = TextBoxMode.MultiLine;
                box.ID = "HtmlErrorsText";
                box.CssClass = "AspTextBox";
                box.Text = Analize.SiteMapOutput;
                foreach (var item in Analize.HtmlErrosList)
                {
                    box.Text += item.ToString() + "\n\n";
                }
                BasicContent.Controls.Add(new Literal() { Text = "<div class=\"erroblock2\">" });
                BasicContent.Controls.Add(box);
                BasicContent.Controls.Add(new Literal() { Text = "</div>" });
            }
            //Favicon
            if (Analize.Favicon == "Нет") { WriteValue = BadValue; } else { WriteValue = GoodValue; Valuation += 5; }
            BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Favicon</span></div><div class=\"divinfocontent\"><span>" + Analize.Favicon + "</span>" + WriteValue + "</div>" });
            //Размер страницы
            if (Convert.ToInt32(Analize.PageSize) > 300) { WriteValue = BadValue; } else { WriteValue = GoodValue; Valuation += 5; }
            BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Размер страницы</span></div><div class=\"divinfocontent\"><span>" + Analize.PageSize + " кб</span>" + WriteValue + "</div>" });
            //Заголовки
            if (Analize.H1 == 0) { WriteValue = BadValue; } else { WriteValue = GoodValue; Valuation += 5; }
            BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Заголовки</span></div><div class=\"divinfocontent\"><span>" + "H1 - " + Analize.H1 + "," + " H2 - " + Analize.H2 + "," + " H3 - " + Analize.H3 + "," + " H4 - " + Analize.H4 + "," + " H5 - " + Analize.H5 + "</span>" + WriteValue + "</div>" });
            //Количество слов
            if (Convert.ToInt32(Analize.WordsSum) < 300) { WriteValue = BadValue; } else { WriteValue = GoodValue; Valuation += 5; }
            BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Количество слов</span></div><div class=\"divinfocontent\"><span>" + Analize.WordsSum + "</span>" + WriteValue + "</div>" });
            //Количество изображений
            if (Analize.ImagesCount < 1) { WriteValue = BadValue; } else { WriteValue = GoodValue; Valuation += 5; }
            BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Количество изображений</span></div><div class=\"divinfocontent\"><span>" + Analize.ImagesCount.ToString() + "</span>" + WriteValue + "</div>" });
            //Изображений без alt
            if (Analize.ImagesCountAlt >= 1) { WriteValue = BadValue; } else { WriteValue = GoodValue; Valuation += 5; }
            BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Количество изображений без атрибута ALT</span></div><div class=\"DivInfoContentImageAlt\"><span>" + Analize.ImagesCountAlt.ToString() + "</span>" + WriteValue + "</div>" });
            if (Analize.ImagesCountAlt >= 1)
            {
                TextBox box = new TextBox();
                box.TextMode = TextBoxMode.MultiLine;
                box.ID = "HtmlErrorsText";
                box.CssClass = "AspTextBox";
                foreach (var item in Analize.ImagesAlt)
                {
                    box.Text += item.ToString() + "\n\n";
                }
                BasicContent.Controls.Add(new Literal() { Text = "<div class=\"erroblock6\">" });
                BasicContent.Controls.Add(box);
                BasicContent.Controls.Add(new Literal() { Text = "</div>" });
            }
            //Ошибки кода и Предупреждения
            if (Analize.HtmlErros >= 1) { WriteValue = BadValue; } else { WriteValue = GoodValue; Valuation += 5; }
            BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Ошибки HTML кода</span></div><div class=\"DivInfoContentHtmlErrors\"><span>" + "Найдено " + Analize.HtmlErros.ToString() + " ошибок и " + Analize.HtmlWarning + " предупреждений" + "</span>" + WriteValue + "</div>" });
            if (Analize.HtmlErrosList.Count != 0)
            {
                TextBox box = new TextBox();
                box.TextMode = TextBoxMode.MultiLine;
                box.ID = "HtmlErrorsText";
                box.CssClass = "AspTextBox";
                foreach (var item in Analize.HtmlErrosList)
                {
                    box.Text += item.ToString() + "\n\n";
                }
                BasicContent.Controls.Add(new Literal() { Text = "<div class=\"erroblock\">" });
                BasicContent.Controls.Add(box);
                BasicContent.Controls.Add(new Literal() { Text = "</div>" });
            }
            //Ссылки
            if (Analize.Links.Count > 100) { WriteValue = BadValue; } else { WriteValue = GoodValue; Valuation += 5; }
            BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Ссылок на сайте</span></div><div class=\"DivInfoContentLinks\"><span>" + Analize.LinksNum + "</span>" + WriteValue + "</div>" });
            if (Analize.Links.Count != 0)
            {
                TextBox box = new TextBox();
                box.TextMode = TextBoxMode.MultiLine;
                box.ID = "HtmlErrorsText";
                box.CssClass = "AspTextBox";
                foreach (var item in Analize.Links)
                {
                    box.Text += item.ToString() + "\n";
                }
                BasicContent.Controls.Add(new Literal() { Text = "<div class=\"erroblock4\">" });
                BasicContent.Controls.Add(box);
                BasicContent.Controls.Add(new Literal() { Text = "</div>" });
            }
            //Внешние ссылки
            if (Analize.ExternalLinks.Count > 10) { WriteValue = BadValue; } else { WriteValue = GoodValue; Valuation += 5; }
            BasicContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Внешних ссылок на сайте</span></div><div class=\"DivInfoContentExternalLinks\"><span>" + Analize.ExternalLinksNum + "</span>" + WriteValue + "</div>" });
            if (Analize.ExternalLinks.Count != 0)
            {
                TextBox box = new TextBox();
                box.TextMode = TextBoxMode.MultiLine;
                box.ID = "HtmlErrorsText";
                box.CssClass = "AspTextBox";
                foreach (var item in Analize.ExternalLinks)
                {
                    box.Text += item.ToString() + "\n";
                }
                BasicContent.Controls.Add(new Literal() { Text = "<div class=\"erroblock5\">" });
                BasicContent.Controls.Add(box);
                BasicContent.Controls.Add(new Literal() { Text = "</div>" });
            }
            //Серверная Информация Заголовок
            ServerContent.Controls.Add(new Literal() { Text = "<div class=\"divbigheader\">Серверная информация</div>" });
            //Ip адрес
            ServerContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Ip Адрес</span></div><div class=\"divinfocontent\"><span>" + Analize.Ip + "</span>" + GoodValue + "</div>" });
            //Hots
            if (Analize.Host == "Не определено") { WriteValue = BadValue; } else { WriteValue = GoodValue; Valuation += 5; }
            ServerContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Сервер</span></div><div class=\"divinfocontent\"><span>" + Analize.Host + "</span>" + WriteValue + "</div>" });
            //Page 404
            if (Analize.Page404 == "Код 404 не получен") { WriteValue = BadValue; } else { WriteValue = GoodValue; Valuation += 5; }
            ServerContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Код ответа страницы 404</span></div><div class=\"divinfocontent\"><span>" + Analize.Page404 + "</span>" + WriteValue + "</div>" });
            //Ssl
            if (Analize.Ssl == "Сайт доступен по HTTPS") { WriteValue = GoodValue; } else { WriteValue = BadValue; Valuation += 5; }
            ServerContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Ssl Сертификат</span></div><div class=\"divinfocontent\"><span>" + Analize.Ssl + "</span>" + WriteValue + "</div>" });
            //Whois
            if (Analize.Whois != null)
            {
                TextBox WhoisList = new TextBox();
                WhoisList.TextMode = TextBoxMode.MultiLine;
                WhoisList.CssClass = "AspTextBox2";
                WhoisList.Text = null;
                WhoisList.Text = Analize.Whois;
                ServerContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Whois</span></div><div class=\"divinfocontent\"><span>" });
                ServerContent.Controls.Add(WhoisList);
                ServerContent.Controls.Add(new Literal() { Text = "</span></div>" });
            }

            //Поисковые системы
            ServerContent.Controls.Add(new Literal() { Text = "<div class=\"divbigheader\">Поисковые системы</div>" });
            //Alexa Rank
            ServerContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Alexa Rank</span></div><div class=\"divinfocontent\"><span>" + Analize.AlexaRank + "</span></div>" });
            //Яндекс Тиц       
            ServerContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Яндекс Тиц</span></div><div class=\"divinfocontent\"><span><img src=" + Analize.TizPic + " alt=\"Яндекс Тиц\"></span></div>" });
            //Яндекс Каталог
            ServerContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Яндекс Каталог</span></div><div class=\"divinfocontent\"><span>" + Analize.YandexCatalog + "</span></div>" });
            //Яндекс АГС
            ServerContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Яндекс АГС</span></div><div class=\"divinfocontent\"><span>" + Analize.Ags + "</span></div>" });
            //Индексация Яндекс
            ServerContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Страниц в Яндекс</span></div><div class=\"divinfocontentIndexPages\"><span class=\"IndexPages\"><a href=" + Analize.YandexPages + " target=\"_blank\">Посмотреть</a></span></div>" });
            //Индексация Google
            ServerContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Страниц в Google</span></div><div class=\"divinfocontentIndexPages\"><span class=\"IndexPages\"><a href=" + Analize.GooglePagesLink + " target=\"_blank\">" + Analize.GooglePages + "</a></div>" });
            //Системы Аналитики
            string systems = "";
            foreach (var item in Analize.AnalyticsSystems)
            {
                systems += item + "<br />";
            }
            ServerContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Системы Аналитики</span></div><div class=\"divinfocontent\"><span>" + systems + "</span></div>" });
            //Посещаемость сайта
            if (Analize.SiteVisits != null)
            {
                ServerContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Посещаемость</span></div><div class=\"divinfocontent\"><span>" + Analize.SiteVisits + "</span></div>" });
            }
            //Ссылаются на сайт
            ServerContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Ссылаются на сайт</span></div><div class=\"divinfocontent\"><span>" + Analize.LinksFromFindSystems + "</span></div>" });
            //Дополнительная информация
            ServerContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Дополнительная информация</span></div><div class=\"divinfocontent\"><span>" + Analize.Info + "</span></div>" });
            //Мобильный вид
            if (Analize.ViewPort == "Сайт правильно отображается на всех устройствах")
            {
                WriteValue = GoodValue;
            }
            else
            {
                WriteValue = BadValue; Valuation += 5;
            }
            MobileContent.Controls.Add(new Literal() { Text = "<div class=\"divbigheader\"></div>" });
            MobileContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Мобильная версия</span></div><div class=\"divinfocontent\"><span><div class=\"col - sm - 8 content - test\"><span class=\"mobiledesc\"><span class=\"SiteSpanText\">Тег Viewport</span> - " + Analize.ViewPort + "&nbsp;" + WriteValue + "</span><div id = \"device\" class=\"iphone\"><div id = \"devicetop\" class=\"iphone-speaker\"></div><div id = \"screen\" class=\"iphone-screen\"><iframe src = \"" + SiteAnaliz.Url + "\" class=\"siteframe\"></iframe></div><a href = \"#\" ><div id=\"button\" onclick=\"ipadSwitch()\"><div id = \"button-square\" ></div></div></a></div></div></span></div>" });
            HeaderContent.Controls.Add(new Literal() { Text = "<div class=\"divinfoheader\"><span class=\"SiteSpanText\">Хотите узнать стоимость " + SiteUrlText.Text + " ?</span></div> <div class=\"divinfocontent\"><span>" });
            PriceButton.Visible = true;
            SitePrice.Controls.Add(new Literal() { Text = "&nbsp;</span><span class=\"Valuation\">&#9749; Оценка сайта - " + Valuation + "%</span></div>" });
        }
        private void ErrorM()
        {
            HeadBlock.Visible = false;
            SiteUrlText.Text = null;
            HeaderContent.Controls.Add(new Literal() { Text = "<style>input[type=\"text\"]::-webkit-input-placeholder {color: red !important;} input[type=\"text\"]:-moz-placeholder { color: red !important; } input[type=\"text\"]::-moz-placeholder { color: red !important; } input[type=\"text\"]:-ms-input-placeholder { color: red !important; }</style>" });
            SiteUrlText.Attributes.Add("placeholder","Введите коректный адрес сайта");
           
        }
        //Определяем стоимость сайта
        protected void ButCost_Click(object sender, EventArgs e)
        {
            SitePrice siteprice = new SitePrice();
            Subject.DataSource = siteprice.Subjects;
            Subject.DataBind();
            Cost_Block.CssClass = "myBlock2";
            Visibility(true);
        }
        //Считаем стоимость сайта
        protected void Get_Cost_Click(object sender, EventArgs e)
        {
            SitePrice s = new SitePrice();
            SitePrice.Controls.Add(new Literal()
            {
                Text = "Приблизительная стоимость сайта - " + Convert.ToString(s.SiteCost(Convert.ToInt32(Tiz.Text), Subject.SelectedValue, Convert.ToInt32(Visitors.Text), Convert.ToInt32(Watches.Text), CheckBoxGoole.Checked, CheckBoxYandex.Checked) + " рублей.")
            });
        }
    }
}