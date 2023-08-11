using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SeoKnife
{
    public class DataBaseClass
    {   //"Data Source = localhost; Initial Catalog = u0371616_seoknife; User id = u0371616_seoknife; Password = yu7Bo~37;";
        //"Data Source = localhost; Initial Catalog = SeoKnife; Integrated Security = true;"
        public static string Connection = "Data Source = localhost; Initial Catalog = u0371616_seoknife; User id = u0371616_seoknife; Password = yu7Bo~37;";
        public static bool IsAdmin { get; set; }
        public static string UserName { get; set; }
        //Хеширование Пароля
        public string MD5HASH(string Password)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(Password);
            byte[] hash = md5.ComputeHash(inputBytes);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        //Проверка входа
        public bool IsAdminCheck(string name)
        {
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand Url = new SqlCommand("select * from [User]", con);
            SqlDataReader ReaderUrl = Url.ExecuteReader();
            while (ReaderUrl.Read())
            {
                if (name == ReaderUrl.GetValue(4).ToString() && Convert.ToInt32(ReaderUrl.GetValue(1)) == 1)
                {
                    IsAdmin = true;
                    ReaderUrl.Close();
                    return IsAdmin;
                }
            }
            IsAdmin = false;
            ReaderUrl.Close();
            return IsAdmin;

        }
        public bool CheckUser(string name, string password)
        {
            try
            {
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                SqlCommand Url = new SqlCommand("select * from [User]", con);
                SqlDataReader ReaderUrl = Url.ExecuteReader();
                while (ReaderUrl.Read())
                {
                    if (name == ReaderUrl.GetValue(4).ToString() && MD5HASH(password) == ReaderUrl.GetValue(3).ToString())
                    {
                        UserName = name;
                        ReaderUrl.Close();
                        return true;
                    }
                }
                ReaderUrl.Close();
                return false;
            }
            catch 
            {
                return false;
            }
        }
        //Админ Панель
        public System.Web.UI.WebControls.DropDownList UsersList { get; set; }
     
        //Сохранение пользователя
        public bool SaveUser(int IsAdmin, string Email, string Password, string Name)
        {
            if (CheckUser2(Name, Password, Email) == false)
            {               
                try
                {
                    SqlConnection con = new SqlConnection(Connection);
                    con.Open();
                    SqlCommand Url = new SqlCommand("insert into [User] values (" + IsAdmin + ",'" + Email + "', '" + MD5HASH(Password) + "', '" + Name + "', '" + null + "', '" + null + "')", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                    Url = new SqlCommand("insert into [UserProfile] values ('"+Name+"','"+Email+ "','https://cdn2.iconfinder.com/data/icons/website-icons/512/User_Avatar-512.png','','','','','','','','')", con);
                    ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                    return true;
                }
                catch 
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private bool CheckUser2(string name, string password, string email)
        {
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand Url = new SqlCommand("select * from [User]", con);
            SqlDataReader ReaderUrl = Url.ExecuteReader();
            while (ReaderUrl.Read())
            {
                if (name == ReaderUrl.GetValue(4).ToString() || email == ReaderUrl.GetValue(2).ToString())
                {
                    ReaderUrl.Close();
                    return true;
                }
            }
            ReaderUrl.Close();
            return false;
        }
        public System.Web.UI.WebControls.DropDownList ReturnUsers()
        {
            UsersList = new System.Web.UI.WebControls.DropDownList();
            UsersList.CssClass = "form-control";
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand Url = new SqlCommand("select [Email] from [User]", con);
            SqlDataReader ReaderUrl = Url.ExecuteReader();
            while (ReaderUrl.Read())
            {
                UsersList.Items.Add(ReaderUrl.GetValue(0).ToString());
            }
            UsersList.DataBind();
            ReaderUrl.Close();
            return UsersList;
        }
        public bool DeleteUser(string Email)
        {
            if (Email == "enykoruna1@gmail.com") { return false; }
            try
            {
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                SqlCommand Url = new SqlCommand("select * from [User] WHERE [Name] =  '"+UserName+"';", con);
                SqlDataReader ReaderUrl = Url.ExecuteReader();
                while (ReaderUrl.Read())
                {
                    if (Email == ReaderUrl.GetValue(2).ToString()) {
                        ReaderUrl.Close();
                        Url = new SqlCommand("delete from [User] Where [Email] = '" + Email + "'", con);
                        ReaderUrl = Url.ExecuteReader();
                        ReaderUrl.Close();
                        Url = new SqlCommand("delete from [UserProfile] Where [Email] = '" + Email + "'", con);
                        ReaderUrl = Url.ExecuteReader();
                        ReaderUrl.Close();
                        HttpContext.Current.Cache.Remove("Entered");
                        return true;
                    }else
                    {
                        ReaderUrl.Close();
                        Url = new SqlCommand("delete from [User] Where [Email] = '" + Email + "'", con);
                        ReaderUrl = Url.ExecuteReader();
                        ReaderUrl.Close();
                        Url = new SqlCommand("delete from [UserProfile] Where [Email] = '" + Email + "'", con);
                        ReaderUrl = Url.ExecuteReader();
                        ReaderUrl.Close();
                        return true;
                    }
                }
                return false;    
            }
            catch 
            {
                return false;
            }
        }   
        public void UserInfo(out string Name, out string Email, out string IsAdmin, out int MessageCout, out int Reputation)
        {
            Name = "";
            Email = "";
            IsAdmin = "";
            MessageCout = 0;
            Reputation = 0;
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand Url = new SqlCommand("select * from [User]", con);
            SqlDataReader ReaderUrl = Url.ExecuteReader();
            while (ReaderUrl.Read())
            {
                if (UserName == ReaderUrl.GetValue(4).ToString())
                {
                    Name = ReaderUrl.GetValue(4).ToString();
                    Email = ReaderUrl.GetValue(2).ToString();
                    IsAdmin = ReaderUrl.GetValue(1).ToString();
                    MessageCout = Convert.ToInt32(ReaderUrl.GetValue(5));
                    Reputation = Convert.ToInt32(ReaderUrl.GetValue(6));
                }
            }
            ReaderUrl.Close();
        }
        public bool ChangePassword(string Name, string Password)
        {
            try
            {
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                SqlCommand Url = new SqlCommand("UPDATE [User] SET [Password]='" + MD5HASH(Password) + "' WHERE [Name]='" + Name + "' ", con);
                SqlDataReader ReaderUrl = Url.ExecuteReader();
                ReaderUrl.Close();
                return true;
            }catch
            {
                return false;
            }
        }
        //Транслит
        public string Translit(string Text)
        {
            char[] rus = new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', ' ','a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z' };
            string[] eng = new string[] { "a", "b", "v", "g", "d", "e", "jo", "zh", "z", "i", "j", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "h", "ts", "ch", "sh", "sch", "\"", "y", "", "`e", "ju", "_", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            string TranslitText = "";

            foreach (char ch in Text)
            {
                for (int i = 0; i < rus.Length; i++)
                {
                    if (ch == rus[i])
                    {
                        TranslitText += eng[i];
                    }
                }
            }
            TranslitText += ".html";
            return TranslitText; 
        }
        //Генерация страницы
        private bool Page(string FileName, string Header, string Body, string Keywords)
        {
            string ForumAdresss = "/Cabinet.aspx"; 
            string HeadHtml = "<!DOCTYPE html><head><title>Seo Knife - "+Header+ "</title><meta charset=\"utf-8\" /><meta name=\"format-detection\" content=\"telephone=no\" /><meta name=\"viewport\" content=\"width=device-width, height=device-height, initial-scale=1.0, maximum-scale=1.0, user-scalable=0\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\"><meta name=\"description\" content=\"SeoKnife - " + Header+"\" /><meta name=\"keywords\" content=\""+Keywords+ "\" /><link rel=\"icon\" href=\"../images/favicon.ico\" type=\"image/x-icon\" /><link rel=\"stylesheet\" type=\"text/css\" href=\"https://fonts.googleapis.com/css?family=Open+Sans:400,700,900\"/><link rel=\"stylesheet\" href=\"../css/style.css\" /><link rel=\"stylesheet\" href=\"../css/normalize.css\" /><link rel=\"stylesheet\" href=\"../css/task.css\" /><link rel=\"stylesheet\" href=\"../css/forum.css\" /><link href=\"../css/font-awesome.min.css\" rel=\"stylesheet\"/><link href=\"https://fonts.googleapis.com/css?family=Roboto:300,400,500,700\" rel=\"stylesheet\"/><link href=\"../css/responsive.css\" rel=\"stylesheet\" /></head><body style=\"background-color:white\">";
            string BodyHtml = "<div id=\"PanelFunctions\"><section class=\"section-80 bg-caribbean context-dark\"><div class=\"shell\"><div class=\"range range-xs-center\"><div class=\"cell-xs-10 cell-md-12\"><div class=\"container\"><div class=\"cabinetelements\"><a href=\""+ForumAdresss+"\"><input type=\"image\" name=\"ctl18\" class=\"imgcabinet\" src=\"../images/indexpage.png\" alt=\"Форум\" style=\"width:95px;\"></a><br>Форум</div></div></div></div></div></section><section class=\"bg-gray-lighter\"><div class=\"shell\"><div class=\"range range-xs-center\" style=\"text-align:-webkit-auto;\"><div class=\"cell-xs-10 cell-md-12\"><br><br><div><div class=\"questions-wrapper\"><div class=\"dwqa-container\"><div class=\"dwqa-questions-archive\"></div><div class=\"dwqa-questions-list\">";
            string Content = "<div class=\"page text-center\"><h2 style=\"margin-top:30px; margin-bottom:60px;\">"+Header+ "</h2><div style=\"margin-bottom:60px;\">"+Body+ "</div><small>"+Keywords+ "</small><br /><br /><br /><div style=\"background-color:#f5f5f5\"><hr>Ваш комментарий<hr></div><textarea name=\"BodyText\" class=\"form-control\" style=\"width: 100%; height: 250px\"></textarea><div style=\"background-color:#f5f5f5\"><hr><a href=\"#\" class=\"btn btn-success\">Отправить</a><hr></div></div>";
            string FooterHtml = " </div><div class=\"dwqa-questions-footer\"><div class=\"dwqa-pagination\"></div></div><br><br></div></div></div></div></div></div></div></section></div></div></body><footer id=\"contacts\" class=\"section-80 section-bottom-30 page-footer bg-gray-base context-dark section-navigation\"><div class=\"shell section-relative\"><div class=\"range range-sm-center text-lg-left\"><div class=\"cell-sm-8 cell-md-12\"><div class=\"range range-xs-center\"><div class=\"cell-xs-7 text-xs-left cell-md-4 cell-lg-4 cell-lg-push-2\"><p class=\"text-uppercase text-spacing-60 text-ubold\">Страницы сайта</p><p><a href=\"index.aspx\">Главная</a></p><p><a href=\"siteanaliz.aspx\">Анализ Сайта</a></p><p><a href=\"seo.aspx\">Анализ Позиций</a></p></div><div class=\"cell-xs-12 offset-top-40 cell-md-5 offset-md-top-0 text-md-left cell-lg-4 cell-lg-push-3\"><p class=\"text-uppercase text-spacing-60 text-ubold\">Информация</p><p><span class=\"small\">Текущая версия программы - 1.0.<br />Если возникли технические неполадки, просьба обратиться в <a href=\"#\">поддержку</a>.</span></p><div class=\"offset-top-30\"></div></div><div class=\"cell-xs-12 offset-top-66 cell-lg-4 cell-lg-push-1 offset-lg-top-0\"><div class=\"footer-brand\"><a href=\"index.aspx\"><img src=\"../images/logo-footer.png\" width=\"184\" height=\"56\" alt=\"Seo Knife\" class=\"img-responsive reveal-inline-block\" oncontextmenu=\"return false;\" /></a></div><div class=\"offset-top-30\"></div></div></div></div></div></div><div class=\"shell offset-top-70\"></div></footer>";
            string basepath = AppDomain.CurrentDomain.BaseDirectory;
            FileName = @""+basepath +"forum\\"+ FileName;
            StreamWriter streamwriter = new StreamWriter(FileName);
            streamwriter.Write(HeadHtml);
            streamwriter.Write(BodyHtml);
            streamwriter.Write(Content);
            streamwriter.Write(FooterHtml);
            streamwriter.Close();
            return true;
        }
        //Проверка заголовка темы
        private bool HeaderCheck(string Header)
        {
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand Url = new SqlCommand("select * from [Questions]", con);
            SqlDataReader ReaderUrl = Url.ExecuteReader();
            while (ReaderUrl.Read())
            {
                if (Header == ReaderUrl.GetValue(2).ToString())
                {
                    ReaderUrl.Close();
                    return false;
                }
            }
            ReaderUrl.Close();
            return true;
        }
     
        //Форум
        public bool SaveQuestion( string Header, string Body, string Keywords)
        {
            if (HeaderCheck(Header) == false)
            {
                return false;
            }
            else
            {
                try
                {
                    string ID = "";
                    string FileName = Translit(Header.ToLower());
                    DateTime Date = DateTime.Now;
                    
                    //Создаем страницу
                    Page(FileName, Header, Body, Keywords);

                    SqlConnection con = new SqlConnection(Connection);
                    con.Open();
                    SqlCommand Url = new SqlCommand("insert into [Questions] values ('" + UserName + "','" + Header + "', '" + Body + "','" + Keywords + "','" + null + "','" + "forum/"+FileName + "')", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                    Url = new SqlCommand("select * from Questions WHERE Header = '"+Header+"';", con);
                    ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        ID = ReaderUrl.GetValue(0).ToString();
                    }
                    ReaderUrl.Close();
                    string QuestionView = "<div class=\"dwqa-question-item dwqa-sticky\"><header class=\"dwqa-question-title\"><a href=\"forum\\" + FileName + "\"> " + Header + "</a></header><div class=\"dwqa-question-meta\"><span class=\"dwqa-status dwqa-status-open\" title=\"Открыть\">Открыть</span><span><a href=\"UserView.aspx?user_name="+UserName+"\">" + UserName + "</a> создал " + Date.ToString() + "</span><span class=\"dwqa-question-category\"> • <a href=\"#\" rel=\"tag\">Новости</a></span></div><div class=\"dwqa-question-stats\"><span class=\"dwqa-views-count\"><br />109</span><a href=\"editor.aspx?edit_id=" + ID + "\"><span class=\"dwqa-votes-count\"><br />Изменить</span></a><a href=\"deleter.aspx?delete_id=" + ID + "\"><span class=\"dwqa-answers-count\"><br />Удалить</span></a></div></div>";
                    Url = new SqlCommand("Update [Questions] set [QuestionView] = '"+QuestionView+"' Where [ID] = " + ID + "", con);
                    ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
                catch 
                {
                    return false;
                }
            }
            return true;
        }
        public PlaceHolder ReturnQuestionView()
        {
            PlaceHolder rqv = new PlaceHolder();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand Url = new SqlCommand("select * from [Questions] ORDER BY [ID] DESC ", con);
            SqlDataReader ReaderUrl = Url.ExecuteReader();
            while (ReaderUrl.Read())
            {
                rqv.Controls.Add(new System.Web.UI.LiteralControl(ReaderUrl.GetValue(5).ToString()));
            }
            ReaderUrl.Close();
            return rqv;
        }
        
        //Темы пользователя
        public List<Themes> UserSubjects()
        {
            List<Themes> themes = new List<Themes>();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand Url = new SqlCommand("select * from Questions WHERE Author = '"+UserName+"'", con);
            SqlDataReader ReaderUrl = Url.ExecuteReader();
            while (ReaderUrl.Read())
            {
                themes.Add(new Themes("22.09.2017", "<a href=\"" +ReaderUrl.GetValue(6).ToString() + "\">" + ReaderUrl.GetValue(2).ToString() + "</a>",123,2));

            }
            ReaderUrl.Close();
            return themes;
        }
        //Вывод информации пользователя
        public List<User> CheckUserName(string name)
        {
            List<User> user = new List<User>();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand Url = new SqlCommand("select * from [User] WHERE [Name] = '" + name + "'", con);
            SqlDataReader ReaderUrl = Url.ExecuteReader();
            while (ReaderUrl.Read())
            {
                user.Add(new User(ReaderUrl.GetValue(1).ToString(), ReaderUrl.GetValue(2).ToString(), ReaderUrl.GetValue(4).ToString(), (int)ReaderUrl.GetValue(5), 123, (int)ReaderUrl.GetValue(6)));
            }
            ReaderUrl.Close();
            return user;
            }
        
        //Вывод дополнительной информации пользователя
        public List<ListClasses.UserInfo> UserInfo(string name)
        {
            string email = "";
            List<ListClasses.UserInfo> user = new List<ListClasses.UserInfo> ();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand Url = new SqlCommand("select * from [User] WHERE [Name] = '" + name + "'", con);
            SqlDataReader ReaderUrl = Url.ExecuteReader();
            while (ReaderUrl.Read())
            {
                email = ReaderUrl.GetValue(2).ToString();
            }
                ReaderUrl.Close();

            Url = new SqlCommand("select * from [UserProfile] WHERE [Email] = '" + email + "'", con);
            ReaderUrl = Url.ExecuteReader();
            while (ReaderUrl.Read())
            {
                user.Add(new ListClasses.UserInfo(ReaderUrl.GetValue(2).ToString(), ReaderUrl.GetValue(3).ToString(), ReaderUrl.GetValue(4).ToString(), ReaderUrl.GetValue(5).ToString(), ReaderUrl.GetValue(6).ToString(), ReaderUrl.GetValue(7).ToString(), ReaderUrl.GetValue(8).ToString(), ReaderUrl.GetValue(9).ToString(), ReaderUrl.GetValue(10).ToString(), ReaderUrl.GetValue(11).ToString()));
            }
            ReaderUrl.Close();
            return user;
        }
    }
}