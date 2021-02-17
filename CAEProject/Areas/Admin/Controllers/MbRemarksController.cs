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

namespace CAEProject.Areas.Admin.Controllers
{
    [Premission]
    public class MbRemarksController : Controller
    {
        private Model1 db = new Model1();

        // POST: Admin/MbRemarks/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaidRemarkCreate([Bind(Include = "Id,MbRemarksId,Remark,AddUser,DateTime")] MbRemarks mbRemarks)
        {
            if (ModelState.IsValid)
            {
                mbRemarks.AddUser = Utility.GetUserTickets().UserCodeName;
                mbRemarks.DateTime = DateTime.Now;
                db.MbRemarkses.Add(mbRemarks);
                db.SaveChanges();
                return RedirectToAction("PaidEdit", "Members", new { area = "Admin" ,id=mbRemarks.MbRemarksId});
            }

            ViewBag.MbRemarksId = new SelectList(db.Members, "Id", "Account", mbRemarks.MbRemarksId);
            return View(mbRemarks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FreeRemarkCreate([Bind(Include = "Id,MbRemarksId,Remark,AddUser,DateTime")] MbRemarks mbRemarks)
        {
            if (ModelState.IsValid)
            {
                mbRemarks.AddUser = Utility.GetUserTickets().UserCodeName;
                mbRemarks.DateTime = DateTime.Now;
                db.MbRemarkses.Add(mbRemarks);
                db.SaveChanges();
                return RedirectToAction("FreeEdit", "Members", new { area = "Admin" });
            }

            ViewBag.MbRemarksId = new SelectList(db.Members, "Id", "Account", mbRemarks.MbRemarksId);
            return View(mbRemarks);
        }

        // GET: Admin/MbRemarks/Delete/5
        public ActionResult PaidDelete(int? id)
        {
            MbRemarks mbRemarks = db.MbRemarkses.Find(id);
            db.MbRemarkses.Remove(mbRemarks);
            db.SaveChanges();
            return RedirectToAction("PaidEdit", "Members", new { area = "Admin" });
        }

        public ActionResult FreeDelete(int? id)
        {
            MbRemarks mbRemarks = db.MbRemarkses.Find(id);
            db.MbRemarkses.Remove(mbRemarks);
            db.SaveChanges();
            return RedirectToAction("FreeEdit", "Members", new { area = "Admin" });
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
