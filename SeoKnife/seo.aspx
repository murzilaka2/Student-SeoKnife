<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="seo.aspx.cs" Inherits="SeoKnife.seo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seo Knife</title>
    <meta charset="utf-8" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="viewport" content="width=device-width, height=device-height, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <link rel="icon" href="images/favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Open+Sans:400,700,900" />
    <link rel="stylesheet" href="css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page text-center">

            <header style="position: absolute" class="page-head">
            </header>

            <main class="page-content" />
            <section class="context-dark offset-top-20 section-absolute veil reveal-md-block">
                <div class="shell-wide">
                    <div class="range range-xs-middle">
                        <div class="cell-sm-6 text-sm-left"><a href="index.aspx">
                            <img src="images/seoknife.png" width="183" height="32" alt="Seo Knife" class="img-responsive reveal-inline-block" oncontextmenu="return false;" /></a></div>
                    </div>
                </div>
            </section>
            <div id="home" class="section-navigation">
                <section class="bg-macaroni context-dark">
                    <div class="shell">
                        <div class="range">
                            <div class="range section-cover range-xs-center range-xs-middle range-md-left">
                                <div class="cell-lg-5 text-lg-left section-80 section-relative">
                                    <h1 class="text-bold">Automate <span class="text-uppercase">Seo</span> <span class="small">processes!</span></h1>
                                    <div class="offset-top-34">
                                        <p class="big">Seo Knife позволяет совершать полный seo аудит вашего сайта. Автоматизируйте все рутинные процессы благодаря Seo Knife.</p>
                                    </div>
                                    <div><a href="index.aspx">
                                        <img src="images/knife.png" width="180" height="60" alt="Seo Knife" class="img-responsive" oncontextmenu="return false;" /></a></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <img src="images/headimage.png" width="634" height="642" alt="Seo Knife" class="img-responsive img-absolute" oncontextmenu="return false;" />
                </section>
            </div>
            <div id="features" class="section-navigation">

                <section class="section-80 section-md-110 section-md-bottom-135 bg-white">
                    <div class="shell">
                        <h2 class="text-bold">Yandex позиции</h2>

                        <label>Введите адрес сайта :</label>
                        <asp:TextBox ID="SiteUrl" runat="server" CssClass="LineText"></asp:TextBox><br />
                        <br />
                        <label>Введите ключевое слово :</label>
                        <asp:TextBox ID="YandexKeyWord" runat="server" CssClass="LineText"></asp:TextBox><br />
                        <br />
                        <label>Глубина анализа :</label>
                        <asp:TextBox ID="ViewDepth" runat="server" Width="30px" CssClass="LineText" Text="5" Enabled="false"></asp:TextBox><label>&nbsp;страниц.</label><br />
                        <br />
                        <label>Введите Yandex регион :</label>
                        <asp:DropDownList ID="YandexRegion" runat="server" CssClass="LineText"></asp:DropDownList><br />
                        <br />
                        <asp:Button ID="YandexKeyFind" runat="server" Text="Определить" OnClick="YandexKeyFind_Click" CssClass="AnalizButton" /><br />
                        <br />
                        <asp:Label ID="YandexKeyPosition" runat="server"></asp:Label>

                    </div>
                </section>
            </div>

            <footer id="contacts" class="section-80 section-bottom-30 page-footer bg-gray-base context-dark section-navigation">
                <div class="shell section-relative">
                    <div class="range range-sm-center text-lg-left">
                        <div class="cell-sm-8 cell-md-12">
                            <div class="range range-xs-center">
                                <div class="cell-xs-7 text-xs-left cell-md-4 cell-lg-4 cell-lg-push-2">
                                    <p class="text-uppercase text-spacing-60 text-ubold">Страницы сайта</p>
                                    <p><a href="index.aspx">Главная</a></p>
                                    <p><a href="siteanaliz.aspx">Анализ Сайта</a></p>
                                    <p><a href="seo.aspx">Анализ Позиций</a></p>
                                </div>
                                <div class="cell-xs-12 offset-top-40 cell-md-5 offset-md-top-0 text-md-left cell-lg-4 cell-lg-push-3">
                                    <p class="text-uppercase text-spacing-60 text-ubold">Информация</p>
                                    <p><span class="small">Текущая версия программы - 1.0.<br />
                                        Если возникли технические неполадки, просьба обратиться в <a href="#">поддержку</a>.</span></p>
                                    <div class="offset-top-30">
                                    </div>
                                </div>
                                <div class="cell-xs-12 offset-top-66 cell-lg-4 cell-lg-push-1 offset-lg-top-0">
                                    <div class="footer-brand"><a href="index.aspx">
                                        <img src="images/logo-footer.png" width="184" height="56" alt="Seo Knife" class="img-responsive reveal-inline-block" oncontextmenu="return false;" /></a></div>
                                    <div class="offset-top-30">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="shell offset-top-70">
                    <p class="small text-darker">Seo Knife &copy; <span id="copyright-year"></span>. <a href="#">Privacy Policy</a></p>
                </div>
            </footer>
        </div>
        <div id="form-output-global" class="snackbars"></div>
        <script src="js/core.min.js"></script>
        <script src="js/script.js"></script>
    </form>
</body>
</html>
