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

namespace CAEProject.Areas.Admin.Controllers
{
    public class FaqsController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 10;
        // GET: Admin/Faqs
        public ActionResult Index(int? page)
        {
            string faqTitle = Session["faqTitle"] == null ? null : Session["faqTitle"].ToString();
            FaqStatus? faqStatus = Session["faqStatus"] == null ? null : (FaqStatus?)Session["faqStatus"];
            DateTime? faqStrDateTime =
                Session["faqStrDateTime"] == null ? null : (DateTime?)Session["faqStrDateTime"];
            DateTime? faqEndDateTime =
                Session["faqEndDateTime"] == null ? null : (DateTime?)Session["faqEndDateTime"];
            Status? status = Session["status"] == null ? null : (Status?)Session["status"];
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var user = db.Faqs.OrderBy(x => x.DateTime).AsQueryable();

            if (!string.IsNullOrEmpty(faqTitle))
            {
                user = user.Where(x => x.Title.Contains(faqTitle) || x.Count.Contains(faqTitle));
            }

            if (faqStatus.HasValue)
            {
                user = user.Where(x => x.FaqStatus == faqStatus);
            }

            if (status.HasValue)
            {
                user = user.Where(x => x.Status == status);
            }

            if (faqStrDateTime.HasValue && faqEndDateTime.HasValue)
            {
                faqEndDateTime = faqEndDateTime.Value.AddDays(1);
                user = user.Where(x => x.ReleaseDateTime >= faqStrDateTime && x.ReleaseDateTime <= faqEndDateTime);
            }
            return View(user.ToPagedList(UserPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(string faqTitle, FaqStatus? faqStatus, DateTime? faqStrDateTime,
            DateTime? faqEndDateTime, Status? Status)
        {
            Session["faqTitle"] = faqTitle;
            Session["faqStatus"] = faqStatus;
            Session["faqStrDateTime"] = faqStrDateTime;
            Session["faqEndDateTime"] = faqEndDateTime;
            Session["status"] = Status;
            return RedirectToAction("Index");
        }

        // GET: Admin/Faqs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faq faq = db.Faqs.Find(id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return View(faq);
        }

        // GET: Admin/Faqs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Faqs/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FaqStatus,Title,SourceDateTime,Status,ReleaseDateTime,Clicks,Url,Count,AddUser,DateTime,EditUser,LastEditDateTime")] Faq faq)
        {
            if (ModelState.IsValid)
            {
                faq.DateTime = DateTime.Now;
                faq.AddUser = Utility.GetUserTickets().UserCodeName;
                faq.LastEditDateTime = DateTime.Now;
                db.Faqs.Add(faq);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(faq);
        }

        // GET: Admin/Faqs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faq faq = db.Faqs.Find(id);
            if (faq == null)
            {
                return HttpNotFound();
            }
            return View(faq);
        }

        // POST: Admin/Faqs/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FaqStatus,Title,SourceDateTime,Status,ReleaseDateTime,Clicks,Url,Count,AddUser,DateTime,EditUser,LastEditDateTime")] Faq faq)
        {
            if (ModelState.IsValid)
            {
                faq.EditUser = Utility.GetUserTickets().UserCodeName;
                faq.LastEditDateTime = DateTime.Now;
                db.Entry(faq).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faq);
        }

        // GET: Admin/Faqs/Delete/5
        public ActionResult Delete(int id)
        {
            Faq faq = db.Faqs.Find(id);
            db.Faqs.Remove(faq);
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
