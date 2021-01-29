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
    public class TechnicalKnowledgesController : Controller
    {
        private const int DefaultPageSize = 4;
        private Model1 db = new Model1();

        // GET: TechnicalKnowledges
        public ActionResult TechDetail(int? page)
        {
            string selectInput = Session["TkSelectInput"]?.ToString(); // ==> Session["selectInput"] == null ? null : Session["selectInput"].ToString()
            string year = Session["TkYear"] == null ? null : Session["TkYear"].ToString();
            string month = Session["TkMonth"] == null ? null : Session["TkMonth"].ToString();
            IndustryCategory? industryCategory = (IndustryCategory?) Session["TkIndustryCategory"];
            var userPage = page - 1 ?? 0;
            var user = db.TechnicalKnowledges.OrderByDescending(x => x.SDate).AsQueryable();
            
            if (!string.IsNullOrEmpty(selectInput))
            {
                user = user.Where(x => x.Title.Contains(selectInput) || x.Count.Contains(selectInput));
            }

            if (!string.IsNullOrEmpty(year))
            {
                int intYear = Convert.ToInt32(year);
                user = user.Where(x => x.SDate.Year == intYear);
            }

            if (!string.IsNullOrEmpty(month))
            {
                int intMonth = Convert.ToInt32(month);
                user = user.Where(x => x.SDate.Month == Convert.ToInt32(intMonth));
            }

            if (industryCategory.HasValue)
            {
                user = user.Where(x => x.IndustryCategory == industryCategory);
            }

            return View(user.ToPagedList(userPage, DefaultPageSize));
        }


        // POST : TechnicalKnowledges
        [HttpPost]
        public ActionResult TechDetail(string selectInput, string year ,string month, IndustryCategory? industryCategory)
        {
            Session["TkSelectInput"] = selectInput;
            Session["TkYear"] = year;
            Session["TkMonth"] = month;
            Session["TkIndustryCategory"] = industryCategory;
            return RedirectToAction("TechDetail");
        }

        // GET: TechnicalKnowledges/Edit/1
        public ActionResult TechDetailList(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalKnowledge tkTechnicalKnowledge = db.TechnicalKnowledges.Find(id);
            if (tkTechnicalKnowledge == null)
            {
                return HttpNotFound();
            }
            //廣告(技術區)
            ViewBag.AdNews = db.Ads.Where(x => x.AdStatus == Status.發行).Where(x => x.AdCategory == AdCategory.小圖示廣告).OrderBy(x => Guid.NewGuid()).Take(3).ToList();

            //研討會(技術區)
            ViewBag.SeminarsNews = db.Seminars.OrderBy(x => x.IsTop).ThenByDescending(x => x.SDate).Take(3).ToList();

            //教育訓練(技術區)
            ViewBag.TrainingCourseNews = db.TrainingCourses.Where(x => x.Status == Status.發行)
                .OrderByDescending(x => x.SignUpSDate).Take(3).ToList();

            //點閱次數
            tkTechnicalKnowledge.Clicks += 1;
            db.Entry(tkTechnicalKnowledge).State = EntityState.Modified;
            db.SaveChanges();

            return View(tkTechnicalKnowledge);
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
