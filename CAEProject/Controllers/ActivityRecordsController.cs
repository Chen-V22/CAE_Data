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
    public class ActivityRecordsController : Controller
    {
        private Model1 db = new Model1();

        // GET: ActivityRecords
        public ActionResult PhotoGallery()
        {
            return View(db.ActivityRecords.ToList());
        }

        // GET: ActivityRecords/Edit/5
        public ActionResult PhotoDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityRecord activityRecord = db.ActivityRecords.Find(id);
            if (activityRecord == null)
            {
                return HttpNotFound();
            }

            //活動圖片
            ViewBag.activityPhoto = db.ActivityPhotos.Where(x => x.ActivityId == id).ToList();

            //點閱次數
            activityRecord.Clicks += 1;
            db.Entry(activityRecord).State = EntityState.Modified;
            db.SaveChanges();
            return View(activityRecord);
        }

        // POST: ActivityRecords/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PhotoDetail([Bind(Include = "Id,Title,ActivityStatus,PublishDateTime,Source,Clicks,Photo,IsTop,SDate,EDate,Count,AddUser,DateTime,EditUser,LastEditDateTime")] ActivityRecord activityRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activityRecord);
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
