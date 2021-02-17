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

namespace CAEProject.Areas.Admin.Controllers
{
    [Premission]
    public class SitesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/Sites
        public ActionResult Index()
        {
            return View(db.Sites.ToList());
        }

        // GET: Admin/Sites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site site = db.Sites.Find(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        // GET: Admin/Sites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sites/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodeName,Name,AddUser,DateTime,EditUser,LastEditDateTime")] Site site)
        {
            if (ModelState.IsValid)
            {
                site.AddUser = Utility.GetUserTickets().UserCodeName;
                site.DateTime = DateTime.Now;
                site.LastEditDateTime = DateTime.Now;
                db.Sites.Add(site);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(site);
        }

        // GET: Admin/Sites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site site = db.Sites.Find(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        // POST: Admin/Sites/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodeName,Name,AddUser,DateTime,EditUser,LastEditDateTime")] Site site)
        {
            if (ModelState.IsValid)
            {
                site.EditUser = Utility.GetUserTickets().UserCodeName;
                site.LastEditDateTime = DateTime.Now;
                db.Entry(site).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(site);
        }

        // GET: Admin/Sites/Delete/5
        public ActionResult Delete(int? id)
        {
            Site site = db.Sites.Find(id);
            db.Sites.Remove(site);
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
