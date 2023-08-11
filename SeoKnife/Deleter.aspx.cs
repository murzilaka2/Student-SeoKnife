using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeoKnife
{
    public partial class Deleter : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try { CheckUser(Convert.ToInt32(Request.QueryString["delete_id"])); } catch  { Response.Redirect("/Cabinet.aspx"); }
        }
        private void CheckUser(int Number)
        {               
            SqlConnection con = new SqlConnection(DataBaseClass.Connection);
            con.Open();
            SqlCommand Url = new SqlCommand("select * from [User];", con);
            SqlDataReader ReaderUrl = Url.ExecuteReader();
            while (ReaderUrl.Read())
            {
                if(DataBaseClass.UserName == ReaderUrl.GetValue(3).ToString() && Convert.ToInt32(ReaderUrl.GetValue(0)) == 1)
                {
                    Delete(Number);
                    ReaderUrl.Close();
                }
            }
            ReaderUrl.Close();
            Url = new SqlCommand("select * from [Questions] WHERE [ID] = " + Number + ";", con);
            ReaderUrl = Url.ExecuteReader();
            while (ReaderUrl.Read())
            {
                if (DataBaseClass.UserName == ReaderUrl.GetValue(1).ToString())
                {
                    ReaderUrl.Close();
                    Delete(Number);
                }
                else
                {
                    ReaderUrl.Close();
                    Response.Redirect("/Cabinet.aspx");
                }
            }
            Response.Redirect("/Cabinet.aspx");
        }
        private void Delete(int Number)
        {
            string File = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                SqlConnection con = new SqlConnection(DataBaseClass.Connection);
                con.Open();
                SqlCommand Url = new SqlCommand("select * from [Questions] WHERE [ID] = " + Number + ";", con);
                SqlDataReader ReaderUrl = Url.ExecuteReader();
                while (ReaderUrl.Read())
                {
                    File += ReaderUrl.GetValue(6).ToString();
                }
                ReaderUrl.Close();
                System.IO.File.Delete(File);
                Url = new SqlCommand("delete from [Questions] WHERE [ID] = " + Number + ";", con);
                ReaderUrl = Url.ExecuteReader();
                ReaderUrl.Close();
                Response.Redirect("/Cabinet.aspx");
            }
            catch  { Response.Redirect("/Cabinet.aspx"); }
        }
    }
}