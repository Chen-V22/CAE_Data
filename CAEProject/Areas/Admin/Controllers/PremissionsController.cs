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
    public class PremissionsController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 10;
        
        // GET: Admin/Premission
        public ActionResult Index(int? page)
        {
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var user = db.Premissions.OrderBy(x => x.Id).AsQueryable();
            return View(user.ToPagedList(UserPage, DefaultPageSize));
        }

        // GET: Admin/Premissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premission premission = db.Premissions.Find(id);
            if (premission == null)
            {
                return HttpNotFound();
            }
            return View(premission);
        }

        // GET: Admin/Premissions/Create
        public ActionResult Create()
        {
            ViewBag.pid = new SelectList(db.Premissions.Where(x=>x.pid==null), "Id", "Name");
            return View();
        }

        // POST: Admin/Premissions/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,pid,PValue,Url")] Premission premission)
        {
            if (ModelState.IsValid)
            {
                db.Premissions.Add(premission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pid = new SelectList(db.Premissions, "Id", "Name", premission.pid);
            return View(premission);
        }

        // GET: Admin/Premissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premission premission = db.Premissions.Find(id);
            if (premission == null)
            {
                return HttpNotFound();
            }
            ViewBag.pid = new SelectList(db.Premissions, "Id", "Name", premission.pid);
            return View(premission);
        }

        // POST: Admin/Premissions/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,pid,PValue,Url")] Premission premission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(premission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pid = new SelectList(db.Premissions, "Id", "Name", premission.pid);
            return View(premission);
        }

        // GET: Admin/Premissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premission premission = db.Premissions.Find(id);
            if (premission == null)
            {
                return HttpNotFound();
            }
            return View(premission);
        }

        // POST: Admin/Premissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Premission premission = db.Premissions.Find(id);
            db.Premissions.Remove(premission);
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
