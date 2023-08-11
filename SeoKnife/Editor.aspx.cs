using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeoKnife
{
    public partial class Editor : System.Web.UI.Page
    {
        CKEditor.NET.CKEditorControl MessageText;
        private void CkEdtitorControls()
        {           
            EditorHeader.Attributes.Add("style", "width:100%");
            EditorHeader.ReadOnly = true;
            EditorKeywords.Attributes.Add("style", "width:100%");
            MessageText = new CKEditor.NET.CKEditorControl();
            MessageText.BasePath = "/ckeditor/";
            MessageText.Height = 500;
            EditorText.Controls.Add(MessageText);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Error.Text = null;
            CkEdtitorControls();
            try { SetDataBaseText(Convert.ToInt32(Request.QueryString["edit_id"])); } catch  { Response.Redirect("/Cabinet.aspx"); }
        }
        private void SetDataBaseText(int ID)
        {
            if (CheckUser(ID) == true)
            {
                SqlConnection con = new SqlConnection(DataBaseClass.Connection);
                con.Open();
                SqlCommand Url = new SqlCommand("select * from [Questions] WHERE ID = " + ID + "", con);
                SqlDataReader ReaderUrl = Url.ExecuteReader();
                while (ReaderUrl.Read())
                {
                    EditorHeader.Text = ReaderUrl.GetValue(2).ToString();
                    MessageText.Text = ReaderUrl.GetValue(3).ToString();
                    EditorKeywords.Text = ReaderUrl.GetValue(4).ToString();
                }
                ReaderUrl.Close();
            }
            else
            {
                Response.Redirect("/Cabinet.aspx");
            }
        }
        private bool CheckUser(int ID)
        {
            SqlConnection con = new SqlConnection(DataBaseClass.Connection);
            con.Open();
            //Проверка если админ
            SqlCommand Url = new SqlCommand("select * from [User] WHERE [Name] = '"+DataBaseClass.UserName+"';", con);
            SqlDataReader ReaderUrl = Url.ExecuteReader();
            while (ReaderUrl.Read())
            {
                if (ReaderUrl.GetValue(0).ToString() == "True")
                {
                    ReaderUrl.Close();
                    return true;
                }
            }
            ReaderUrl.Close();
            //Проверка если запись пользователя
            Url = new SqlCommand("select * from [Questions] WHERE ID = " + ID + "", con);
            ReaderUrl = Url.ExecuteReader();
            while (ReaderUrl.Read())
            {
                if(DataBaseClass.UserName == ReaderUrl.GetValue(1).ToString())
                {
                    ReaderUrl.Close();
                    return true;
                }else
                {
                    ReaderUrl.Close();
                    return false;
                }
            }
            ReaderUrl.Close();
            return false;
        }

        private void SetNewText()
        {
            if (MessageText.Text.Length > 50)
            {
                string ForumAdresss = "/Cabinet.aspx";
                string HeadHtml = "<!DOCTYPE html><head><title>Seo Knife - " + EditorHeader.Text + "</title><meta charset=\"utf-8\" /><meta name=\"format-detection\" content=\"telephone=no\" /><meta name=\"description\" content=\"SeoKnife - " + EditorHeader.Text + "\" /><meta name=\"keywords\" content=\"" + EditorKeywords.Text + "\" /><link rel=\"icon\" href=\"../images/favicon.ico\" type=\"image/x-icon\" /><link rel=\"stylesheet\" type=\"text/css\" href=\"https://fonts.googleapis.com/css?family=Open+Sans:400,700,900\"/><link rel=\"stylesheet\" href=\"../css/style.css\" /><link rel=\"stylesheet\" href=\"../css/normalize.css\" /><link rel=\"stylesheet\" href=\"../css/task.css\" /><link rel=\"stylesheet\" href=\"../css/forum.css\" /><link href=\"../css/font-awesome.min.css\" rel=\"stylesheet\"/><link href=\"https://fonts.googleapis.com/css?family=Roboto:300,400,500,700\" rel=\"stylesheet\"/><link href=\"../css/responsive.css\" rel=\"stylesheet\" /></head><body style=\"background-color:white\">";
                string BodyHtml = "<div id=\"PanelFunctions\"><section class=\"section-80 bg-caribbean context-dark\"><div class=\"shell\"><div class=\"range range-xs-center\"><div class=\"cell-xs-10 cell-md-12\"><div class=\"container\"><div class=\"cabinetelements\"><a href=\"" + ForumAdresss + "\"><input type=\"image\" name=\"ctl18\" class=\"imgcabinet\" src=\"../images/indexpage.png\" alt=\"Форум\" style=\"width:95px;\"></a><br>Форум</div></div></div></div></div></section><section class=\"bg-gray-lighter\"><div class=\"shell\"><div class=\"range range-xs-center\" style=\"text-align:-webkit-auto;\"><div class=\"cell-xs-10 cell-md-12\"><br><br><div><div class=\"questions-wrapper\"><div class=\"dwqa-container\"><div class=\"dwqa-questions-archive\"></div><div class=\"dwqa-questions-list\">";
                string Content = "<div class=\"page text-center\" style=\"padding: 20px 180px 20px 80px;\"><h2 style=\"margin-top:30px; margin-bottom:60px;\">" + EditorHeader.Text + "</h2><div style=\"margin-bottom:60px;\">" + MessageText.Text + "</div><small>" + EditorKeywords.Text + "</small>";
                string FooterHtml = " </div><div class=\"dwqa-questions-footer\"><div class=\"dwqa-pagination\"><a class=\"dwqa-next dwqa-page-numbers\" href=\"#\">Назад</a><a class=\"dwqa-next dwqa-page-numbers\" href=\"#\">Вперед</a></div></div><br><br></div></div></div></div></div></div></div></section></div></div></body><footer></footer>";

                string basepath = AppDomain.CurrentDomain.BaseDirectory;
                DataBaseClass d = new DataBaseClass();
                string FileName = d.Translit(EditorHeader.Text.ToLower());
                FileName = @"" + basepath + "forum\\" + FileName;
                StreamWriter streamwriter = new StreamWriter(FileName);
                streamwriter.Write(HeadHtml);
                streamwriter.Write(BodyHtml);
                streamwriter.Write(Content);
                streamwriter.Write(FooterHtml);
                streamwriter.Close();
                SqlConnection con = new SqlConnection(DataBaseClass.Connection);
                con.Open();
                SqlCommand Url = new SqlCommand("UPDATE [Questions] SET [Body] = '" + MessageText.Text + "', [Keywords] = '" + EditorKeywords.Text + "'", con);
                SqlDataReader ReaderUrl = Url.ExecuteReader();
                ReaderUrl.Close();
                Response.Redirect("/Cabinet.aspx");
            }
            else
            {
                Error.Text = "Минимальное количество знаков - 50.";
            }
        }

        protected void EditorSave_Click(object sender, EventArgs e)
        {
            SetNewText();
        }
    }
}