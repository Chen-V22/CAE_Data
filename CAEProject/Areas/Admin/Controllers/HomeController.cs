using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CAEProject.Areas.Admin.Filters;
using CAEProject.Models;
using Newtonsoft.Json;

namespace CAEProject.Areas.Admin.Controllers
{
    
    public class HomeController : Controller
    {
        Model1 db = new Model1();
        // GET: Admin/Home
        [Premission]
        public ActionResult Index()
        {
            return View();
        }


        // GET: Admin/LogInViewModels/Create
        [AllowAnonymous]
        public ActionResult UserLogin()
        {
            return View();
        }

        // POST: Admin/LogInViewModels/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult UserLogin([Bind(Include = "UserCodeName,Password")] LogInViewModel logInViewModel)
        {
            if (ModelState.IsValid)
            {
                var Account = db.Users.FirstOrDefault(x => x.UserName == logInViewModel.UserCodeName);
                if (Account==null)
                {
                    TempData["Error"] = "登入失敗";
                    return View("UserLogin");
                }
                string Password = Utility.GenerateHashWithSalt(logInViewModel.Password, Account.PasswordSalt);
                if (Account.Password != Password)
                {
                    TempData["Error"] = "登入失敗";
                    return View("UserLogin");
                }
                Account.NumberOfLogins = Account.NumberOfLogins+1;
                db.Entry(Account).State = EntityState.Modified;
                db.SaveChanges();
                string userDate = JsonConvert.SerializeObject(Account);
                Utility.SetAuthenTicket(userDate,Account.UserCodeName);
                return RedirectToAction("Index", "Home");
            }
            return View(logInViewModel);
        }

        [Premission]
        [ValidateAntiForgeryToken]
        public ActionResult SingOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return Redirect(FormsAuthentication.LoginUrl);
        }

        [Premission]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transfer()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToActionPermanent("index", "Home", new { Area = "" });
        }
    }
}