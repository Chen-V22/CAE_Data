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
    public class RegistrationFormsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/RegistrationForms
        public ActionResult Index(int? id)
        {
            var registrationForms = db.RegistrationForms.Include(r => r.TrainingCourse).Where(x=>x.TrainingId==id);
            return View(registrationForms.ToList());
        }

        // GET: Admin/RegistrationForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistrationForm registrationForm = db.RegistrationForms.Find(id);
            if (registrationForm == null)
            {
                return HttpNotFound();
            }
            return View(registrationForm);
        }

        // GET: Admin/RegistrationForms/Create  前台用
        //public ActionResult Create(int? id)
        //{
        //    var status = db.RegistrationForms.Where(x => x.TrainingId == id)
        //        .FirstOrDefault(x => x.MemberName == Session["Member"].ToString());
        //    if (status!=null)
        //    {
        //        TempData["Status"] = "你已報名本次課程";
        //        return View("Index");
        //    }
        //    ViewBag.TrainingId = new SelectList(db.TrainingCourses, "Id", "Title");
        //    return View();
        //}

        // POST: Admin/RegistrationForms/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,TrainingId,MemberName,Company,CompanyNumber,Name,Email,Department,JobTitle,Cellphone,Phone,Address,Remarks,GenderType,UniformInvoice,DateTime")] RegistrationForm registrationForm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        registrationForm.DateTime = DateTime.Now;
        //        db.RegistrationForms.Add(registrationForm);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.TrainingId = new SelectList(db.TrainingCourses, "Id", "Title", registrationForm.TrainingId);
        //    return View(registrationForm);
        //}

        // GET: Admin/RegistrationForms/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistrationForm registrationForm = db.RegistrationForms.Find(id);
            if (registrationForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrainingId = new SelectList(db.TrainingCourses, "Id", "Title", registrationForm.TrainingId);
            return View(registrationForm);
        }

        // POST: Admin/RegistrationForms/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TrainingId,MemberName,Company,CompanyNumber,Name,Email,Department,JobTitle,Cellphone,Phone,Address,Remarks,GenderType,UniformInvoice,DateTime")] RegistrationForm registrationForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registrationForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrainingId = new SelectList(db.TrainingCourses, "Id", "Title", registrationForm.TrainingId);
            return View(registrationForm);
        }

        // GET: Admin/RegistrationForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistrationForm registrationForm = db.RegistrationForms.Find(id);
            if (registrationForm == null)
            {
                return HttpNotFound();
            }
            return View(registrationForm);
        }

        // POST: Admin/RegistrationForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegistrationForm registrationForm = db.RegistrationForms.Find(id);
            db.RegistrationForms.Remove(registrationForm);
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
