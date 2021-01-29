using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAEProject.Models;

namespace CAEProject.Controllers
{
    public class MembersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Members/Right
        public ActionResult Right()
        {
            return View();
        }

        // GET: Members/Right
        public ActionResult Register()
        {
            return View();
        }

        // POST: Members/Right
        [HttpPost]
        public ActionResult Register(string memberType)
        {
            if (memberType == "0" || memberType == "1")
            {
                return RedirectToAction("PaidCreate", "Members", new { area = "", Type = memberType });
            }
            else if (memberType == "2")
            {
                return RedirectToAction("GeneralCreate", "Members", new { area = "", Type = memberType });
            }
            else
            {
                return RedirectToAction("StudentCreate", "Members", new { area = "", Type = memberType });
            }
        }

        // GET: Members/GeneralCreate
        public ActionResult GeneralCreate()
        {
            return View();
        }

        // POST: Members/GeneralCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GeneralCreate([Bind(Include = "Id,MemberLevel,DateTime,Account,Password,ApplicationStatus,ApproveDateTime,DisenableDateTime,NoticeDateTime,ContactPerson,ContactPersonPhone,Address,Extension,Fax,CurrentIdentity,CurrentUnit,JobTitle,MobilePhone,IdCard,Email,Phone,BusinessItem,Demand,Subscription,EditUser,LastEditDateTime")] MbFreeViewModel mbFreeViewModel, MemberLevel applicationStatus)
        {
            if (ModelState.IsValid)
            {
                Member member = new Member();
                member.MemberLevel = applicationStatus;
                member.Account = mbFreeViewModel.Account;
                member.PasswordSalt = Utility.CreateSalt();
                member.Password = Utility.GenerateHashWithSalt(mbFreeViewModel.Password, member.PasswordSalt);
                member.NoticeDateTime = mbFreeViewModel.NoticeDateTime;
                member.CurrentIdentity = mbFreeViewModel.CurrentIdentity;
                member.CurrentUnit = mbFreeViewModel.CurrentUnit;
                member.JobTitle = mbFreeViewModel.JobTitle;
                member.MobilePhone = mbFreeViewModel.MobilePhone;
                member.IdCard = mbFreeViewModel.IdCard;
                member.Email = mbFreeViewModel.Email;
                member.Phone = mbFreeViewModel.Phone;
                member.BusinessItem = mbFreeViewModel.BusinessItem;
                member.ContactPerson = mbFreeViewModel.ContactPerson;
                member.ContactPersonPhone = mbFreeViewModel.ContactPersonPhone;
                member.Address = mbFreeViewModel.Address;
                member.Extension = Convert.ToInt32(mbFreeViewModel.Extension);
                member.Fax = mbFreeViewModel.Fax;
                member.Demand = mbFreeViewModel.Demand;
                member.Subscription = mbFreeViewModel.Subscription;
                member.DateTime = DateTime.Now;
                member.LastEditDateTime = DateTime.Now;
                member.EditUser = mbFreeViewModel.Account;

                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(mbFreeViewModel);
        }

        // GET: Members/StudentCreate
        public ActionResult StudentCreate()
        {
            return View();
        }

        // POST: Members/StudentCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StudentCreate([Bind(Include = "Id,MemberLevel,DateTime,Account,Password,ApplicationStatus,ApproveDateTime,DisenableDateTime,NoticeDateTime,ContactPerson,ContactPersonPhone,Address,Extension,Fax,CurrentIdentity,CurrentUnit,JobTitle,MobilePhone,IdCard,Email,Phone,BusinessItem,Demand,Subscription,EditUser,LastEditDateTime")] MbFreeViewModel mbFreeViewModel, MemberLevel applicationStatus)
        {
            if (ModelState.IsValid)
            {
                Member member = new Member();
                member.MemberLevel = applicationStatus;
                member.Account = mbFreeViewModel.Account;
                member.PasswordSalt = Utility.CreateSalt();
                member.Password = Utility.GenerateHashWithSalt(mbFreeViewModel.Password, member.PasswordSalt);
                member.NoticeDateTime = mbFreeViewModel.NoticeDateTime;
                member.CurrentIdentity = mbFreeViewModel.CurrentIdentity;
                member.CurrentUnit = mbFreeViewModel.CurrentUnit;
                member.JobTitle = mbFreeViewModel.JobTitle;
                member.MobilePhone = mbFreeViewModel.MobilePhone;
                member.IdCard = mbFreeViewModel.IdCard;
                member.Email = mbFreeViewModel.Email;
                member.Phone = mbFreeViewModel.Phone;
                member.BusinessItem = mbFreeViewModel.BusinessItem;
                member.ContactPerson = mbFreeViewModel.ContactPerson;
                member.ContactPersonPhone = mbFreeViewModel.ContactPersonPhone;
                member.Address = mbFreeViewModel.Address;
                member.Extension = Convert.ToInt32(mbFreeViewModel.Extension);
                member.Fax = mbFreeViewModel.Fax;
                member.Demand = mbFreeViewModel.Demand;
                member.Subscription = mbFreeViewModel.Subscription;
                member.DateTime = DateTime.Now;
                member.LastEditDateTime = DateTime.Now;
                member.EditUser = mbFreeViewModel.Account;

                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(mbFreeViewModel);
        }

        //============鑽石、白金============

        // GET: Members/PaidCreate
        public ActionResult PaidCreate()
        {
            return View();
        }

        // POST: Admin/MbPaidViewModels/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaidCreate([Bind(Include = "Id,DateTime,Account,Password,CompanyName,CompanyNumber,Principal,PrincipalJobTitle,CompanyPhone,CompanyUrl,ContactPersonJobTitle,EmployeeCount,CompanyType,Industry,Training,CompanyIntroduction,Business,CompanyPhoto,ContactPerson,ContactPersonPhone,ContactPersonEmail,Address,Extension,Fax,Demand,Subscription,EditUser,LastEditDateTime")] MbPaidViewModel mbPaidViewModel, HttpPostedFileBase photo, MemberLevel applicationType)
        {
            if (ModelState.IsValid)
            {
                Member member = new Member();
                member.MemberLevel = applicationType;
                member.ApplicationStatus = ApplicationStatus.審核中;
                member.Account = mbPaidViewModel.Account;
                member.PasswordSalt = Utility.CreateSalt();
                member.Password = Utility.GenerateHashWithSalt(mbPaidViewModel.Password, member.PasswordSalt);
                member.CompanyName = mbPaidViewModel.CompanyName;
                member.CompanyNumber = mbPaidViewModel.CompanyNumber;
                member.Principal = mbPaidViewModel.Principal;
                member.PrincipalJobTitle = mbPaidViewModel.PrincipalJobTitle;
                member.CompanyPhone = mbPaidViewModel.CompanyPhone;
                member.CompanyUrl = mbPaidViewModel.CompanyUrl;
                member.ContactPersonJobTitle = mbPaidViewModel.ContactPersonJobTitle;
                member.EmployeeCount = Convert.ToInt32(mbPaidViewModel.EmployeeCount);
                member.ContactPersonEmail = mbPaidViewModel.ContactPersonEmail;
                member.CompanyType = mbPaidViewModel.CompanyType;
                member.Industry = mbPaidViewModel.Industry;
                member.Training = mbPaidViewModel.Training;
                member.CompanyIntroduction = mbPaidViewModel.CompanyIntroduction;
                member.Business = mbPaidViewModel.Business;
                member.CompanyPhone = mbPaidViewModel.CompanyPhone;
                member.ContactPerson = mbPaidViewModel.ContactPerson;
                member.ContactPersonPhone = mbPaidViewModel.ContactPersonPhone;
                member.Address = mbPaidViewModel.Address;
                member.Extension = Convert.ToInt32(mbPaidViewModel.Extension);
                member.Fax = mbPaidViewModel.Fax;
                member.Demand = mbPaidViewModel.Demand;
                member.Subscription = mbPaidViewModel.Subscription;
                member.DateTime = DateTime.Now;
                member.LastEditDateTime = DateTime.Now;
                member.EditUser = mbPaidViewModel.Account;

                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(mbPaidViewModel);
        }

        //===============================

        // GET: Members/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Members/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login([Bind(Include = "UserCodeName,Password")] LogInViewModel logInViewModel)
        {
            if (ModelState.IsValid)
            {
                var Account = db.Members.FirstOrDefault(x => x.Account == logInViewModel.UserCodeName);
                if (Account == null)
                {
                    TempData["Error"] = "登入失敗";
                    return View("Login");
                }
                string Password = Utility.GenerateHashWithSalt(logInViewModel.Password, Account.PasswordSalt);
                if (Account.Password != Password)
                {
                    TempData["Error"] = "登入失敗";
                    return View("Login");
                }
                var userData = Account;
                Session["member"] = Account;
                return RedirectToAction("Index", "Home");
            }
            return View(logInViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult HomeLogin(string account, string password)
        {
            if (!string.IsNullOrEmpty(account)&&!string.IsNullOrEmpty(password))
            {
                var Account = db.Members.FirstOrDefault(x => x.Account == account);
                if (Account == null)
                {
                    TempData["Error"] = "登入失敗";
                    return View("Login");
                }
                string Password = Utility.GenerateHashWithSalt(password, Account.PasswordSalt);
                if (Account.Password != Password)
                {
                    TempData["Error"] = "登入失敗";
                    return View("Login");
                }
                var userData = Account;
                Session["member"] = Account;
                return RedirectToAction("Index", "Home");
            }

            TempData["homeError"] = "帳號或密碼錯誤，登入失敗";
            return RedirectToAction("Index", "Home");

        }

        public ActionResult Logoff()
        {
            Session["member"] = null;
            return RedirectToAction("Index", "Home");
        }

        // GET: Members/Forget
        public ActionResult Forget()
        {
            return View();
        }

        // POST: Members/Forget
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forget(string account, string email)
        {
            var Account = db.Members.FirstOrDefault(x => x.Account == account);
            if (Account == null)
            {
                TempData["ForgetError"] = "驗證失敗";
                return View("Forget");
            }
            if (Account.Email != email || Account.CompanyUrl != email)
            {
                TempData["ForgetError"] = "驗證失敗";
                return View("Forget");
            }

            TempData["ForgetError"] = "驗證成功";
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
