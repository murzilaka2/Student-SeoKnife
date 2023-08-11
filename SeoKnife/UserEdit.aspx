<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="SeoKnife.UserEdit" %>

<!DOCTYPE html>
<head runat="server">
    <title>Seo Knife - Редактирование пользователя</title>
    <meta charset="utf-8" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="viewport" content="width=device-width, height=device-height, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta name="description" content="SeoKnife - Редактирование пользователя." />
    <meta name="keywords" content="Редактирование пользователя." />
    <link rel="icon" href="../images/favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Open+Sans:400,700,900" />
    <link rel="stylesheet" href="../css/style.css" />
    <link rel="stylesheet" href="../css/normalize.css" />
    <link rel="stylesheet" href="../css/task.css" />
    <link rel="stylesheet" href="../css/forum.css" />
    <link href="../css/font-awesome.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700" rel="stylesheet" />
    <link href="../css/responsive.css" rel="stylesheet" />
    <link href="css-social-buttons/social-buttons.css" rel="stylesheet" />
</head>
<body style="background-color: white">
    <div id="PanelFunctions">
        <section class="section-80 bg-caribbean context-dark">
            <div class="shell">
                <div class="range range-xs-center">
                    <div class="cell-xs-10 cell-md-12">
                        <div class="container">
                            <div class="cabinetelements">
                                <a href="/Cabinet.aspx">
                                    <input type="image" name="ctl18" class="imgcabinet" src="../images/indexpage.png" alt="Назад" style="width: 95px;">
                                </a>
                                <br>
                                Назад
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-gray-lighter">
            <div class="shell">
                <div class="range range-xs-center" style="text-align: -webkit-auto;">
                    <div class="cell-xs-10 cell-md-12">
                        <br>
                        <br>
                        <div>
                            <form id="form1" runat="server">
                                <div class="questions-wrapper">
                                    <div class="dwqa-container">
                                        <div class="dwqa-questions-archive"></div>
                                        <div class="dwqa-questions-list" style="text-align: center">
                                            <table>
                                                <tr>
                                                    <th>Facebook</th>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="Facebook" Width="100%" CssClass="bg-gray-lighter" BorderWidth="1" BorderStyle="Dashed"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <th>Twitter</th>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="Twitter" Width="100%" CssClass="bg-gray-lighter" BorderWidth="1" BorderStyle="Dashed"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <th>Linkedin</th>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="Linkedin" Width="100%" CssClass="bg-gray-lighter" BorderWidth="1" BorderStyle="Dashed"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <th style="text-align: center;">Общая информация</th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="Info" TextMode="MultiLine" Width="100%" Height="175px" Style="resize: none" CssClass="bg-gray-lighter" BorderWidth="1" BorderStyle="Dashed"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <th>Месторасположение</th>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="Location" Width="100%" CssClass="bg-gray-lighter" BorderWidth="1" BorderStyle="Dashed"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <th>Домашняя страница</th>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="OwnSiteUrl" Width="100%" CssClass="bg-gray-lighter" BorderWidth="1" BorderStyle="Dashed"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <th>Контактный телефон</th>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="ContactPhone" Width="100%" CssClass="bg-gray-lighter" BorderWidth="1" BorderStyle="Dashed"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <th>Email адрес</th>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="ContactEmail" Width="100%" TextMode="Email" CssClass="bg-gray-lighter" BorderWidth="1" BorderStyle="Dashed"></asp:TextBox></td>
                                                </tr>
                                            </table>
                                            <br />
                                            <asp:Button runat="server" ID="Save" Text="Сохранить" CssClass="btn btn-success" OnClick="Save_Click" />
                                        </div>
                                    </div>
                                    <br>
                                    <br>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</body>
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
                        <p>
                            <span class="small">Текущая версия программы - 1.0.<br />
                                Если возникли технические неполадки, просьба обратиться в <a href="#">поддержку</a>.</span>
                        </p>
                        <div class="offset-top-30">
                        </div>
                    </div>
                    <div class="cell-xs-12 offset-top-66 cell-lg-4 cell-lg-push-1 offset-lg-top-0">
                        <div class="footer-brand">
                            <a href="index.aspx">
                                <img src="images/logo-footer.png" width="184" height="56" alt="Seo Knife" class="img-responsive reveal-inline-block" oncontextmenu="return false;" /></a>
                        </div>
                        <div class="offset-top-30">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</footer>
