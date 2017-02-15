using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class AccountModel
    {
        public int AccountId { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int AccessLevel { get; set; }
    }
}