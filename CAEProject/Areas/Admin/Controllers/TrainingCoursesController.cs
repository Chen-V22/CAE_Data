using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAEProject.Models;

namespace CAEProject.Areas.Admin.Controllers
{
    public class TrainingCoursesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/TrainingCourses
        public ActionResult Index()
        {
            var trainingCourses = db.TrainingCourses.Include(t => t.User);
            ViewBag.Rm = db.RegistrationMinors.ToList();
            //EventLog eventLog = new EventLog();
            //string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            //ViewBag.ip = ip;
            return View(trainingCourses.ToList());
        }

        // GET: Admin/TrainingCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingCourse trainingCourse = db.TrainingCourses.Find(id);
            if (trainingCourse == null)
            {
                return HttpNotFound();
            }
            return View(trainingCourse);
        }

        // GET: Admin/TrainingCourses/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserCodeName");
            return View();
        }

        // POST: Admin/TrainingCourses/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrainingCourse trainingCourse)
        {
            if (ModelState.IsValid)
            {
                trainingCourse.AddUser = Utility.GetUserTickets().UserCodeName;
                trainingCourse.DateTime = DateTime.Now;
                trainingCourse.LastEditDateTime = DateTime.Now;
                db.TrainingCourses.Add(trainingCourse);
                db.SaveChanges();
                Session["TcId"] = trainingCourse.Id;
                return RedirectToAction("Create","RegistrationMinors",new {trainingCourse.Id});
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "UserCodeName", trainingCourse.UserId);
            return View(trainingCourse);
        }

        // GET: Admin/TrainingCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingCourse trainingCourse = db.TrainingCourses.Find(id);
            if (trainingCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserCodeName", trainingCourse.UserId);
            return View(trainingCourse);
        }

        // POST: Admin/TrainingCourses/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Status,SeminarStatus,Cost,UserId,ContactPerson,ContactPhone,ContactEmail,SDate,EDate,SignUpSDate,SignUpEDate,Address,Quota,Alternate,Condition,Handle,Assisting,ProjectName,Count,File,Description,Success,AdImage,Clicks,AddUser,DateTime,EditUser,LastEditDateTime")] TrainingCourse trainingCourse)
        {
            if (ModelState.IsValid)
            {
                trainingCourse.EditUser = Utility.GetUserTickets().UserCodeName;
                trainingCourse.LastEditDateTime = DateTime.Now;
                db.Entry(trainingCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserCodeName", trainingCourse.UserId);
            return View(trainingCourse);
        }

        // GET: Admin/TrainingCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            TrainingCourse trainingCourse = db.TrainingCourses.Find(id);
            db.TrainingCourses.Remove(trainingCourse);
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
