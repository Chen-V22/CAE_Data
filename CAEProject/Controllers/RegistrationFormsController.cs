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
    public class RegistrationFormsController : Controller
    {
        private Model1 db = new Model1();

        // GET: RegistrationForms/Details/5
        public ActionResult Details(int? id,int? trainingId)//id = 報名人id ,trainingId = 課程id
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistrationForm registrationForm = db.RegistrationForms.Find(id);
            ViewBag.TrainingCourses = db.TrainingCourses.Find(trainingId);
            if (registrationForm == null)
            {
                return HttpNotFound();
            }
            return View(registrationForm);
        }

        // GET: RegistrationForms/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.TrainingCourses = db.TrainingCourses.Find(id);
            ViewBag.TrainingId = new SelectList(db.TrainingCourses, "Id", "Title");
            return View();
        }

        // POST: RegistrationForms/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TrainingId,MemberName,Company,CompanyNumber,Name,Email,Department,JobTitle,Cellphone,Phone,Address,Remarks,GenderType,UniformInvoice,AddData,DateTime")] RegistrationForm registrationForm)
        {
            if (ModelState.IsValid)
            {
                registrationForm.DateTime = DateTime.Now;
                
                db.RegistrationForms.Add(registrationForm);
                db.SaveChanges();
                return RedirectToAction("Details",new {id=registrationForm.Id, trainingId=registrationForm.TrainingId });
            }

            ViewBag.TrainingId = new SelectList(db.TrainingCourses, "Id", "Title", registrationForm.TrainingId);
            return View(registrationForm);
        }

        // GET: RegistrationForms/Edit/5
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

        // POST: RegistrationForms/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TrainingId,MemberName,Company,CompanyNumber,Name,Email,Department,JobTitle,Cellphone,Phone,Address,Remarks,GenderType,UniformInvoice,AddData,DateTime")] RegistrationForm registrationForm)
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
