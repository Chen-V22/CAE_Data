using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CAEProject.Areas.Admin.Filters;
using CAEProject.Models;
using MvcPaging;

namespace CAEProject.Areas.Admin.Controllers
{
    [Premission]
    public class ShowOffsController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 10;
        // GET: Admin/Faqs
        public ActionResult Index(int? page)
        {
            string soTitle = Session["soTitle"] == null ? null : Session["soTitle"].ToString();
            DateTime? soStrDateTime =
                Session["soStrDateTime"] == null ? null : (DateTime?)Session["soStrDateTime"];
            DateTime? soEndDateTime =
                Session["soEndDateTime"] == null ? null : (DateTime?)Session["soEndDateTime"];
            Status? status = Session["status"] == null ? null : (Status?)Session["status"];
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var user = db.ShowOffs.OrderBy(x => x.DateTime).AsQueryable();

            if (!string.IsNullOrEmpty(soTitle))
            {
                user = user.Where(x => x.Title.Contains(soTitle) || x.Count.Contains(soTitle));
            }

            if (status.HasValue)
            {
                user = user.Where(x => x.Status == status);
            }

            if (soStrDateTime.HasValue && soEndDateTime.HasValue)
            {
                soEndDateTime = soEndDateTime.Value.AddDays(1);
                user = user.Where(x => x.ShowDateTime >= soStrDateTime && x.ShowDateTime <= soEndDateTime);
            }
            return View(user.ToPagedList(UserPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(string soTitle, DateTime? soStrDateTime,
            DateTime? soEndDateTime, Status? Status)
        {
            Session["soTitle"] = soTitle;
            Session["soStrDateTime"] = soStrDateTime;
            Session["soEndDateTime"] = soEndDateTime;
            Session["status"] = Status;
            return RedirectToAction("Index");
        }

        // GET: Admin/ShowOffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowOff showOff = db.ShowOffs.Find(id);
            if (showOff == null)
            {
                return HttpNotFound();
            }
            return View(showOff);
        }

        // GET: Admin/ShowOffs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ShowOffs/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,SourceDate,Source,Status,ContactPerson,ContactPhone,ContactEmail,ShowDateTime,Count,Photo,Clicks,AddUser,DateTime,EditUser,LastEditDateTime")] ShowOff showOff,HttpPostedFileBase photo)
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
                    showOff.Photo = Utility.SaveUpImage(photo);
                    Utility.GenerateThumbnailImage(showOff.Photo, photo.InputStream, Server.MapPath("~/UpFile/Images"),
                        "s", 167, 115);
                }
                showOff.AddUser = Utility.GetUserTickets().UserCodeName;
                showOff.DateTime = DateTime.Now;
                showOff.LastEditDateTime = DateTime.Now;
                db.ShowOffs.Add(showOff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(showOff);
        }

        // GET: Admin/ShowOffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowOff showOff = db.ShowOffs.Find(id);
            if (showOff == null)
            {
                return HttpNotFound();
            }
            return View(showOff);
        }

        // POST: Admin/ShowOffs/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,SourceDate,Source,Status,ContactPerson,ContactPhone,ContactEmail,ShowDateTime,Count,Photo,Clicks,AddUser,DateTime,EditUser,LastEditDateTime")] ShowOff showOff,HttpPostedFileBase photo)
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
                    showOff.Photo = Utility.SaveUpImage(photo);
                    Utility.GenerateThumbnailImage(showOff.Photo, photo.InputStream, Server.MapPath("~/UpFile/Images"),
                        "s", 167, 115);
                }
                showOff.EditUser = Utility.GetUserTickets().UserCodeName;
                showOff.LastEditDateTime = DateTime.Now;
                db.Entry(showOff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(showOff);
        }

        // GET: Admin/ShowOffs/Delete/5
        public ActionResult Delete(int? id)
        {
            ShowOff showOff = db.ShowOffs.Find(id);
            db.ShowOffs.Remove(showOff);
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
