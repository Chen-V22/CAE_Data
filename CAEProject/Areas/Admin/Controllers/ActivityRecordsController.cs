using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAEProject.Models;
using MvcPaging;

namespace CAEProject.Areas.Admin.Controllers
{
    public class ActivityRecordsController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 10;
        // GET: Admin/ActivityRecords
        public ActionResult Index(int? page)
        {
            string ArTitle = Session["ArTitle"] == null ? null : Session["ArTitle"].ToString();
            IsTop? ArIsTop = Session["ArIsTop"] == null ? null : (IsTop?)Session["ArIsTop"];
            DateTime? ArStrDateTime =
                Session["ArStrDateTime"] == null ? null : (DateTime?)Session["ArStrDateTime"];
            DateTime? ArEndDateTime =
                Session["ArEndDateTime"] == null ? null : (DateTime?)Session["ArEndDateTime"];
            Status? ArAdStatus = Session["ArAdStatus"] == null ? null : (Status?)Session["ArAdStatus"];
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var user = db.ActivityRecords.OrderBy(x => x.DateTime).AsQueryable();

            if (!string.IsNullOrEmpty(ArTitle))
            {
                user = user.Where(x => x.Title.Contains(ArTitle) || x.Count.Contains(ArTitle));
            }

            if (ArIsTop.HasValue)
            {
                user = user.Where(x => x.IsTop == ArIsTop);
            }

            if (ArAdStatus.HasValue)
            {
                user = user.Where(x => x.ActivityStatus == ArAdStatus);
            }

            if (ArStrDateTime.HasValue && ArEndDateTime.HasValue)
            {
                ArEndDateTime = ArEndDateTime.Value.AddDays(1);
                user = user.Where(x => x.DateTime >= ArStrDateTime && x.LastEditDateTime <= ArEndDateTime);
            }
            return View(user.ToPagedList(UserPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(string ArTitle, IsTop? ArIsTop, DateTime? ArStrDateTime,
            DateTime? ArEndDateTime, IndustryCategory? ArIC, Status? ArStatus)
        {
            Session["ArTitle"] = ArTitle;
            Session["ArIsTop"] = ArIsTop;
            Session["ArStrDateTime"] = ArStrDateTime;
            Session["ArEndDateTime"] = ArEndDateTime;
            Session["ArIC"] = ArIC;
            Session["ArAdStatus"] = ArStatus;
            return RedirectToAction("Index");
        }

        // GET: Admin/ActivityRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityRecord activityRecord = db.ActivityRecords.Find(id);
            if (activityRecord == null)
            {
                return HttpNotFound();
            }
            return View(activityRecord);
        }

        // GET: Admin/ActivityRecords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ActivityRecords/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ActivityStatus,PublishDateTime,Source,Clicks,Photo,IsTop,SDate,EDate,Count,AddUser,DateTime,EditUser,LastEditDateTime")] ActivityRecord activityRecord, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                //相片上傳
                if (photo != null)
                {
                    if (photo.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }
                    activityRecord.Photo = Utility.SaveUpImage(photo);
                    Utility.GenerateThumbnailImage(activityRecord.Photo, photo.InputStream, Server.MapPath("~/UpFile/Images"),
                        "s", 167, 115);
                }
                activityRecord.DateTime = DateTime.Now;
                activityRecord.AddUser = Utility.GetUserTickets().UserCodeName;
                activityRecord.LastEditDateTime = DateTime.Now;
                db.ActivityRecords.Add(activityRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(activityRecord);
        }

        // GET: Admin/ActivityRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityRecord activityRecord = db.ActivityRecords.Find(id);
            if (activityRecord == null)
            {
                return HttpNotFound();
            }
            return View(activityRecord);
        }

        // POST: Admin/ActivityRecords/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,ActivityStatus,PublishDateTime,Source,Clicks,Photo,IsTop,SDate,EDate,Count,AddUser,DateTime,EditUser,LastEditDateTime")] ActivityRecord activityRecord, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                //相片上傳
                if (photo != null)
                {
                    if (photo.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }
                    activityRecord.Photo = Utility.SaveUpImage(photo);
                    Utility.GenerateThumbnailImage(activityRecord.Photo, photo.InputStream, Server.MapPath("~/UpFile/Images"),
                        "s", 167, 115);
                }
                activityRecord.EditUser = Utility.GetUserTickets().UserCodeName;
                activityRecord.LastEditDateTime = DateTime.Now;
                db.Entry(activityRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activityRecord);
        }

        public ActionResult Delete(int id)
        {
            ActivityRecord activityRecord = db.ActivityRecords.Find(id);
            db.ActivityRecords.Remove(activityRecord);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //<--------------------圖片列表Start---------------------->
        // GET: Admin/ActivityPhotoes
        public ActionResult ApIndex(int? id)
        {
            TempData["addId"] = id;
            //Tab頁面切換
            TempData["tab"] = TempData["tab"] == null ? 0 : TempData["tab"];
            ViewBag.ArData = db.ActivityRecords.Where(w => w.Id == id).ToList();
            var activityPhotos = db.ActivityPhotos.Include(a => a.ActivityRecord).Where(i => i.ActivityId == id);
            return View(activityPhotos.ToList());
        }
        //<--------------------圖片列表End---------------------->

        //<--------------------圖片上傳Start---------------------->
        [HttpGet]
        public ActionResult ArPhoto()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArPhoto(IEnumerable<HttpPostedFileBase> photo, string[] photoAnnotation)
        {
            if (ModelState.IsValid)
            {
                int i = 0;
                int ActivityId = (int)TempData["addId"];
                foreach (var fileBase in photo)
                {
                    ActivityPhoto activityPhotoAdd = new ActivityPhoto();
                    activityPhotoAdd.ActivityId = ActivityId;
                    if (fileBase != null && fileBase.ContentLength > 0)
                    {
                        activityPhotoAdd.Photo = Utility.SaveUpImage(fileBase);
                        Utility.GenerateThumbnailImage(activityPhotoAdd.Photo, fileBase.InputStream, Server.MapPath("~/UpFile/Images"),
                            "s", 167, 115);
                    }
                    activityPhotoAdd.PhotoAnnotation = photoAnnotation[i];
                    activityPhotoAdd.DateTime = DateTime.Now;
                    db.ActivityPhotos.Add(activityPhotoAdd);
                    i++;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["tab"] = 1;
            return View();
        }

        //<--------------------圖片上傳End---------------------->

        //<--------------------圖片刪除Start---------------------->
        // POST: Admin/ActivityPhotoes/Delete/5
        public ActionResult ApDelete(int id)
        {
            ActivityPhoto activityPhoto = db.ActivityPhotos.Find(id);
            db.ActivityPhotos.Remove(activityPhoto);
            db.SaveChanges();
            return RedirectToAction("ApIndex");
        }
        //<--------------------圖片刪除End---------------------->

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
