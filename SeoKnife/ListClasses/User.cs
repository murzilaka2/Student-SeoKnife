using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeoKnife
{
    public class User
    {
        public string Admin{ get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Messages { get; set; }
        public int Views { get; set; }
        public int Reputation { get; set; }

        public User(string Admin, string Email, string Name, int Messages, int Views, int Reputation)
        {
            this.Admin = Admin;
            this.Email = Email;
            this.Name = Name;
            this.Messages = Messages;
            this.Views = Views;
            this.Reputation = Reputation;
        }
    }
}