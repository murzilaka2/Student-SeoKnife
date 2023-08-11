<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cabinet.aspx.cs" Inherits="SeoKnife.Cabinet" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seo Knife - Кабинет</title>
    <meta charset="utf-8" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="viewport" content="width=device-width, height=device-height, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta name="description" content="SeoKnife - Личный Кабинет" />
    <meta name="keywords" content="SeoKnife - Личный Кабинет" />
    <link rel="icon" href="images/favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Open+Sans:400,700,900" />
    <link rel="stylesheet" href="css/style.css" />
    <link rel="stylesheet" href="css/normalize.css" />
    <link rel="stylesheet" href="css/task.css" />
     <link rel="stylesheet" href="css/forum.css" />
    <link href="css/font-awesome.min.css" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700" rel="stylesheet"/> 
    <link href="css/responsive.css" rel="stylesheet"/>
    <link href="css-social-buttons/social-buttons.css" rel="stylesheet" />
</head>

<body>

    <form id="form1" runat="server">         
        <div class="page text-center">
            <main class="page-content" />
            <div id="responsive" class="section-navigation">

                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <%-- Функции сайта --%>
                        <asp:Panel runat="server" ID="PanelFunctions">
                        <asp:PlaceHolder runat="server" ID="HeaderPanel"></asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="DownloadSite"></asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="DownloadImage"></asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="AdminPanel"></asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="ProfilePanel"></asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="ForumPanelHeader"></asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="ForumPanelBody"></asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="ForumPanelNewQuestion"></asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="ForumPanelFooter"></asp:PlaceHolder>
                        </asp:Panel>
                        <%-- Функции сайта конец --%>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <footer id="contacts" class="section-80 section-bottom-30 page-footer bg-gray-base context-dark section-navigation" runat="server">
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
                                            <img src="../images/logo-footer.png" width="184" height="56" alt="Seo Knife" class="img-responsive reveal-inline-block" oncontextmenu="return false;" /></a></div>
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
        </div>
    </form>
</body>
</html>
