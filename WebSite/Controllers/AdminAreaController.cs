using ETS.BLL;
using ETS.Contracts.DataContracts;
using ETS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{
    [Authorize(Roles = "1")]
    public class AdminAreaController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: AdminArea
        public async Task<ActionResult> Index()
        {
            IEnumerable<AccountEntity> accounts = await unitOfWork.AccountRepository.GetAll();

            IEnumerable<AccountModel> user = accounts.Select(accountEntity => new AccountModel
            {
                AccountId = accountEntity.AccountId,
                Name = accountEntity.Name,
                MiddleName = accountEntity.MiddleName,
                Surname = accountEntity.Surname,
                Email = accountEntity.Email,
                Login = accountEntity.Login,
                Password = accountEntity.Password,
                AccessLevel = accountEntity.AccessLevel,
            });
            return View(user);
        }

        public async Task<ViewResult> Edit(int id)
        {
            AccountEntity account = await unitOfWork.AccountRepository.GetByID(id);
            var model = new AccountModel
            {
                AccountId = account.AccountId,
                Name = account.Name,
                Surname = account.Surname,
                MiddleName = account.MiddleName,
                Email = account.Email,
                Login = account.Login,
                AccessLevel = account.AccessLevel
            };
            return View("Edit", model);
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> Update(AccountModel model)
        {
            AccountEntity account = await unitOfWork.AccountRepository.GetByID(model.AccountId);
            account.Name = model.Name;
            account.Surname = model.Surname;
            account.MiddleName = model.MiddleName;
            account.Email = model.Email;
            if (account.Login != model.Login)
            {
                account.Login = model.Login;
                Feature.SendEmail(account.Email, "Your credentials!",
                String.Format("Hello, {0} {1}! Your credentials have been changed.\nPlease, now use this credentials to sign in:\nLogin: {2}\nPassword: {3}\nPlease do not forget your password.",
                account.Name, account.Surname, account.Login, account.Password));
            }
            account.AccessLevel = model.AccessLevel;
            unitOfWork.AccountRepository.Update(account);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public RedirectToRouteResult Delete(int id)
        {
            unitOfWork.AccountRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public async Task<RedirectToRouteResult> Reassign(int id)
        {
            AccountEntity account = await unitOfWork.AccountRepository.GetByID(id);
            account.Password = Feature.SetPass(10);
            Feature.SendEmail(account.Email, "Your credentials!",
                String.Format("Hello, {0} {1}! Your credentials have been changed.\nPlease, now use this credentials to sign in:\nLogin: {2}\nPassword: {3}\nPlease do not forget your password.",
                account.Name, account.Surname, account.Login, account.Password));
            unitOfWork.AccountRepository.Update(account);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}