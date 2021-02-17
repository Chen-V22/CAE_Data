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
    [Premission]
    public class ResultsPublishedsController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 10;
        // GET: Admin/Faqs
        public ActionResult Index(int? page)
        {
            string rpTitle = Session["rpTitle"] == null ? null : Session["rpTitle"].ToString();
            DateTime? rpStrDateTime =
                Session["rpStrDateTime"] == null ? null : (DateTime?)Session["rpStrDateTime"];
            DateTime? rpEndDateTime =
                Session["rpEndDateTime"] == null ? null : (DateTime?)Session["rpEndDateTime"];
            Status? status = Session["status"] == null ? null : (Status?)Session["status"];
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var user = db.ResultsPublisheds.OrderBy(x => x.DateTime).AsQueryable();

            if (!string.IsNullOrEmpty(rpTitle))
            {
                user = user.Where(x => x.Title.Contains(rpTitle) || x.Count.Contains(rpTitle));
            }

            if (status.HasValue)
            {
                user = user.Where(x => x.Status == status);
            }

            if (rpStrDateTime.HasValue && rpEndDateTime.HasValue)
            {
                rpEndDateTime = rpEndDateTime.Value.AddDays(1);
                user = user.Where(x => x.ShowDate >= rpStrDateTime && x.ShowDate <= rpEndDateTime);
            }
            return View(user.ToPagedList(UserPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(string rpTitle, DateTime? rpStrDateTime,
            DateTime? rpEndDateTime, Status? Status)
        {
            Session["rpTitle"] = rpTitle;
            Session["rpStrDateTime"] = rpStrDateTime;
            Session["rpEndDateTime"] = rpEndDateTime;
            Session["status"] = Status;
            return RedirectToAction("Index");
        }

        // GET: Admin/ResultsPublisheds/Details/5
        public ActionResult Details(int? id)
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
            return View(resultsPublished);
        }

        // GET: Admin/ResultsPublisheds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ResultsPublisheds/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,SourceDate,Source,Status,ReviewDate,ContactPerson,ContactPhone,ContactEmail,Count,Photo,Clicks,ShowDate,AddUser,DateTime,EditUser,LastEditDateTime")] ResultsPublished resultsPublished,HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null)
                {
                    if (photo.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }
                    resultsPublished.Photo = Utility.SaveUpImage(photo);
                    Utility.GenerateThumbnailImage(resultsPublished.Photo, photo.InputStream, Server.MapPath("~/UpFile/Images"),
                        "s", 167, 115);
                }
                resultsPublished.AddUser = Utility.GetUserTickets().UserCodeName;
                resultsPublished.DateTime = DateTime.Now;
                resultsPublished.LastEditDateTime = DateTime.Now;
                db.ResultsPublisheds.Add(resultsPublished);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resultsPublished);
        }

        // GET: Admin/ResultsPublisheds/Edit/5
        public ActionResult Edit(int? id)
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
            return View(resultsPublished);
        }

        // POST: Admin/ResultsPublisheds/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,SourceDate,Source,Status,ReviewDate,ContactPerson,ContactPhone,ContactEmail,Count,Photo,Clicks,ShowDate,AddUser,DateTime,EditUser,LastEditDateTime")] ResultsPublished resultsPublished, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null)
                {
                    if (photo.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }
                    resultsPublished.Photo = Utility.SaveUpImage(photo);
                    Utility.GenerateThumbnailImage(resultsPublished.Photo, photo.InputStream, Server.MapPath("~/UpFile/Images"),
                        "s", 167, 115);
                }
                resultsPublished.EditUser = Utility.GetUserTickets().UserCodeName;
                resultsPublished.LastEditDateTime = DateTime.Now;
                db.Entry(resultsPublished).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resultsPublished);
        }

        // GET: Admin/ResultsPublisheds/Delete/5
        public ActionResult Delete(int? id)
        {
            ResultsPublished resultsPublished = db.ResultsPublisheds.Find(id);
            db.ResultsPublisheds.Remove(resultsPublished);
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
