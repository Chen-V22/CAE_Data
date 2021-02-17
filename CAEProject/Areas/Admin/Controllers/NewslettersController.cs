using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAEProject.Areas.Admin.Filters;
using CAEProject.Models;
using MvcPaging;

namespace CAEProject.Areas.Admin.Controllers
{
    [Premission]
    public class NewslettersController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 10;
        // GET: Admin/Newsletters
        public ActionResult Index(int? page)
        {
            string nlTitle = Session["nlTitle"] == null ? null : Session["nlTitle"].ToString();
            DateTime? nlStrDateTime =
                Session["nlStrDateTime"] == null ? null : (DateTime?)Session["nlStrDateTime"];
            DateTime? nlEndDateTime =
                Session["nlEndDateTime"] == null ? null : (DateTime?)Session["nlEndDateTime"];
            int userPage = page.HasValue ? page.Value - 1 : 0;
            var user = db.Newsletters.OrderBy(x => x.DateTime).AsQueryable();

            if (!string.IsNullOrEmpty(nlTitle))
            {
                user = user.Where(x => x.Title.Contains(nlTitle));
            }

            if (nlStrDateTime.HasValue && nlEndDateTime.HasValue)
            {
                nlEndDateTime = nlEndDateTime.Value.AddDays(1);
                user = user.Where(x => x.Date >= nlStrDateTime && x.Date <= nlEndDateTime);
            }
            return View(user.ToPagedList(userPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(string nlTitle, DateTime? nlStrDateTime,
            DateTime? nlEndDateTime)
        {
            Session["nlTitle"] = nlTitle;
            Session["nlStrDateTime"] = nlStrDateTime;
            Session["nlEndDateTime"] = nlEndDateTime;
            return RedirectToAction("Index");
        }
        // GET: Admin/Newsletters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newsletter newsletter = db.Newsletters.Find(id);
            if (newsletter == null)
            {
                return HttpNotFound();
            }
            return View(newsletter);
        }

        // GET: Admin/Newsletters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Newsletters/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Num,Date,File,AddUser,DateTime,EditUser,LastEditDateTime")] Newsletter newsletter, HttpPostedFileBase upfile)
        {
            if (ModelState.IsValid)
            {
                if (upfile != null)
                {
                    newsletter.File = Utility.SaveUpFile(upfile);
                }

                newsletter.DateTime = DateTime.Now;
                newsletter.AddUser = Utility.GetUserTickets().UserCodeName;
                newsletter.LastEditDateTime = DateTime.Now;
                db.Newsletters.Add(newsletter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newsletter);
        }

        // GET: Admin/Newsletters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newsletter newsletter = db.Newsletters.Find(id);
            if (newsletter == null)
            {
                return HttpNotFound();
            }
            return View(newsletter);
        }

        // POST: Admin/Newsletters/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Num,Date,File,AddUser,DateTime,EditUser,LastEditDateTime")] Newsletter newsletter, HttpPostedFileBase upfile)
        {
            if (ModelState.IsValid)
            {
                if (upfile != null)
                {
                    newsletter.File = Utility.SaveUpFile(upfile);
                }
                newsletter.EditUser = Utility.GetUserTickets().UserCodeName;
                newsletter.LastEditDateTime = DateTime.Now;
                db.Entry(newsletter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsletter);
        }

        // GET: Admin/Newsletters/Delete/5
        public ActionResult Delete(int? id)
        {
            Newsletter newsletter = db.Newsletters.Find(id);
            db.Newsletters.Remove(newsletter);
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
