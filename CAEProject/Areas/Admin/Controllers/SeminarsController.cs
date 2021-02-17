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
    public class SeminarsController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 10;
        // GET: Admin/Seminars
        public ActionResult Index(int? page)
        {
            string semTitle = Session["semTitle"] == null ? null : Session["semTitle"].ToString();
            SeminarStatus? seminarStatus = Session["SeminarStatus"] == null ? null : (SeminarStatus?)Session["SeminarStatus"];
            IsTop? semIsTop = Session["semIsTop"] == null ? null : (IsTop?)Session["semIsTop"];
            Status? semAdStatus = Session["semStatus"] == null ? null : (Status?)Session["semStatus"];
            DateTime? semStrDateTime =
                Session["semStrDateTime"] == null ? null : (DateTime?)Session["semStrDateTime"];
            DateTime? semEndDateTime =
                Session["semEndDateTime"] == null ? null : (DateTime?)Session["semEndDateTime"];
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var user = db.Seminars.OrderBy(x => x.DateTime).AsQueryable();

            if (!string.IsNullOrEmpty(semTitle))
            {
                user = user.Where(x => x.Title.Contains(semTitle) || x.Count.Contains(semTitle));
            }

            if (seminarStatus.HasValue)
            {
                user = user.Where(x => x.SeminarStatus == seminarStatus);
            }

            if (semIsTop.HasValue)
            {
                user = user.Where(x => x.IsTop == semIsTop);
            }

            if (semAdStatus.HasValue)
            {
                user = user.Where(x => x.Status == semAdStatus);
            }

            if (semStrDateTime.HasValue && semEndDateTime.HasValue)
            {
                semEndDateTime = semEndDateTime.Value.AddDays(1);
                user = user.Where(x => x.ShowDateTime >= semStrDateTime && x.ShowDateTime <= semEndDateTime);
            }
            return View(user.ToPagedList(UserPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(string semTitle, IsTop? semIsTop, DateTime? semStrDateTime,
            DateTime? semEndDateTime, SeminarStatus? seminarStatus, Status? semStatus)
        {
            Session["semTitle"] = semTitle;
            Session["SeminarStatus"] = seminarStatus;
            Session["semIsTop"] = semIsTop;
            Session["semStrDateTime"] = semStrDateTime;
            Session["semEndDateTime"] = semEndDateTime;
            Session["semStatus"] = semStatus;
            return RedirectToAction("Index");
        }
        // GET: Admin/Seminars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seminar seminar = db.Seminars.Find(id);
            if (seminar == null)
            {
                return HttpNotFound();
            }
            return View(seminar);
        }

        // GET: Admin/Seminars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Seminars/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,SeminarStatus,Title,ShowDateTime,Status,Lecturer,IsTop,Clicks,Url,Address,Organizer,Assisting,Count,File,SDate,EDate,AddUser,DateTime,EditUser,LastEditDateTime")] Seminar seminar, HttpPostedFileBase upfile)
        {
            if (ModelState.IsValid)
            {
                //附件上傳
                if (upfile != null)
                {
                    seminar.File = Utility.SaveUpFile(upfile);
                }
                seminar.AddUser = Utility.GetUserTickets().UserCodeName;
                seminar.DateTime = DateTime.Now;
                seminar.LastEditDateTime = DateTime.Now;
                db.Seminars.Add(seminar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(seminar);
        }

        // GET: Admin/Seminars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seminar seminar = db.Seminars.Find(id);
            if (seminar == null)
            {
                return HttpNotFound();
            }
            return View(seminar);
        }

        // POST: Admin/Seminars/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SeminarStatus,Title,ShowDateTime,Status,Lecturer,IsTop,Clicks,Url,Address,Organizer,Assisting,Count,File,SDate,EDate,AddUser,DateTime,EditUser,LastEditDateTime")] Seminar seminar, HttpPostedFileBase upfile)
        {
            if (ModelState.IsValid)
            {
                if (upfile != null)
                {
                    seminar.File = Utility.SaveUpFile(upfile);
                }
                seminar.EditUser = Utility.GetUserTickets().UserCodeName;
                seminar.LastEditDateTime = DateTime.Now;
                db.Entry(seminar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seminar);
        }

        // GET: Admin/Seminars/Delete/5
        public ActionResult Delete(int? id)
        {
            Seminar seminar = db.Seminars.Find(id);
            db.Seminars.Remove(seminar);
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
