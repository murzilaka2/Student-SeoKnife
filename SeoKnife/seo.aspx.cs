using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeoKnife
{
    public partial class seo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                YandexRegion.Items.Add("ua");
                YandexRegion.Items.Add("ru");
            }
        }
        public void ShowKey()
        {
            KeyWords key = new KeyWords();
            YandexKeyPosition.Text = "Позиция ключевого слова :  " + key.GetKeys(YandexKeyWord.Text, SiteUrl.Text, Convert.ToInt32(ViewDepth.Text), YandexRegion.SelectedItem.ToString()).ToString();
        }

        protected void YandexKeyFind_Click(object sender, EventArgs e)
        {
            ShowKey();
        }
    }
}