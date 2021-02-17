using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CAEProject.Areas.Admin.Filters;
using CAEProject.Models;
using MvcPaging;
using Newtonsoft.Json;

namespace CAEProject.Areas.Admin.Controllers
{
    [Premission]
    public class UsersController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 15;
        // GET: Admin/Users
        public ActionResult Index(int? Page)
        {
            string Keyword = Session["Keyword"] == null ? null : Session["Keyword"].ToString();
            DateTime? strDate = Session["strDate"] == null ? null : (DateTime?)Session["strDate"];
            DateTime? endDate = Session["endDate"] == null ? null : (DateTime?)Session["endDate"];
            Session["Page"] = Page == null ? 1 : Page;
            int UserPage = Page.HasValue ? Page.Value - 1 : 0;
            var UserSelect = db.Users.OrderBy(x => x.Id).AsQueryable();

            if (!string.IsNullOrEmpty(Keyword))
            {
                UserSelect = UserSelect.Where(x => x.UserCodeName.Contains(Keyword) || x.UserName.Contains(Keyword));
            }

            if (strDate.HasValue && endDate.HasValue)
            {
                endDate = endDate.Value.AddDays(1);
                UserSelect = UserSelect.Where(x =>
                    x.LastEditDateTime >= strDate && x.LastEditDateTime <= endDate);
            }

            return View(UserSelect.ToPagedList(UserPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(string Keyword, DateTime? strDate, DateTime? endDate)
        {
            Session["Keyword"] = Keyword;
            Session["strDate"] = strDate;
            Session["endDate"] = endDate;
            return RedirectToAction("Index");
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName");
            return View();
        }

        // POST: Admin/Users/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserCodeName,UserName,Email,Password,PasswordSalt,NumberOfLogins,RoleId,DateTime,LastEditor,LastEditDateTime")] User user)
        {
            if (ModelState.IsValid)
            {
                user.PasswordSalt = Utility.CreateSalt();
                user.Password = Utility.GenerateHashWithSalt(user.Password, user.PasswordSalt);
                user.LastEditor = Utility.GetUserTickets().UserCodeName;
                user.DateTime = DateTime.Now;
                user.LastEditDateTime = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName");
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserCodeName,UserName,Email,Password,PasswordSalt,NumberOfLogins,RoleId,DateTime,LastEditor,LastEditDateTime")] User user, string passwordNew)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(passwordNew))
                {
                    user.PasswordSalt = Utility.CreateSalt();
                    user.Password = Utility.GenerateHashWithSalt(passwordNew, user.PasswordSalt);
                }
                user.LastEditDateTime = DateTime.Now;
                User Users = Utility.GetUserTickets();
                user.LastEditor = Users.UserCodeName;
                user.LastEditDateTime = DateTime.Now;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        // POST: Admin/Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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
