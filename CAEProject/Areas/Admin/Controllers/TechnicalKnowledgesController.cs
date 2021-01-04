using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAEProject.Models;
using MvcPaging;

namespace CAEProject.Areas.Admin.Controllers
{
    public class TechnicalKnowledgesController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 10;
        // GET: Admin/TechnicalKnowledges
        public ActionResult Index(int? page)
        {
            string TkTitle = Session["TkTitle"] == null ? null : Session["TkTitle"].ToString();
            IsTop? TkIsTop = Session["TkIsTop"] == null ? null : (IsTop?)Session["TkIsTop"];
            DateTime? TkStrDateTime =
                Session["TkStrDateTime"] == null ? null : (DateTime?)Session["TkStrDateTime"];
            DateTime? TkEndDateTime =
                Session["TkEndDateTime"] == null ? null : (DateTime?)Session["TkEndDateTime"];
            IndustryCategory? TkIC= Session["TkIC"] == null ? null : (IndustryCategory?)Session["TkIC"];
            Status? TkAdStatus = Session["TkAdStatus"] == null ? null : (Status?)Session["TkAdStatus"];
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var user = db.TechnicalKnowledges.OrderBy(x => x.DateTime).AsQueryable();

            if (!string.IsNullOrEmpty(TkTitle))
            {
                user = user.Where(x => x.Title.Contains(TkTitle) || x.Count.Contains(TkTitle));
            }

            if (TkIsTop.HasValue)
            {
                user = user.Where(x => x.IsTop == TkIsTop);
            }

            if (TkIC.HasValue)
            {
                user = user.Where(x => x.IndustryCategory == TkIC);
            }

            if (TkAdStatus.HasValue)
            {
                user = user.Where(x => x.AdStatus == TkAdStatus);
            }

            if (TkStrDateTime.HasValue && TkEndDateTime.HasValue)
            {
                TkEndDateTime = TkEndDateTime.Value.AddDays(1);
                user = user.Where(x => x.DateTime >= TkStrDateTime && x.LastEditDateTime <= TkEndDateTime);
            }
            return View(user.ToPagedList(UserPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(string TkTitle, IsTop? TkIsTop, DateTime? TkStrDateTime,
            DateTime? TkEndDateTime, IndustryCategory? TkIC, Status? TkAdStatus)
        {
            Session["TkTitle"] = TkTitle;
            Session["TkIsTop"] = TkIsTop;
            Session["TkStrDateTime"] = TkStrDateTime;
            Session["TkEndDateTime"] = TkEndDateTime;
            Session["TkIC"] = TkIC;
            Session["TkAdStatus"] = TkAdStatus;
            return RedirectToAction("Index");
        }

        // GET: Admin/TechnicalKnowledges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalKnowledge technicalKnowledge = db.TechnicalKnowledges.Find(id);
            if (technicalKnowledge == null)
            {
                return HttpNotFound();
            }
            return View(technicalKnowledge);
        }

        // GET: Admin/TechnicalKnowledges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TechnicalKnowledges/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,IndustryCategory,Title,PublishDateTime,Source,Clicks,ContactPerson,Email,Phone,AdStatus,Url,IsTop,Count,SDate,EDate,Photo,File,AddUser,DateTime,EditUser,LastEditDateTime")] TechnicalKnowledge technicalKnowledge, HttpPostedFileBase photo, HttpPostedFileBase upfile)
        {
            if (ModelState.IsValid)
            {
                if (upfile != null)
                {
                    technicalKnowledge.File = Utility.SaveUpFile(upfile);
                }

                //相片上傳
                if (photo != null)
                {
                    if (photo.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }
                    technicalKnowledge.Photo = Utility.SaveUpImage(photo);
                    Utility.GenerateThumbnailImage(technicalKnowledge.Photo, photo.InputStream, Server.MapPath("~/UpFile/Images"),
                        "s", 290, 217);
                }
                technicalKnowledge.DateTime = DateTime.Now;
                technicalKnowledge.AddUser = Utility.GetUserTickets().UserCodeName;
                technicalKnowledge.LastEditDateTime = DateTime.Now;
                db.TechnicalKnowledges.Add(technicalKnowledge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(technicalKnowledge);
        }

        // GET: Admin/TechnicalKnowledges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalKnowledge technicalKnowledge = db.TechnicalKnowledges.Find(id);
            if (technicalKnowledge == null)
            {
                return HttpNotFound();
            }
            return View(technicalKnowledge);
        }

        // POST: Admin/TechnicalKnowledges/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,IndustryCategory,Title,PublishDateTime,Source,Clicks,ContactPerson,Email,Phone,AdStatus,Url,IsTop,Count,SDate,EDate,Photo,File,AddUser,DateTime,EditUser,LastEditDateTime")] TechnicalKnowledge technicalKnowledge, HttpPostedFileBase photo, HttpPostedFileBase upfile)
        {
            if (ModelState.IsValid)
            {
                
                if (upfile != null)
                {
                    technicalKnowledge.File = Utility.SaveUpFile(upfile);
                }

                //相片上傳
                if (photo != null)
                {
                    if (photo.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }
                    technicalKnowledge.Photo = Utility.SaveUpImage(photo);
                    Utility.GenerateThumbnailImage(technicalKnowledge.Photo, photo.InputStream, Server.MapPath("~/UpFile/Images"),
                        "s", 290, 217);
                }
                
                technicalKnowledge.EditUser = Utility.GetUserTickets().UserCodeName;
                technicalKnowledge.LastEditDateTime = DateTime.Now;
                db.Entry(technicalKnowledge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(technicalKnowledge);
        }

        // GET: Admin/TechnicalKnowledges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalKnowledge technicalKnowledge = db.TechnicalKnowledges.Find(id);
            if (technicalKnowledge == null)
            {
                return HttpNotFound();
            }
            return View(technicalKnowledge);
        }

        // POST: Admin/TechnicalKnowledges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TechnicalKnowledge technicalKnowledge = db.TechnicalKnowledges.Find(id);
            db.TechnicalKnowledges.Remove(technicalKnowledge);
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
