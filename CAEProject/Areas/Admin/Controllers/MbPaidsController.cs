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
    public class MbPaidsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/MbPaids
        public ActionResult Index(int? id)
        {
            ViewBag.PaId = id;
            var mbPaids = db.MbPaids.Include(m => m.Member);
            ViewBag.MbPoint = db.MbPointses.Find(id);
            
            return View(mbPaids.ToList());
        }

        // GET: Admin/MbPaids/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MbPaid mbPaid = db.MbPaids.Find(id);
            if (mbPaid == null)
            {
                return HttpNotFound();
            }
            return View(mbPaid);
        }

        // GET: Admin/MbPaids/Create
        public ActionResult Create()
        {
            ViewBag.MbId = new SelectList(db.Members, "Id", "Account");
            return View();
        }

        // POST: Admin/MbPaids/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MbId,Paid,AddPoints,PaidDateTime,DateTime")] MbPaid mbPaid)
        {
            if (ModelState.IsValid)
            {
                db.MbPaids.Add(mbPaid);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MbId = new SelectList(db.Members, "Id", "Account", mbPaid.MbId);
            return View(mbPaid);
        }

        // GET: Admin/MbPaids/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MbPaid mbPaid = db.MbPaids.Find(id);
            if (mbPaid == null)
            {
                return HttpNotFound();
            }
            ViewBag.MbId = new SelectList(db.Members, "Id", "Account", mbPaid.MbId);
            return View(mbPaid);
        }

        // POST: Admin/MbPaids/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MbId,Paid,AddPoints,PaidDateTime,DateTime")] MbPaid mbPaid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mbPaid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MbId = new SelectList(db.Members, "Id", "Account", mbPaid.MbId);
            return View(mbPaid);
        }

        // GET: Admin/MbPaids/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MbPaid mbPaid = db.MbPaids.Find(id);
            if (mbPaid == null)
            {
                return HttpNotFound();
            }
            return View(mbPaid);
        }

        // POST: Admin/MbPaids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MbPaid mbPaid = db.MbPaids.Find(id);
            db.MbPaids.Remove(mbPaid);
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
