using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using ETS.BLL.Infrastructure.Abstract;
using ETS.DAL;

namespace ETS.BLL.Infrastructure.Concrete
{
    public class FormAuthProvider : IAuthProvider
    {
        

        public bool Authenticate(string username, string password)
        {
            //bool result = FormsAuthentication.Authenticate(username, password);
            var res = new AccountsInMemoryRepository();
            if (res.Login(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return true;
            }
            return false;
        }

        public void DeAuthenticate()
        {
            FormsAuthentication.SignOut();
        }
    }
}
