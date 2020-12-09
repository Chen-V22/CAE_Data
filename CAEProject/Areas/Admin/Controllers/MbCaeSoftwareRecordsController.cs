using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAEProject.Models;

namespace CAEProject.Areas.Admin.Controllers
{
    public class MbCaeSoftwareRecordsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/MbCaeSoftwareRecords
        public ActionResult Index()
        {
            var mbCaeSoftwareRecords = db.MbCaeSoftwareRecords.Include(m => m.Member);
            return View(mbCaeSoftwareRecords.ToList());
        }

        // GET: Admin/MbCaeSoftwareRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MbCaeSoftwareRecord mbCaeSoftwareRecord = db.MbCaeSoftwareRecords.Find(id);
            if (mbCaeSoftwareRecord == null)
            {
                return HttpNotFound();
            }
            return View(mbCaeSoftwareRecord);
        }

        // GET: Admin/MbCaeSoftwareRecords/Create
        public ActionResult Create()
        {
            ViewBag.MbId = new SelectList(db.Members, "Id", "Account");
            return View();
        }

        // POST: Admin/MbCaeSoftwareRecords/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MbId,Name,SoftwareType,Status,Remarks,ReservationDateTime")] MbCaeSoftwareRecord mbCaeSoftwareRecord)
        {
            if (ModelState.IsValid)
            {
                db.MbCaeSoftwareRecords.Add(mbCaeSoftwareRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MbId = new SelectList(db.Members, "Id", "Account", mbCaeSoftwareRecord.MbId);
            return View(mbCaeSoftwareRecord);
        }

        // GET: Admin/MbCaeSoftwareRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MbCaeSoftwareRecord mbCaeSoftwareRecord = db.MbCaeSoftwareRecords.Find(id);
            if (mbCaeSoftwareRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.MbId = new SelectList(db.Members, "Id", "Account", mbCaeSoftwareRecord.MbId);
            return View(mbCaeSoftwareRecord);
        }

        // POST: Admin/MbCaeSoftwareRecords/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MbId,Name,SoftwareType,Status,Remarks,ReservationDateTime")] MbCaeSoftwareRecord mbCaeSoftwareRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mbCaeSoftwareRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MbId = new SelectList(db.Members, "Id", "Account", mbCaeSoftwareRecord.MbId);
            return View(mbCaeSoftwareRecord);
        }

        // GET: Admin/MbCaeSoftwareRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MbCaeSoftwareRecord mbCaeSoftwareRecord = db.MbCaeSoftwareRecords.Find(id);
            if (mbCaeSoftwareRecord == null)
            {
                return HttpNotFound();
            }
            return View(mbCaeSoftwareRecord);
        }

        // POST: Admin/MbCaeSoftwareRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MbCaeSoftwareRecord mbCaeSoftwareRecord = db.MbCaeSoftwareRecords.Find(id);
            db.MbCaeSoftwareRecords.Remove(mbCaeSoftwareRecord);
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
