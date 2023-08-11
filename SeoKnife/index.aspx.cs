using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeoKnife
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Cache["Entered"] != null)
            {

                Login_Text.Visible = false;
                Password_Text.Visible = false;
                error_text.InnerText = "Здраствуйте " + DataBaseClass.UserName + ", вы уже авторизованы.";
                Profile_Image.ImageUrl = "images/profiles/admin.png";
                Profile_Image.Visible = true;
                AuthorizeButton.Text = "Зайти в Кабинет";
            }

        }
        protected void AuthorizeButton_Click(object sender, EventArgs e)
        {
            if (Cache["Entered"] == null)
            {
                DataBaseClass dbc = new DataBaseClass();
                if (dbc.CheckUser(Login_Text.Text, Password_Text.Text) == true)
                {
                    dbc.IsAdminCheck(Login_Text.Text);
                    Cache.Insert("Entered", DateTime.Now, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30));
                    Response.Redirect("Cabinet.aspx");
                }
                else
                {
                    error_text.InnerText = "Не верный пароль или логин.";
                }
            }
            else
            {
                Response.Redirect("Cabinet.aspx");
            }
        }
    }
}