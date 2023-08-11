using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeoKnife
{

    public class Themes
    {
        public string Date { get; set; }
        public string Subject { get; set; }
        public int Views { get; set; }
        public int Messages { get; set; }

        public Themes(string Date, string Subject, int Views, int Messages)
        {
            this.Date = Date;
            this.Subject = Subject;
            this.Views = Views;
            this.Messages = Messages;
        }
    }
}