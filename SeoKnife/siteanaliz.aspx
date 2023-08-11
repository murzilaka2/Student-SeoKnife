﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="siteanaliz.aspx.cs" Inherits="SeoKnife.siteanaliz" %>

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
    <%-- Скрипты для скрытых блоков --%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script type="text/javascript">$(document).ready(function () { $('.DivInfoContentHtmlErrors').click(function () { $(this).parent().children('div.erroblock').toggle('normal'); return false; }); });</script>
    <script type="text/javascript">$(document).ready(function () { $('.DivInfoContentSiteMap').click(function () { $(this).parent().children('div.erroblock2').toggle('normal'); return false; }); });</script>
    <script type="text/javascript">$(document).ready(function () { $('.DivInfoContentRobots').click(function () { $(this).parent().children('div.erroblock3').toggle('normal'); return false; }); });</script>
    <script type="text/javascript">$(document).ready(function () { $('.DivInfoContentLinks').click(function () { $(this).parent().children('div.erroblock4').toggle('normal'); return false; }); });</script>
    <script type="text/javascript">$(document).ready(function () { $('.DivInfoContentExternalLinks').click(function () { $(this).parent().children('div.erroblock5').toggle('normal'); return false; }); });</script>
    <script type="text/javascript">$(document).ready(function () { $('.DivInfoContentImageAlt').click(function () { $(this).parent().children('div.erroblock6').toggle('normal'); return false; }); });</script>

</head>
<body onload="location += '#top';">
    <form id="form1" runat="server">
        <%-- Для оценки сайта --%>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                                    <div><a href="index.aspx#">
                                        <img src="images/knife.png" width="180" height="60" alt="Seo Knife" class="img-responsive" oncontextmenu="return false;" /></a></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <img src="images/headimage.png" width="634" height="642" alt="Seo Knife" class="img-responsive img-absolute" oncontextmenu="return false;" />
                </section>
            </div>
            <div id="features" class="section-navigation">
                <section class="section-80 section-md-110 section-md-bottom-135 bg-white" style="color: #4A4A4A; background: url(data:;base64,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);" id="top">
                    <div class="shell">

                        <asp:Image ID="SiteFavicon" runat="server" Visible="false" />
                        <asp:TextBox ID="SiteUrlText" runat="server" ToolTip="Введите адрес сайта" CssClass="form-control" placeholder="Введите сайт, например: site.ru"></asp:TextBox>
                        <asp:Button ID="SiteUrlButton" runat="server" Text="Анализ" OnClick="SiteUrlButton_Click" CssClass="btn btn-success" />


                        <!-- Блок анализа информации-->
                        <div style="margin-top: 20px;">
                            <div style="overflow: hidden; margin-top: 60px;" id="HeadBlock" runat="server" visible="false">
                            </div>
                            <div class="leftinfo">
                                <asp:PlaceHolder ID="HeaderContent" runat="server"></asp:PlaceHolder>

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Button runat="server" CssClass="btn btn-success" Text="Да, хочу!" OnClick="ButCost_Click" ID="PriceButton" Visible="false" />
                                        <asp:PlaceHolder ID="SitePrice" runat="server"></asp:PlaceHolder>



                                        <asp:Panel runat="server" ID="Cost_Block" CssClass="myBlock">
                                            <asp:DropDownList runat="server" ID="Subject" Visible="false" CssClass="form-control"></asp:DropDownList><br />
                                            <br />
                                            <asp:TextBox runat="server" ID="Visitors" Visible="false" TextMode="Number" CssClass="form-control" placeholder="Посетителей в месяц"></asp:TextBox><br />
                                            <br />
                                            <asp:TextBox runat="server" ID="Watches" Visible="false" TextMode="Number" CssClass="form-control" placeholder="Просмотров в месяц"></asp:TextBox><br />
                                            <br />
                                            <asp:TextBox runat="server" ID="Tiz" Visible="false" TextMode="Number" CssClass="form-control" placeholder="Тиц"></asp:TextBox><br />
                                            <br />
                                            <asp:CheckBox runat="server" ID="CheckBoxGoole" Visible="false" Text="Сайт проиндексирован в Google" CssClass="form-control"/><br />
                                            <br />
                                            <asp:CheckBox runat="server" ID="CheckBoxYandex" Visible="false" Text="Сайт проиндексирован в Яндекс" CssClass="form-control"/><br />
                                            <br />
                                            <asp:Button runat="server" ID="Get_Cost" Text="Посчитать" OnClick="Get_Cost_Click" Visible="false" CssClass="btn btn-success"/>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <asp:PlaceHolder ID="BasicContent" runat="server"></asp:PlaceHolder>
                                <asp:PlaceHolder ID="ServerContent" runat="server"></asp:PlaceHolder>
                                <asp:PlaceHolder ID="MobileContent" runat="server"></asp:PlaceHolder>

                            </div>
                        </div>
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
