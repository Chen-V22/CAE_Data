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
    public class RegistrationMinorsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/RegistrationMinors/Create
        public ActionResult Create(int? id,int? tab)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "TrainingCourses");
            }
            TempData["tab"] = tab ?? 0;
            ViewBag.RmIndex = db.RegistrationMinors.Where(x => x.TrainingCourseId == id).ToList();
            ViewBag.TrainingCourseId = new SelectList(db.TrainingCourses, "Id", "Title");
            return View();
        }

        // POST: Admin/RegistrationMinors/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TrainingCourseId,Sort,Name,Length,IsRequired")] RegistrationMinor registrationMinor, int? id)
        {
            if (ModelState.IsValid)
            {
                if (id != null)
                {
                    registrationMinor.TrainingCourseId = (int) id;
                    db.RegistrationMinors.Add(registrationMinor);
                    db.SaveChanges();
                    TempData["RcMessage"] = "已加入欄位";
                    return RedirectToAction("Create", "RegistrationMinors", new {area = "Admin", id = id, tab = 1});
                }
            }

            ViewBag.TrainingCourseId = new SelectList(db.TrainingCourses, "Id", "Title", registrationMinor.TrainingCourseId);
            return View(registrationMinor);
        }

        // GET: Admin/RegistrationMinors/Delete/5
        public ActionResult Delete(int? id)
        {
            RegistrationMinor registrationMinor = db.RegistrationMinors.Find(id);
            db.RegistrationMinors.Remove(registrationMinor);
            db.SaveChanges();
            return RedirectToAction("Create", "RegistrationMinors", new { area = "Admin", id = id, tab = 2 });
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
