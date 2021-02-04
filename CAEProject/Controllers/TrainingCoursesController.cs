using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAEProject.Models;
using MvcPaging;

namespace CAEProject.Controllers
{
    public class TrainingCoursesController : Controller
    {
        private const int DefaultPageSize = 10;
        private Model1 db = new Model1();

        // GET: TrainingCourses/Index(教育訓練-列表)
        public ActionResult Index(int? page)
        {
            string selectInput = Session["selectInput"]?.ToString(); // ==> Session["selectInput"] == null ? null : Session["selectInput"].ToString()
            string year = Session["year"] == null ? null : Session["year"].ToString();
            var userPage = page - 1 ?? 0;
            var user = db.TrainingCourses.OrderByDescending(x => x.SignUpSDate).AsQueryable();
            if (!string.IsNullOrEmpty(selectInput))
            {
                user = user.Where(x => x.Title.Contains(selectInput));
            }
            if (!string.IsNullOrEmpty(year))
            {
                int intDate = Convert.ToInt32(year);
                user = user.Where(x => x.SignUpSDate.Year == intDate);
            }

            return View(user.ToPagedList(userPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(string year, string selectInput)
        {
            Session["selectInput"] = selectInput;
            Session["year"] = year;
            return RedirectToAction("Index", "TrainingCourses",new {area=""});
        }

        // GET: TrainingCourses/Details/5(教育訓練-詳細資訊)
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

        // GET: TrainingCourses/GeneralApply(非會員報名)
        public ActionResult GeneralApply()
        {
            if (Session["member"]==null)
            {
                TempData["memberError"] = "請先登入會員";
                return RedirectToAction("Login", "Members");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserCodeName");
            return View();
        }

        // GET: TrainingCourses/MemberApply(會員報名)
        public ActionResult MemberApply()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserCodeName");
            return View();
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
