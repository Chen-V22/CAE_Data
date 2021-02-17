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
    public class AdsController : Controller
    {

        private Model1 db = new Model1();
        private const int DefaultPageSize = 10;
        // GET: Admin/Ads
        public ActionResult Index(int? page)
        {
            AdCategory? adCategory = Session["adCategory"]==null?null:(AdCategory?)Session["adCategory"];
            string adTitle = Session["adTitle"]==null?null: Session["adTitle"].ToString();
            Status? adStatus = Session["adStatus"]==null?null:(Status?)Session["adStatus"];
            DateTime? adStrDateTime = Session["adStrDateTime"]==null?null:(DateTime?)Session["adStrDateTime"];
            DateTime? adEndDateTime = Session["adEndDateTime"] == null ? null : (DateTime?)Session["adEndDateTime"];
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var user = db.Ads.OrderBy(x => x.DateTime).AsQueryable();
            if (adCategory.HasValue)
            {
                user = user.Where(x => x.AdCategory == adCategory);
            }

            if (!string.IsNullOrEmpty(adTitle))
            {
                user = user.Where(x => x.Title.Contains(adTitle)|| x.Content.Contains(adTitle));
            }

            if (adStatus.HasValue)
            {
                user = user.Where(x => x.AdStatus == adStatus);
            }

            if (adStrDateTime.HasValue && adEndDateTime.HasValue)
            {
                adEndDateTime = adEndDateTime.Value.AddDays(1);
                user = user.Where(x => x.DateTime >= adStrDateTime && x.LastEditDateTime <= adEndDateTime);
            }
            return View(user.ToPagedList(UserPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(AdCategory? adCategory, string adTitle, Status? adStatus, DateTime? adStrDateTime,
            DateTime? adEndDateTime)
        {

            Session["adCategory"] = adCategory;
            Session["adTitle"] = adTitle;
            Session["adStatus"] = adStatus;
            Session["adStrDateTime"] = adStrDateTime;
            Session["adEndDateTime"] = adEndDateTime;
            return RedirectToAction("Index");
        }

        // GET: Admin/Ads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad ad = db.Ads.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // GET: Admin/Ads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Ads/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,AdCategory,Title,Photo,Content,Applican,AdStatus,Url,ClickRate,SDate,EDate,Annex,IsTop,DateTime,LastEditDateTime,AddUser,EditUser")] Ad ad, HttpPostedFileBase photo, HttpPostedFileBase upfile)
        {
            if (ModelState.IsValid)
            {
                //附件上傳
                if (upfile != null)
                {
                    ad.Annex = Utility.SaveUpFile(upfile);
                }

                //相片上傳
                if (photo != null)
                {
                    if (photo.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }
                    ad.Photo = Utility.SaveUpImage(photo);
                    Utility.GenerateThumbnailImage(ad.Photo, photo.InputStream, Server.MapPath("~/UpFile/Images"),
                        "s", 167, 115);
                }

                ad.AddUser = Utility.GetUserTickets().UserCodeName;
                ad.DateTime = DateTime.Now;
                ad.EditUser = Utility.GetUserTickets().UserCodeName;
                ad.LastEditDateTime = DateTime.Now;
                db.Ads.Add(ad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ad);
        }

        // GET: Admin/Ads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad ad = db.Ads.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // POST: Admin/Ads/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,AdCategory,Title,Photo,Content,Applican,AdStatus,Url,ClickRate,SDate,EDate,Annex,IsTop,DateTime,LastEditDateTime,AddUser,EditUser")] Ad ad, HttpPostedFileBase photo, HttpPostedFileBase upfile)
        {
            if (ModelState.IsValid)
            {
                //附件上傳
                if (upfile != null)
                {
                    ad.Annex = Utility.SaveUpFile(upfile);
                }

                //相片上傳
                if (photo != null)
                {
                    if (photo.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }
                    ad.Photo = Utility.SaveUpImage(photo);
                    Utility.GenerateThumbnailImage(ad.Photo, photo.InputStream, Server.MapPath("~/upfile/Images"),
                        "s", 167, 115);
                }

                ad.EditUser = Utility.GetUserTickets().UserCodeName;
                ad.LastEditDateTime = DateTime.Now;
                db.Entry(ad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ad);
        }

        // POST: Admin/Ads/Delete/5
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Ad ad = db.Ads.Find(id);
            db.Ads.Remove(ad);
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
