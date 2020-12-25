using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CAEProject.Models;

namespace CAEProject.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();
        public ActionResult Index()
        {
            //TOP廣告
            ViewBag.AdTop = db.Ads.Where(x => x.AdStatus == Status.發行).OrderBy(x => x.IsTop == IsTop.是).ToList();

            //最新快訊
            ViewBag.Newses = db.News.OrderBy(x => x.IsTop).ThenByDescending(x=>x.SDate).Take(4).ToList();

            //研討會
            ViewBag.Seminars = db.Seminars.OrderBy(x => x.IsTop).ThenByDescending(x => x.SDate).Take(4).ToList();

            //電子報
            ViewBag.Newsletters = db.Newsletters.OrderByDescending(x => x.Date).ToList();

            //技術新知
            ViewBag.TechnicalKnowledge = db.TechnicalKnowledges.Where(x=>x.AdStatus== Status.發行).OrderBy(x => x.IsTop).ThenByDescending(x => x.SDate).ToList();
            
            //教育訓練
            ViewBag.TrainingCourse = db.TrainingCourses.Where(x => x.Status == Status.發行)
                .OrderByDescending(x => x.SignUpSDate).ToList();

            //活動集錦
            ViewBag.ActivityRecord = db.ActivityRecords.Where(x => x.ActivityStatus == Status.發行)
                .OrderByDescending(x => x.SDate).Take(10).ToList();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}