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
    public class ResultsPublishedsController : Controller
    {
        private const int DefaultPageSize = 10;
        private Model1 db = new Model1();

        // GET: ResultsPublisheds
        public ActionResult Result(int? page)
        {

            string selectInput = Session["selectInput"]?.ToString(); // ==> Session["selectInput"] == null ? null : Session["selectInput"].ToString()
            string date = Session["date"] == null ? null : Session["date"].ToString();
            var userPage = page - 1 ?? 0;
            var user = db.ResultsPublisheds.OrderByDescending(x => x.ShowDate).AsQueryable();
            if (!string.IsNullOrEmpty(selectInput))
            {
                user = user.Where(x => x.Title.Contains(selectInput) || x.Count.Contains(selectInput));
            }

            if (!string.IsNullOrEmpty(date))
            {
                int intDate = Convert.ToInt32(date);
                user = user.Where(x => x.ShowDate.Year == intDate);
            }

            return View(user.ToPagedList(userPage, DefaultPageSize));
        }

        // GET: ResultsPublisheds/Details/5
        public ActionResult ResultDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultsPublished resultsPublished = db.ResultsPublisheds.Find(id);
            if (resultsPublished == null)
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

            return View(resultsPublished);
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
