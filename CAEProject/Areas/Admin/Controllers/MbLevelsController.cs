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
    public class MbLevelsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/MbLevels
        public ActionResult Index()
        {
            return View(db.MbLevels.ToList());
        }

        // GET: Admin/MbLevels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MbLevel mbLevel = db.MbLevels.Find(id);
            if (mbLevel == null)
            {
                return HttpNotFound();
            }
            return View(mbLevel);
        }

        // GET: Admin/MbLevels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MbLevels/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Points,Welfare,AddUser,DateTime,EditUser,LastEditDateTime")] MbLevel mbLevel)
        {
            if (ModelState.IsValid)
            {
                mbLevel.AddUser = Utility.GetUserTickets().UserCodeName;
                mbLevel.DateTime = DateTime.Now;
                mbLevel.EditUser = Utility.GetUserTickets().UserCodeName;
                mbLevel.LastEditDateTime = DateTime.Now;
                db.MbLevels.Add(mbLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mbLevel);
        }

        // GET: Admin/MbLevels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MbLevel mbLevel = db.MbLevels.Find(id);
            if (mbLevel == null)
            {
                return HttpNotFound();
            }
            return View(mbLevel);
        }

        // POST: Admin/MbLevels/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Points,Welfare,AddUser,DateTime,EditUser,LastEditDateTime")] MbLevel mbLevel)
        {
            if (ModelState.IsValid)
            {
                mbLevel.EditUser = Utility.GetUserTickets().UserCodeName;
                mbLevel.LastEditDateTime = DateTime.Now;
                db.Entry(mbLevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mbLevel);
        }

        // GET: Admin/MbLevels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MbLevel mbLevel = db.MbLevels.Find(id);
            if (mbLevel == null)
            {
                return HttpNotFound();
            }
            return View(mbLevel);
        }

        // POST: Admin/MbLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MbLevel mbLevel = db.MbLevels.Find(id);
            db.MbLevels.Remove(mbLevel);
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
