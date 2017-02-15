using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ETS.DAL.Database;
using WebSite.Models;
using System.Web.Security;
using ETS.Contracts.DataContracts;
using ETS.DAL;

namespace WebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new DatabaseInitializer());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        //let us take out the username now                
                        string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                        string roles = string.Empty;
                        DatabaseContext db = new DatabaseContext();
                        //AccountsInMemoryRepository acc = new AccountsInMemoryRepository();
                        //var db = acc.GetAll();
                        AccountEntity user = db.Accounts.SingleOrDefault(u => u.Login == username);
                        //var user = db.SingleOrDefault(u => u.Email == username);
                        roles = user.AccessLevel.ToString();
                        //let us extract the roles from our own custom cookie


                        //Let us set the Pricipal with our user specific details
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                          new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split(';'));
                    }
                    catch (Exception)
                    {
                        //somehting went wrong
                    }
                }
            }
        }
    }
}
