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
    public class ActivityPhotoesController : Controller
    {
        private Model1 db = new Model1();

        public ActionResult CreTest()
        {
            return View();
        }

        // GET: Admin/ActivityPhotoes
        public ActionResult Index()
        {
            var activityPhotos = db.ActivityPhotos.Include(a => a.ActivityRecord);
            return View(activityPhotos.ToList());
        }

        // GET: Admin/ActivityPhotoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityPhoto activityPhoto = db.ActivityPhotos.Find(id);
            if (activityPhoto == null)
            {
                return HttpNotFound();
            }
            return View(activityPhoto);
        }

        // GET: Admin/ActivityPhotoes/Create
        public ActionResult Create()
        {
            ViewBag.ActivityId = new SelectList(db.ActivityRecords, "Id", "Title");
            return View();
        }

        // POST: Admin/ActivityPhotoes/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ActivityId,Photo,PhotoAnnotation,DateTime")] ActivityPhoto activityPhoto)
        {
            if (ModelState.IsValid)
            {
                db.ActivityPhotos.Add(activityPhoto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityId = new SelectList(db.ActivityRecords, "Id", "Title", activityPhoto.ActivityId);
            return View(activityPhoto);
        }

        // GET: Admin/ActivityPhotoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityPhoto activityPhoto = db.ActivityPhotos.Find(id);
            if (activityPhoto == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityId = new SelectList(db.ActivityRecords, "Id", "Title", activityPhoto.ActivityId);
            return View(activityPhoto);
        }

        // POST: Admin/ActivityPhotoes/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ActivityId,Photo,PhotoAnnotation,DateTime")] ActivityPhoto activityPhoto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityPhoto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityId = new SelectList(db.ActivityRecords, "Id", "Title", activityPhoto.ActivityId);
            return View(activityPhoto);
        }

        // GET: Admin/ActivityPhotoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityPhoto activityPhoto = db.ActivityPhotos.Find(id);
            if (activityPhoto == null)
            {
                return HttpNotFound();
            }
            return View(activityPhoto);
        }

        // POST: Admin/ActivityPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityPhoto activityPhoto = db.ActivityPhotos.Find(id);
            db.ActivityPhotos.Remove(activityPhoto);
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
