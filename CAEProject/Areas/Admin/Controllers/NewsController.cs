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
using MvcPaging;

namespace CAEProject.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 10;
        // GET: Admin/News
        [Premission]
        public ActionResult Index(int? page)
        {
            string newsTitle = Session["newsTitle"] == null ? null : Session["newsTitle"].ToString();
            IsTop? newsIsTop = Session["newsIsTop"] == null ? null : (IsTop?)Session["newsIsTop"];
            DateTime? newsStrDateTime =
                Session["newsStrDateTime"] == null ? null : (DateTime?)Session["newsStrDateTime"];
            DateTime? newsEndDateTime =
                Session["newsEndDateTime"] == null ? null : (DateTime?)Session["newsEndDateTime"];
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var user = db.News.OrderByDescending(x => x.DateTime).AsQueryable();
           
            if (!string.IsNullOrEmpty(newsTitle))
            {
                user = user.Where(x => x.Title.Contains(newsTitle) || x.Count.Contains(newsTitle));
            }

            if (newsIsTop.HasValue)
            {
                user = user.Where(x => x.IsTop == newsIsTop);
            }

            if (newsStrDateTime.HasValue && newsEndDateTime.HasValue)
            {
                newsEndDateTime = newsEndDateTime.Value.AddDays(1);
                user = user.Where(x => x.DateTime >= newsStrDateTime && x.LastEditDateTime <= newsEndDateTime);
            }
            return View(user.ToPagedList(UserPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(string newsTitle, IsTop? newsIsTop, DateTime? newsStrDateTime,
            DateTime? newsEndDateTime)
        {
            Session["newsTitle"] = newsTitle;
            Session["newsIsTop"] = newsIsTop;
            Session["newsStrDateTime"] = newsStrDateTime;
            Session["newsEndDateTime"] = newsEndDateTime;
            return RedirectToAction("Index");
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int? id)
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
            return View(news);
        }

        // GET: Admin/News/Create
        [Premission]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/News/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "Id,Title,SourceDate,Source,Url,Clicks,Photo,IsTop,SDate,EDate,Count,File,AddUser,DateTime,EditUser,LastEditDateTime")] News news, HttpPostedFileBase photo, HttpPostedFileBase upfile)
        {
            if (ModelState.IsValid)
            {
                if (upfile != null)
                {
                    news.File = Utility.SaveUpFile(upfile);
                }

                //相片上傳
                if (photo != null)
                {
                    if (photo.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }
                    news.Photo = Utility.SaveUpImage(photo);
                    Utility.GenerateThumbnailImage(news.Photo, photo.InputStream, Server.MapPath("~/UpFile/Images"),
                        "s", 290, 217);
                }

                news.DateTime = DateTime.Now;
                news.AddUser = Utility.GetUserTickets().UserCodeName;
                news.LastEditDateTime = DateTime.Now;
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: Admin/News/Edit/5
        [Premission]
        public ActionResult Edit(int? id)
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
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Title,SourceDate,Source,Url,Clicks,Photo,IsTop,SDate,EDate,Count,File,AddUser,DateTime,EditUser,LastEditDateTime")] News news, HttpPostedFileBase photo, HttpPostedFileBase upfile)
        {
            if (ModelState.IsValid)
            {
                if (upfile != null)
                {
                    news.File = Utility.SaveUpFile(upfile);
                }

                //相片上傳
                if (photo != null)
                {
                    if (photo.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }
                    news.Photo = Utility.SaveUpImage(photo);
                    Utility.GenerateThumbnailImage(news.Photo, photo.InputStream, Server.MapPath("~/UpFile/Images"),
                        "s", 290, 217);
                }

                news.EditUser = Utility.GetUserTickets().UserCodeName;
                news.LastEditDateTime = DateTime.Now;
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // POST: Admin/News/Delete/5
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
