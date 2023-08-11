using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeoKnife
{
    public partial class UserEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DataBaseClass.UserName == null){Response.Redirect("index.aspx");}
            if (!IsPostBack){LoadDB();}
        }
        private void LoadDB()
        {
            SqlConnection con = new SqlConnection(DataBaseClass.Connection);
            con.Open();
            SqlCommand Url = new SqlCommand("select * from [UserProfile] WHERE [Name] =  '" + DataBaseClass.UserName + "';", con);
            SqlDataReader ReaderUrl = Url.ExecuteReader();
            while (ReaderUrl.Read())
            {
                Info.Text = ReaderUrl.GetValue(4).ToString();
                Location.Text = ReaderUrl.GetValue(5).ToString();
                OwnSiteUrl.Text = ReaderUrl.GetValue(6).ToString();
                ContactPhone.Text = ReaderUrl.GetValue(7).ToString();
                ContactEmail.Text = ReaderUrl.GetValue(8).ToString();
                Twitter.Text = ReaderUrl.GetValue(9).ToString();
                Linkedin.Text = ReaderUrl.GetValue(10).ToString();
                Facebook.Text = ReaderUrl.GetValue(11).ToString();
            }
            ReaderUrl.Close();
        }
        private void SaveDB()
        {
            SqlConnection con = new SqlConnection(DataBaseClass.Connection);
            con.Open();
            SqlCommand Url = new SqlCommand("UPDATE[UserProfile] SET[Info] = '" +Info.Text+ "', [Location] = '"+Location.Text+"', [OwnSiteUrl] = '"+OwnSiteUrl.Text+"', [ContactPhone] = '"+ContactPhone.Text+"', [ContactEmail]  = '"+ContactEmail.Text+"', [Twitter]  = '"+Twitter.Text+"', [Linkedin] = '"+Linkedin.Text+"', [Facebook] = '"+Facebook.Text+"' WHERE[Name] = '" + DataBaseClass.UserName + "' ", con);
            SqlDataReader ReaderUrl = Url.ExecuteReader();           
            ReaderUrl.Close();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            SaveDB();
            Response.Redirect("Cabinet.aspx");
        }
    }    
}