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
    public class SeminarsController : Controller
    {
        private const int DefaultPageSize = 10;
        private Model1 db = new Model1();

        // GET: Seminars
        public ActionResult SeminarDetail(int? page)
        {
            //廣告(新聞區)
            ViewBag.AdNews = db.Ads.Where(x => x.AdStatus == Status.發行).Where(x => x.AdCategory == AdCategory.小圖示廣告).OrderBy(x => Guid.NewGuid()).Take(3).ToList();

            //研討會(新聞區)
            ViewBag.SeminarsNews = db.Seminars.OrderBy(x => x.IsTop).ThenByDescending(x => x.SDate).Take(3).ToList();

            //教育訓練(新聞區)
            ViewBag.TrainingCourseNews = db.TrainingCourses.Where(x => x.Status == Status.發行)
                .OrderByDescending(x => x.SignUpSDate).Take(3).ToList();

            string selectInput = Session["selectInput"]?.ToString(); // ==> Session["selectInput"] == null ? null : Session["selectInput"].ToString()
            string year = Session["year"] == null ? null : Session["year"].ToString();
            var userPage = page - 1 ?? 0;
            var user = db.Seminars.OrderByDescending(x => x.SDate).AsQueryable();
            if (!string.IsNullOrEmpty(selectInput))
            {
                user = user.Where(x => x.Title.Contains(selectInput) || x.Count.Contains(selectInput));
            }

            if (!string.IsNullOrEmpty(year))
            {
                int intYear = Convert.ToInt32(year);
                user = user.Where(x => x.SDate.Year == intYear);
            }

            return View(user.ToPagedList(userPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult SeminarDetail(string selectInput, string year)
        {
            Session["year"] = year;
            Session["selectInput"] = selectInput;
            return RedirectToAction("News", "News");
        }

        // GET: Seminars/sem_page/5
        public ActionResult sem_page(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seminar seminar = db.Seminars.Find(id);
            if (seminar == null)
            {
                return HttpNotFound();
            }

            //廣告(新聞區)
            ViewBag.AdNews = db.Ads.Where(x => x.AdStatus == Status.發行).Where(x => x.AdCategory == AdCategory.小圖示廣告).OrderBy(x => Guid.NewGuid()).Take(3).ToList();

            //研討會(新聞區)
            ViewBag.SeminarsNews = db.Seminars.OrderBy(x => x.IsTop).ThenByDescending(x => x.SDate).Take(3).ToList();

            //教育訓練(新聞區)
            ViewBag.TrainingCourseNews = db.TrainingCourses.Where(x => x.Status == Status.發行)
                .OrderByDescending(x => x.SignUpSDate).Take(3).ToList();

            seminar.Clicks += 1;
            db.Entry(seminar).State = EntityState.Modified;
            db.SaveChanges();

            return View(seminar);
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
