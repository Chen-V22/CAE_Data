using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAEProject.Models;
using MvcPaging;

namespace CAEProject.Controllers
{
    public class NewsController : Controller
    {
        private const int DefaultPageSize = 4;
        private Model1 db = new Model1();

        // GET: News
        public ActionResult News(int? page)
        {
            
            //廣告(新聞區)
            ViewBag.AdNews = db.Ads.Where(x => x.AdStatus == Status.發行).Where(x => x.AdCategory == AdCategory.小圖示廣告).OrderBy(x => Guid.NewGuid()).Take(3).ToList();

            //研討會(新聞區)
            ViewBag.SeminarsNews = db.Seminars.OrderBy(x => x.IsTop).ThenByDescending(x => x.SDate).Take(3).ToList();

            //教育訓練(新聞區)
            ViewBag.TrainingCourseNews = db.TrainingCourses.Where(x => x.Status == Status.發行)
                .OrderByDescending(x => x.SignUpSDate).Take(3).ToList();

            string selectInput = Session["selectInput"]?.ToString(); // ==> Session["selectInput"] == null ? null : Session["selectInput"].ToString()
            string date = Session["date"] == null ? null : Session["date"].ToString();
            var userPage = page - 1 ?? 0;
            var user = db.News.OrderByDescending(x => x.SDate).AsQueryable();
            if (!string.IsNullOrEmpty(selectInput))
            {
                user = user.Where(x => x.Title.Contains(selectInput) || x.Count.Contains(selectInput));
            }

            if (!string.IsNullOrEmpty(date))
            {
                int intDate = Convert.ToInt32(date);
                user = user.Where(x => x.SDate.Year==intDate);
            }

            return View(user.ToPagedList(userPage,DefaultPageSize));
        }

        [HttpPost]
        public ActionResult News(string selectInput , string date)
        {
            Session["date"] = date;
            Session["selectInput"] = selectInput;
            return RedirectToAction("News","News");
        }

        // GET: News/News_List/5
        public ActionResult NewsList(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
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

            news.Clicks += 1;
            db.Entry(news).State = EntityState.Modified;
            db.SaveChanges();
            
            return View(news);
        }

        // POST: News/News_List/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult NewsList([Bind(Include = "Id,Title,SourceDate,Source,Url,Clicks,Photo,IsTop,SDate,EDate,Count,File,AddUser,DateTime,EditUser,LastEditDateTime")] News news)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(news).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(news);
        //}

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
