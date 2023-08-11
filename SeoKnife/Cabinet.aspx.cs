using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeoKnife
{
    public partial class Cabinet : System.Web.UI.Page
    {
        private static string SelectedPage { get; set; }
        TextBox SiteUrl { get; set; }
        DataBaseClass dbc = new DataBaseClass();

        private PlaceHolder Page()
        {
            if (SelectedPage == "FR") { return ForumPanelHeader; }
            if (SelectedPage == "AD") { return AdminPanel; }
            if (SelectedPage == "IM") { return DownloadImage; }
            if (SelectedPage == "DS") { return DownloadSite; }
            if (SelectedPage == "FQ") { return ForumPanelNewQuestion; }
            else { PanelFunctions.DefaultButton = "DownloadSiteBut"; return ProfilePanel; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Cache["Entered"] == null)
                {
                    Response.Redirect("index.aspx");
                }
                else
                {
                    CreateContros();
                    Visability();
                }
                if (!IsPostBack)
                {
                    Page().Visible = true;
                    if (SelectedPage == "FR")
                    {
                        ForumPanelBody.Visible = true;
                        ForumPanelFooter.Visible = true;
                    }
                }
                ForumPanelBody.Controls.Add(dbc.ReturnQuestionView());
            }
            catch  { }
        }
        private void Visability()
        {
            DownloadSite.Visible = false;
            DownloadImage.Visible = false;
            AdminPanel.Visible = false;
            ProfilePanel.Visible = false;
            ForumPanelHeader.Visible = false;
            ForumPanelBody.Visible = false;
            ForumPanelFooter.Visible = false;
            ForumPanelNewQuestion.Visible = false;
        }

        private void CreateContros()
        {
            //Шаблон
            HeaderPanel.Controls.Add(new LiteralControl("<section class=\"section-80 bg-caribbean context-dark\">"));
            HeaderPanel.Controls.Add(new LiteralControl("<div class=\"shell\">"));
            HeaderPanel.Controls.Add(new LiteralControl("<div class=\"range range-xs-center\">"));
            HeaderPanel.Controls.Add(new LiteralControl("<div class=\"cell-xs-10 cell-md-12\">"));
            HeaderPanel.Controls.Add(new LiteralControl("<div class=\"container\">"));

            //Главная страница
            HeaderPanel.Controls.Add(new LiteralControl("<div class=\"cabinetelements\">"));
            ImageButton IndexPage = new ImageButton();
            IndexPage.ImageUrl = "images/indexpage.png";
            IndexPage.Width = 95;
            IndexPage.AlternateText = "Главная страница";
            IndexPage.CssClass = "imgcabinet";
            IndexPage.Click += new System.Web.UI.ImageClickEventHandler(IndexPage_Click);
            HeaderPanel.Controls.Add(IndexPage);
            HeaderPanel.Controls.Add(new LiteralControl("<br />Главная страница"));
            HeaderPanel.Controls.Add(new LiteralControl("</div>"));

            //Профайл
            HeaderPanel.Controls.Add(new LiteralControl("<div class=\"cabinetelements\">"));
            ImageButton Profile = new ImageButton();
            Profile.ImageUrl = "images/profile.png";
            Profile.Width = 95;
            Profile.AlternateText = "Профайл";
            Profile.CssClass = "imgcabinet";
            Profile.Click += new System.Web.UI.ImageClickEventHandler(Profile_Click);
            HeaderPanel.Controls.Add(Profile);
            HeaderPanel.Controls.Add(new LiteralControl("<br />Профайл"));
            HeaderPanel.Controls.Add(new LiteralControl("</div>"));

            //Форум
            HeaderPanel.Controls.Add(new LiteralControl("<div class=\"cabinetelements\">"));
            ImageButton Forum = new ImageButton();
            Forum.ImageUrl = "images/forum.png";
            Forum.Width = 95;
            Forum.AlternateText = "Форум";
            Forum.CssClass = "imgcabinet";
            Forum.Click += new System.Web.UI.ImageClickEventHandler(Forum_Click);
            HeaderPanel.Controls.Add(Forum);
            HeaderPanel.Controls.Add(new LiteralControl("<br />Форум"));
            HeaderPanel.Controls.Add(new LiteralControl("</div>"));

            if (DataBaseClass.IsAdmin == true)
            {
                //Админ Панель
                HeaderPanel.Controls.Add(new LiteralControl("<div class=\"cabinetelements\">"));
                ImageButton AdminPanel = new ImageButton();
                AdminPanel.ImageUrl = "images/admin.png";
                AdminPanel.Width = 95;
                AdminPanel.AlternateText = "Админ Панель";
                AdminPanel.CssClass = "imgcabinet";
                AdminPanel.Click += new System.Web.UI.ImageClickEventHandler(AdminPanel_Click);
                HeaderPanel.Controls.Add(AdminPanel);
                HeaderPanel.Controls.Add(new LiteralControl("<br />Админ Панель"));
                HeaderPanel.Controls.Add(new LiteralControl("</div>"));
            }

            //Скачать сайт
            HeaderPanel.Controls.Add(new LiteralControl("<div class=\"cabinetelements\">"));
            ImageButton DownloadSite = new ImageButton();
            DownloadSite.ImageUrl = "images/DownloadSite.png";
            DownloadSite.Width = 95;
            DownloadSite.AlternateText = "Скачать сайт";
            DownloadSite.CssClass = "imgcabinet";
            DownloadSite.Click += new System.Web.UI.ImageClickEventHandler(DownloadSite_Click);
            HeaderPanel.Controls.Add(DownloadSite);
            HeaderPanel.Controls.Add(new LiteralControl("<br />Скачать сайт"));
            HeaderPanel.Controls.Add(new LiteralControl("</div>"));

            //Скачать изображение
            HeaderPanel.Controls.Add(new LiteralControl("<div class=\"cabinetelements\">"));
            ImageButton DownloadImages = new ImageButton();
            DownloadImages.ImageUrl = "images/DownloadImages.png";
            DownloadImages.Width = 95;
            DownloadImages.AlternateText = "Скачать изображения";
            DownloadImages.CssClass = "imgcabinet";
            DownloadImages.Click += new System.Web.UI.ImageClickEventHandler(DownloadImages_Click);
            HeaderPanel.Controls.Add(DownloadImages);
            HeaderPanel.Controls.Add(new LiteralControl("<br />Скачать изображения"));
            HeaderPanel.Controls.Add(new LiteralControl("</div>"));

            //Выход
            HeaderPanel.Controls.Add(new LiteralControl("<div class=\"cabinetelements\">"));
            ImageButton Exite = new ImageButton();
            Exite.ImageUrl = "images/exit.png";
            Exite.Width = 95;
            Exite.AlternateText = "Выход";
            Exite.CssClass = "imgcabinet";
            Exite.Click += new System.Web.UI.ImageClickEventHandler(Exite_Click);
            HeaderPanel.Controls.Add(Exite);
            HeaderPanel.Controls.Add(new LiteralControl("<br />Выход"));
            HeaderPanel.Controls.Add(new LiteralControl("</div>"));
            HeaderPanel.Controls.Add(new LiteralControl("</div>"));
            HeaderPanel.Controls.Add(new LiteralControl("</div>"));
            HeaderPanel.Controls.Add(new LiteralControl("</div>"));
            HeaderPanel.Controls.Add(new LiteralControl("</div>"));
            HeaderPanel.Controls.Add(new LiteralControl("</section>"));

            DS();
            IM();
            AD();
            PR();
            FR();
            NQ();
        }

        private void Forum_Click(object sender, ImageClickEventArgs e)
        {
            SelectedPage = "FR";
            ForumPanelHeader.Visible = true;
            ForumPanelBody.Visible = true;
            ForumPanelFooter.Visible = true;
        }
        private void Profile_Click(object sender, ImageClickEventArgs e)
        {
            SelectedPage = "";
            ProfilePanel.Visible = true;
            PanelFunctions.DefaultButton = "ChangePasswordBut";
        }
        private void AdminPanel_Click(object sender, ImageClickEventArgs e)
        {
            SelectedPage = "AD";
            AdminPanel.Visible = true;
            PanelFunctions.DefaultButton = "AddUserbut";
        }
        private void IndexPage_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("index.aspx");
        }
        private void DownloadImages_Click(object sender, ImageClickEventArgs e)
        {
            SelectedPage = "IM";
            DownloadImage.Visible = true;
            PanelFunctions.DefaultButton = "DownloadImageBut";
        }
        private void DownloadSite_Click(object sender, ImageClickEventArgs e)
        {
            SelectedPage = "DS";
            DownloadSite.Visible = true;
            PanelFunctions.DefaultButton = "DownloadSiteBut";
        }
        private void Exite_Click(object sender, ImageClickEventArgs e)
        {
            Cache.Remove("Entered");
            DataBaseClass.UserName = null;
            SelectedPage = null;
            Response.Redirect("index.aspx");
        }
        private void NewQuestionBut_Click(object sender, EventArgs e)
        {
            SelectedPage = "FQ";
            ForumPanelNewQuestion.Visible = true;
        }

        private void DS()
        {
            DownloadSite.Controls.Add(new LiteralControl("<section class=\"bg-gray-lighter\">"));
            DownloadSite.Controls.Add(new LiteralControl("<div class=\"shell\">"));
            DownloadSite.Controls.Add(new LiteralControl("<div class=\"range range-xs-center\">"));
            DownloadSite.Controls.Add(new LiteralControl("<div class=\"cell-xs-10 cell-md-12\"><br /><br />"));
            DownloadSite.Controls.Add(new LiteralControl("<div class=\"\"><h3>Скачать сайт</h3></div><br /><br />"));

            TextBox SiteUrl = new TextBox();
            SiteUrl.ToolTip = "Адрес сайта";
            SiteUrl.Attributes.Add("placeholder", "Введите адрес сайта");
            SiteUrl.CssClass = "form-control";
            DownloadSite.Controls.Add(SiteUrl);
            Button DownloadSiteBut = new Button();
            DownloadSiteBut.ID = "DownloadSiteBut";
            DownloadSiteBut.Text = "Скачать";
            DownloadSiteBut.CssClass = "btn btn-success";
            DownloadSiteBut.Click += new EventHandler(DownloadSiteFunction_Click);
            DownloadSite.Controls.Add(DownloadSiteBut);

            DownloadSite.Controls.Add(new LiteralControl("<br /><br />"));
            DownloadSite.Controls.Add(new LiteralControl("<div class=\"\"><p>Скачивайте полную версию сайта в 2 клика, без дополнительных настроек.</p></div><br /><br />"));
            DownloadSite.Controls.Add(new LiteralControl("</div>"));
            DownloadSite.Controls.Add(new LiteralControl("</div>"));
            DownloadSite.Controls.Add(new LiteralControl("</div>"));
            DownloadSite.Controls.Add(new LiteralControl("</section>"));
        }
        private void IM()
        {
            DownloadImage.Controls.Add(new LiteralControl("<section class=\"bg-gray-lighter\">"));
            DownloadImage.Controls.Add(new LiteralControl("<div class=\"shell\">"));
            DownloadImage.Controls.Add(new LiteralControl("<div class=\"range range-xs-center\">"));
            DownloadImage.Controls.Add(new LiteralControl("<div class=\"cell-xs-10 cell-md-12\"><br /><br />"));
            DownloadImage.Controls.Add(new LiteralControl("<div class=\"\"><h3>Скачать изображения</h3></div><br /><br />"));

            SiteUrl = new TextBox();
            SiteUrl.Attributes.Add("placeholder", "Введите адрес сайта");
            SiteUrl.ToolTip = "Адрес сайта";
            SiteUrl.CssClass = "form-control";
            DownloadImage.Controls.Add(SiteUrl);
            Button DownloadImageBut = new Button();
            DownloadImageBut.ID = "DownloadImageBut";
            DownloadImageBut.Text = "Скачать";
            DownloadImageBut.CssClass = "btn btn-success";
            DownloadImageBut.Click += new EventHandler(DownloadImageFunction_Click);
            DownloadImage.Controls.Add(DownloadImageBut);

            DownloadImage.Controls.Add(new LiteralControl("<br /><br />"));
            DownloadImage.Controls.Add(new LiteralControl("<div class=\"\"><p>Скачайте все изображения с необходимого сайта в 2 клика.</p></div><br /><br />"));
            DownloadImage.Controls.Add(new LiteralControl("</div>"));
            DownloadImage.Controls.Add(new LiteralControl("</div>"));
            DownloadImage.Controls.Add(new LiteralControl("</div>"));
            DownloadImage.Controls.Add(new LiteralControl("</section>"));
        }

        private TextBox Email { get; set; }
        private TextBox Password { get; set; }
        private TextBox Name { get; set; }
        private CheckBox IsAdmin { get; set; }

        private void AD()
        {
            AdminPanel.Controls.Add(new LiteralControl("<section class=\"bg-gray-lighter\">"));
            AdminPanel.Controls.Add(new LiteralControl("<div class=\"shell\">"));
            AdminPanel.Controls.Add(new LiteralControl("<div class=\"range range-xs-center\">"));
            AdminPanel.Controls.Add(new LiteralControl("<div class=\"cell-xs-10 cell-md-12\"><br /><br />"));
            AdminPanel.Controls.Add(new LiteralControl("<div class=\"\"><h3>Панель Администратора</h3></div><br /><br />"));

            AdminPanel.Controls.Add(new LiteralControl("<h5>Добавление пользователя</h5>"));
            AdminPanel.Controls.Add(new LiteralControl("<div style=\"display:flex;\">"));

            IsAdmin = new CheckBox();
            IsAdmin.Text = "Админ";
            IsAdmin.CssClass = "form-control";
            AdminPanel.Controls.Add(IsAdmin);

            Email = new TextBox();
            Email.TextMode = TextBoxMode.Email;
            Email.Attributes.Add("placeholder", "Введите Email");
            Email.ToolTip = "Введите Email";
            Email.CssClass = "form-control";
            AdminPanel.Controls.Add(Email);

            Password = new TextBox();
            Password.TextMode = TextBoxMode.Password;
            Password.Attributes.Add("placeholder", "Введите Пароль");
            Password.ToolTip = "Введите Пароль";
            Password.CssClass = "form-control";
            AdminPanel.Controls.Add(Password);

            Name = new TextBox();
            Name.Attributes.Add("placeholder", "Введите Имя");
            Name.ToolTip = "Введите Email";
            Name.CssClass = "form-control";
            AdminPanel.Controls.Add(Name);

            Button AddUser = new Button();
            AddUser.ID = "AddUserbut";
            AddUser.Text = "Добавить";
            AddUser.CssClass = "btn btn-success";
            AddUser.Click += new EventHandler(AddUser_Click);
            AdminPanel.Controls.Add(AddUser);
            AdminPanel.Controls.Add(new LiteralControl("</div><br /><br />"));

            AdminPanel.Controls.Add(new LiteralControl("<h5>Список пользователей</h5>"));
            AdminPanel.Controls.Add(new LiteralControl("<div>"));
            AdminPanel.Controls.Add(dbc.ReturnUsers());

            Button DeleteUser = new Button();
            DeleteUser.Text = "Удалить";
            DeleteUser.CssClass = "btn btn-success";
            DeleteUser.Click += new EventHandler(DeleteUser_Click);
            AdminPanel.Controls.Add(DeleteUser);
            AdminPanel.Controls.Add(new LiteralControl("</div>"));

            AdminPanel.Controls.Add(new LiteralControl("<br /><br />"));
            AdminPanel.Controls.Add(new LiteralControl("</div>"));
            AdminPanel.Controls.Add(new LiteralControl("</div>"));
            AdminPanel.Controls.Add(new LiteralControl("</div>"));
            AdminPanel.Controls.Add(new LiteralControl("</section>"));
        }

        private TextBox ChangePassword { get; set; }
        private TextBox ChangePasswordAgaine { get; set; }

        private void PR()
        {
            ProfilePanel.Controls.Add(new LiteralControl("<section class=\"bg-gray-lighter\">"));
            ProfilePanel.Controls.Add(new LiteralControl("<div class=\"shell\">"));
            ProfilePanel.Controls.Add(new LiteralControl("<div class=\"range range-xs-center\">"));
            ProfilePanel.Controls.Add(new LiteralControl("<div class=\"cell-xs-10 cell-md-12\"><br /><br />"));
            Image UserImage = new Image();
            UserImage.ImageUrl = "https://cdn2.iconfinder.com/data/icons/website-icons/512/User_Avatar-512.png";
            UserImage.Width = 150;
            UserImage.Height = 150;
            UserImage.CssClass = "round-image";
            ProfilePanel.Controls.Add(UserImage);

            ProfilePanel.Controls.Add(new LiteralControl("<br /><br /><div style=\"max-width:100%; overflow:auto;\">"));
            string Name;
            string Email;
            string IsAdmin;
            int MessageCout;
            int Reputation;
            dbc.UserInfo(out Name, out Email, out IsAdmin, out MessageCout, out Reputation);
            if (IsAdmin == "True")
            {
                IsAdmin = "Администратор";
            }
            else
            {
                IsAdmin = "Пользователь";
            }
            ProfilePanel.Controls.Add(new LiteralControl("<table><tr><th>Логин</th><th>Email</th><th>Статус</th><th>Сообщений</th><th>Репутация</th></tr>"));
            ProfilePanel.Controls.Add(new LiteralControl("<tr><td>" + Name + "</td><td>" + Email + "</td><td>" + IsAdmin + "</td><td>" + MessageCout + "</td><td>" + Reputation + "</td></tr></table>"));
            ProfilePanel.Controls.Add(new LiteralControl("</div>"));
            UserInfo(DataBaseClass.UserName, UserImage);

            ProfilePanel.Controls.Add(new LiteralControl("<div>"));
            ProfilePanel.Controls.Add(new LiteralControl("<br />"));
            Button ChangeInfodBut = new Button();
            ChangeInfodBut.ID = "ChangeInfodBut";
            ChangeInfodBut.Text = "Изменить";
            ChangeInfodBut.CssClass = "btn btn-success";
            ChangeInfodBut.Click += new EventHandler(ChangeInfodBut_Click);
            ProfilePanel.Controls.Add(ChangeInfodBut);
            ProfilePanel.Controls.Add(new LiteralControl("</div>"));

            ProfilePanel.Controls.Add(new LiteralControl("<br /><br />"));
            ProfilePanel.Controls.Add(new LiteralControl("<h5>Изменение Пароля</h5>"));
            ProfilePanel.Controls.Add(new LiteralControl("<div style=\"display:flex;\">"));

            ChangePassword = new TextBox();
            ChangePassword.TextMode = TextBoxMode.Password;
            ChangePassword.Attributes.Add("placeholder", "Введите новый пароль");
            ChangePassword.ToolTip = "Введите новый пароль";
            ChangePassword.CssClass = "form-control";
            ProfilePanel.Controls.Add(ChangePassword);

            ChangePasswordAgaine = new TextBox();
            ChangePasswordAgaine.TextMode = TextBoxMode.Password;
            ChangePasswordAgaine.Attributes.Add("placeholder", "Подтвердите пароль");
            ChangePasswordAgaine.ToolTip = "Подтвердите пароль";
            ChangePasswordAgaine.CssClass = "form-control";
            ProfilePanel.Controls.Add(ChangePasswordAgaine);

            Button ChangePasswordBut = new Button();
            ChangePasswordBut.ID = "ChangePasswordBut";
            ChangePasswordBut.Text = "Изменить";
            ChangePasswordBut.CssClass = "btn btn-success";
            ChangePasswordBut.Click += new EventHandler(ChangePassword_Click);
            ProfilePanel.Controls.Add(ChangePasswordBut);
            ProfilePanel.Controls.Add(new LiteralControl("</div><br /><br />"));


            //Темы
            ProfilePanel.Controls.Add(new LiteralControl("<h5>Ваши Темы</h5>"));
            ProfilePanel.Controls.Add(new LiteralControl("<div style=\"max-width:100%; overflow:auto;\">"));
            ProfilePanel.Controls.Add(new LiteralControl("<table><tr><th>Дата</th><th>Тема</th><th>Просмотры</th><th>Сообщения</th></tr>"));
            List<Themes> themes = new List<Themes>();
            themes = dbc.UserSubjects();
            for (int i = 0; i < themes.Count; i++)
            {
                ProfilePanel.Controls.Add(new LiteralControl("<tr><td>" + themes[i].Date + "</td><td>" + themes[i].Subject + "</td><td>" + themes[i].Views + "</td><td>" + themes[i].Messages + "</td></tr>"));
            }
            ProfilePanel.Controls.Add(new LiteralControl("</table></div><br />"));
            ProfilePanel.Controls.Add(new LiteralControl("</div>"));
            ProfilePanel.Controls.Add(new LiteralControl("</div>"));
            ProfilePanel.Controls.Add(new LiteralControl("</div>"));
            ProfilePanel.Controls.Add(new LiteralControl("</section>"));
        }

        private void UserInfo(string name, Image UserImage)
        {

            List<ListClasses.UserInfo> userinfo = new List<ListClasses.UserInfo>();
            DataBaseClass dbc = new DataBaseClass();
            userinfo = dbc.UserInfo(name);
            foreach (var item in userinfo)
            {
                UserImage.ImageUrl = item.Image;
                ProfilePanel.Controls.Add(new LiteralControl("<div style=\"max-width:100%; overflow:auto;\"><table>"));
                if (item.Twitter != null) { ProfilePanel.Controls.Add(new LiteralControl("<tr><th><a href=\"" + item.Twitter + "\" class=\"sb black twitter\">Twitter</a> "+ item.Twitter + "</tr></th>")); }
                if (item.Linkedin != null) { ProfilePanel.Controls.Add(new LiteralControl("<tr><th><a href=\"" + item.Linkedin + "\" class=\"sb black linkedin\">Linkedin</a> "+item.Linkedin+"</tr></th>")); }
                if (item.Facebook != null) { ProfilePanel.Controls.Add(new LiteralControl("<tr><th><a href=\"" + item.Facebook + "\" class=\"sb black facebook\">Facebook</a> "+item.Facebook+"</tr></th>")); }
                ProfilePanel.Controls.Add(new LiteralControl("</table>"));
                if (item.Info != null) { ProfilePanel.Controls.Add(new LiteralControl("<table><tr><th style=\"text-align: center;\">Общая информация</th></tr><tr><td>" + item.Info + "</td></tr></table>")); }
                ProfilePanel.Controls.Add(new LiteralControl("</div>"));
                ProfilePanel.Controls.Add(new LiteralControl("<div style=\"max-width:100%; overflow:auto;\">"));
                ProfilePanel.Controls.Add(new LiteralControl("<table><tr><th>Месторасположение</th><td>" + item.Location + "</td></tr><tr><th>Домашняя страница</th><td>" + item.OwnSiteUrl + "</td></tr><tr><th>Контактный телефон</th><td>" + item.ContactPhone + "</td></tr><tr><th>Email адрес</th><td>" + item.ContactEmail + "</td></tr></table>"));
                ProfilePanel.Controls.Add(new LiteralControl("</div>"));
            }
        }

        //Общая информация настройки
        private void ChangeInfodBut_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserEdit.aspx");
        }

        private void FR()
        {
            ForumPanelHeader.Controls.Add(new LiteralControl("<section class=\"bg-gray-lighter\">"));
            ForumPanelHeader.Controls.Add(new LiteralControl("<div class=\"shell\">"));
            ForumPanelHeader.Controls.Add(new LiteralControl("<div class=\"range range-xs-center\" style=\"text-align:-webkit-auto;\">"));
            ForumPanelHeader.Controls.Add(new LiteralControl("<div class=\"cell-xs-10 cell-md-12\"><br /><br />"));
            //Форум начало
            ForumPanelHeader.Controls.Add(new LiteralControl("<div>"));
            ForumPanelHeader.Controls.Add(new LiteralControl("<div class=\"questions-wrapper\">"));
            ForumPanelHeader.Controls.Add(new LiteralControl("<div class=\"dwqa-container\">"));
            ForumPanelHeader.Controls.Add(new LiteralControl("<div class=\"dwqa-questions-archive\">"));
            //Фильтр
            ForumPanelHeader.Controls.Add(new LiteralControl("<div class=\"dwqa-question-filter\">"));
            ForumPanelHeader.Controls.Add(new LiteralControl("<span>Фильтр:</span>"));
            ForumPanelHeader.Controls.Add(new LiteralControl("<ul>"));
            ForumPanelHeader.Controls.Add(new LiteralControl("<li><a href=\"questions.html#\" class=\"active\">Все</a></li>"));
            ForumPanelHeader.Controls.Add(new LiteralControl("<li><a href=\"questions.html#\">Открытые</a></li>"));
            ForumPanelHeader.Controls.Add(new LiteralControl("<li><a href=\"questions.html#\">Решеные</a></li>"));
            ForumPanelHeader.Controls.Add(new LiteralControl("</ul>"));
            //Фильтр Конец
            //Создание новой темы
            ForumPanelHeader.Controls.Add(new LiteralControl("<label for=\"dwqa-sort-by\" class=\"dwqa-sort-by\">"));
            Button NewQuestionBut = new Button();
            NewQuestionBut.Text = "Новая Тема";
            NewQuestionBut.CssClass = "btn btn-success";
            NewQuestionBut.Click += new EventHandler(NewQuestionBut_Click);
            ForumPanelHeader.Controls.Add(NewQuestionBut);
            ForumPanelHeader.Controls.Add(new LiteralControl("</label>"));
            //Создание новой темы конец
            ForumPanelHeader.Controls.Add(new LiteralControl("</div>"));
            //Лист вопросов
            ForumPanelHeader.Controls.Add(new LiteralControl("<div class=\"dwqa-questions-list\"><br />"));
            ForumPanelFooter.Controls.Add(new LiteralControl("</div>"));
            //Лист вопросов конец
            //Навигация
            ForumPanelFooter.Controls.Add(new LiteralControl("<div class=\"dwqa-questions-footer\">"));
            ForumPanelFooter.Controls.Add(new LiteralControl("<div class=\"dwqa-pagination\"><span class=\"dwqa-page-numbers dwqa-current\">1</span><a class=\"dwqa-page-numbers\" href=\"#\">2</a><a class=\"dwqa-next dwqa-page-numbers\" href=\"#\">Вперед</a></div>"));
            ForumPanelFooter.Controls.Add(new LiteralControl("</div><br /><br />"));
            //Навигация конец
            ForumPanelFooter.Controls.Add(new LiteralControl("</div>"));
            ForumPanelFooter.Controls.Add(new LiteralControl("</div>"));
            ForumPanelFooter.Controls.Add(new LiteralControl("</div>"));
            ForumPanelFooter.Controls.Add(new LiteralControl("</div>"));
            //Форум конец
            ForumPanelFooter.Controls.Add(new LiteralControl("</div>"));
            ForumPanelFooter.Controls.Add(new LiteralControl("</div>"));
            ForumPanelFooter.Controls.Add(new LiteralControl("</div>"));
            ForumPanelFooter.Controls.Add(new LiteralControl("</div>"));
            ForumPanelFooter.Controls.Add(new LiteralControl("</section>"));
        }

        //Создание новой темы
        private TextBox HeaderText;
        private CKEditor.NET.CKEditorControl MessageText;
        private TextBox Keywords;
        private void NQ()
        {
            ForumPanelBody.Visible = true;
            //Разметка новой темы
            ForumPanelNewQuestion.Controls.Clear();
            ForumPanelNewQuestion.Controls.Add(new LiteralControl("<section class=\"bg-gray-lighter\">"));
            ForumPanelNewQuestion.Controls.Add(new LiteralControl("<div class=\"shell\"><br /><br />"));

            HeaderText = new TextBox();
            HeaderText.Attributes.Add("placeholder", "Заголовок темы");
            HeaderText.Attributes.Add("style", "width:100%");
            HeaderText.CssClass = "form-control";
            ForumPanelNewQuestion.Controls.Add(HeaderText);

            MessageText = new CKEditor.NET.CKEditorControl();
            MessageText.BasePath = "/ckeditor/";
            MessageText.Height = 500;
            ForumPanelNewQuestion.Controls.Add(MessageText);

            Keywords = new TextBox();
            Keywords.Attributes.Add("placeholder", "Ключевые слова");
            Keywords.Attributes.Add("style", "width:100%");
            Keywords.CssClass = "form-control";
            ForumPanelNewQuestion.Controls.Add(Keywords);
            ForumPanelNewQuestion.Controls.Add(new LiteralControl("<br /><br />"));

            Button AddQuestion = new Button();
            AddQuestion.Text = "Опубликовать";
            AddQuestion.CssClass = "btn btn-success";
            AddQuestion.Click += new EventHandler(AddQuestion_Click);
            ForumPanelNewQuestion.Controls.Add(AddQuestion);

            ForumPanelNewQuestion.Controls.Add(new LiteralControl("<br /><br />"));
            ForumPanelNewQuestion.Controls.Add(new LiteralControl("</section>"));
            ForumPanelNewQuestion.Controls.Add(new LiteralControl("</div>"));
        }

        //Добавление записи на форуме
        private void AddQuestion_Click(object sender, EventArgs e)
        {
            if (HeaderText.Text.Length > 2 && Server.HtmlDecode(MessageText.Text).Length > 50)
            {
                if (dbc.SaveQuestion(HeaderText.Text, Server.HtmlDecode(MessageText.Text), Keywords.Text) == true)
                {
                    ForumPanelBody.Controls.Add(dbc.ReturnQuestionView());
                    ForumPanelHeader.Visible = true;
                    ForumPanelBody.Visible = true;
                    ForumPanelFooter.Visible = true;
                    SelectedPage = "FR";
                }
                else
                {
                    ForumPanelNewQuestion.Controls.Add(new LiteralControl("Такая тема уже есть."));
                    ForumPanelNewQuestion.Visible = true;
                }
            }
            else
            {
                ForumPanelNewQuestion.Controls.Add(new LiteralControl("Минимальное количество знаков - 50."));
                ForumPanelNewQuestion.Visible = true;
            }
        }
        //Изменение пароля
        private void ChangePassword_Click(object sender, EventArgs e)
        {
            if (ChangePassword.Text.Length >= 4 && ChangePasswordAgaine.Text.Length >= 4 && ChangePassword.Text == ChangePasswordAgaine.Text)
            {
                if (dbc.ChangePassword(DataBaseClass.UserName, ChangePassword.Text) == true)
                {
                    ProfilePanel.Controls.Add(new LiteralControl("Пароль успешно изменен."));
                }
                else
                {
                    ProfilePanel.Controls.Add(new LiteralControl("Ошибка."));
                }
            }
            else
            {
                ProfilePanel.Controls.Add(new LiteralControl("Введите одиннаковый пароль."));
            }
            ProfilePanel.Visible = true;
        }
        //Удаление пользователя
        private void DeleteUser_Click(object sender, EventArgs e)
        {
            if (dbc.DeleteUser(dbc.UsersList.SelectedValue) == true)
            {
                AdminPanel.Controls.Add(new LiteralControl("Пользователь успешно удален."));
            }
            else
            {
                AdminPanel.Controls.Add(new LiteralControl("Ошибка."));
            }
            AdminPanel.Visible = true;
        }
        //Добавление пользователя
        private void AddUser_Click(object sender, EventArgs e)
        {
            if (Email.Text.Length > 3 && Password.Text.Length > 3 && Name.Text.Length > 3)
            {
                int num = 0;
                if (IsAdmin.Checked == true)
                {
                    num = 1;
                }
                if (dbc.SaveUser(num, Email.Text, Password.Text, Name.Text) == true)
                {
                    Email.Text = "";
                    Name.Text = "";
                    AdminPanel.Controls.Add(new LiteralControl("Пользователь успешно добавлен."));
                }
                else
                {
                    AdminPanel.Controls.Add(new LiteralControl("Такой пользователь уже есть."));
                }
            }
            else
            {
                AdminPanel.Controls.Add(new LiteralControl("Введите корректные данные"));
            }
            AdminPanel.Visible = true;
        }
        //Скачивание изображений
        private void DownloadImageFunction_Click(object sender, EventArgs e)
        {
            if (SiteUrl.Text.Length < 3) { DownloadImage.Controls.Add(new LiteralControl("Введите корректный адрес сайта.")); }
            else
            {
                CabinetTools ct = new CabinetTools();
                ct.GetImages(SiteUrl.Text);
                DownloadImage.Controls.Add(new LiteralControl("Изображения успешно скачаны!"));
            }
            DownloadImage.Visible = true;
        }
        //Скачивание сайта
        private void DownloadSiteFunction_Click(object sender, EventArgs e)
        {
            if (SiteUrl.Text.Length < 3) { DownloadSite.Controls.Add(new LiteralControl("Введите корректный адрес сайта.")); }
            else { }
            DownloadSite.Visible = true;
        }
    }
}