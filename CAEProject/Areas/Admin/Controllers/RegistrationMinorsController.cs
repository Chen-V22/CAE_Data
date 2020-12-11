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
    public class RegistrationMinorsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/RegistrationMinors
        public ActionResult Index()
        {
            var registrationMinors = db.RegistrationMinors.Include(r => r.TrainingCourse);
            return View(registrationMinors.ToList());
        }

        // GET: Admin/RegistrationMinors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistrationMinor registrationMinor = db.RegistrationMinors.Find(id);
            if (registrationMinor == null)
            {
                return HttpNotFound();
            }
            return View(registrationMinor);
        }

        // GET: Admin/RegistrationMinors/Create
        public ActionResult Create()
        {
            ViewBag.TrainingCourseId = new SelectList(db.TrainingCourses, "Id", "Title");
            return View();
        }

        // POST: Admin/RegistrationMinors/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TrainingCourseId,Sort,Name,Length,IsRequired")] RegistrationMinor registrationMinor)
        {
            if (ModelState.IsValid)
            {
                db.RegistrationMinors.Add(registrationMinor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TrainingCourseId = new SelectList(db.TrainingCourses, "Id", "Title", registrationMinor.TrainingCourseId);
            return View(registrationMinor);
        }

        // GET: Admin/RegistrationMinors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistrationMinor registrationMinor = db.RegistrationMinors.Find(id);
            if (registrationMinor == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrainingCourseId = new SelectList(db.TrainingCourses, "Id", "Title", registrationMinor.TrainingCourseId);
            return View(registrationMinor);
        }

        // POST: Admin/RegistrationMinors/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TrainingCourseId,Sort,Name,Length,IsRequired")] RegistrationMinor registrationMinor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registrationMinor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrainingCourseId = new SelectList(db.TrainingCourses, "Id", "Title", registrationMinor.TrainingCourseId);
            return View(registrationMinor);
        }

        // GET: Admin/RegistrationMinors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistrationMinor registrationMinor = db.RegistrationMinors.Find(id);
            if (registrationMinor == null)
            {
                return HttpNotFound();
            }
            return View(registrationMinor);
        }

        // POST: Admin/RegistrationMinors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegistrationMinor registrationMinor = db.RegistrationMinors.Find(id);
            db.RegistrationMinors.Remove(registrationMinor);
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
