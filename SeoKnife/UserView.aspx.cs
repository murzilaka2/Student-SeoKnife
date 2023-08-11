using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeoKnife
{
    public partial class UserView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserCheckBaseInfo(Request.QueryString["user_name"]);
        }
        private void UserCheckBaseInfo(string name)
        {
            title.Text = (name != null) ? "Seo Knife - "+name+"" : "Seo Knife - Пользователь не указан";
            description.Content = (name != null) ? "Профиль пользователя - " + name + "" : "Профиль пользователя - Пользователь не указан";
            keywords.Content = (name != null) ? "Профиль пользователя - " + name + "" : "Профиль пользователя - Пользователь не указан";
            List<User> user = new List<User>();
            DataBaseClass dbc = new DataBaseClass();
            user = dbc.CheckUserName(name);
            if (user.Count >= 1)
            {
                string Who = "";
                foreach (var item in user)
                {
                    if (item.Admin == "True"){Who = "Администратор";}
                    else { Who = "Пользователь"; }
                    UserInfoPlace.Controls.Add(new LiteralControl("<div style=\"max-width:100%; overflow:auto;\"><table><tr><th>Логин</th><th>Email адрес</th><th>Статус</th><th>Репутация</th><th>Количество сообщений</th></tr><tr><td>" + item.Name+"</td><td>" + item.Email + "</td><td>"+Who+"</td><td>" + item.Reputation + "</td><td>" + item.Messages + "</td></tr></table></div>"));
                }
                UserInfo(name);
            }else
            {
                UserInfoPlace.Controls.Add(new LiteralControl("<h4 class=\"text-bold\"style=\"font-family: mv boli;\">Пользователь не найден</h4>"));
            }
        }
        private void UserInfo(string name)
        {
            List<ListClasses.UserInfo> userinfo = new List<ListClasses.UserInfo>();
            DataBaseClass dbc = new DataBaseClass();
            userinfo = dbc.UserInfo(name);
            foreach (var item in userinfo)
            {
                UserImage.ImageUrl = item.Image;
                UserInfoPlace.Controls.Add(new LiteralControl("<div style=\"max-width:100%; overflow:auto;\"><table>"));
                if (item.Twitter != null) { UserInfoPlace.Controls.Add(new LiteralControl("<tr><th><a href=\"" + item.Twitter + "\" class=\"sb black twitter\">Twitter</a> " + item.Twitter + "</tr></th>")); }
                if (item.Linkedin != null) { UserInfoPlace.Controls.Add(new LiteralControl("<tr><th><a href=\"" + item.Linkedin + "\" class=\"sb black linkedin\">Linkedin</a> " + item.Linkedin + "</tr></th>")); }
                if (item.Facebook != null) { UserInfoPlace.Controls.Add(new LiteralControl("<tr><th><a href=\"" + item.Facebook + "\" class=\"sb black facebook\">Facebook</a> " + item.Facebook + "</tr></th>")); }
                UserInfoPlace.Controls.Add(new LiteralControl("</table>"));
                if (item.Info != null) { UserInfoPlace.Controls.Add(new LiteralControl("<table><tr><th style=\"text-align: center;\">Общая информация</th></tr><tr><td class=\"my-td\">" + item.Info + "</td></tr></table>")); }
                UserInfoPlace.Controls.Add(new LiteralControl("</div>"));
                UserInfoPlace.Controls.Add(new LiteralControl("<div style=\"max-width:100%; overflow:auto;\">"));
                UserInfoPlace.Controls.Add(new LiteralControl("<table><tr><th>Месторасположение</th><td>" + item.Location + "</td></tr><tr><th>Домашняя страница</th><td>" + item.OwnSiteUrl + "</td></tr><tr><th>Контактный телефон</th><td>" + item.ContactPhone + "</td></tr><tr><th>Email адрес</th><td>" + item.ContactEmail + "</td></tr></table>"));
                UserInfoPlace.Controls.Add(new LiteralControl("</div>"));
            }
        }
    }
}