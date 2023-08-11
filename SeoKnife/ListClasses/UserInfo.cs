using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeoKnife.ListClasses
{
    public class UserInfo
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Info { get; set; }
        public string Location { get; set; }
        public string OwnSiteUrl{ get; set; }
        public string ContactPhone{ get; set; }
        public string ContactEmail { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }
        public string Facebook { get; set; }

        public UserInfo(string Name, string Image, string Info, string Location, string OwnSiteUrl, string ContactPhone, string ContactEmail, string Twitter, string Linkedin, string Facebook)
        {
            this.Name = Name;
            this.Image = Image;
            this.Info = Info;
            this.Location = Location;
            this.OwnSiteUrl = OwnSiteUrl;
            this.ContactPhone = ContactPhone;
            this.ContactEmail = ContactEmail;
            this.Twitter = Twitter;
            this.Linkedin = Linkedin;
            this.Facebook = Facebook;
        }
    }
}