using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using CAEProject.Models;
using MvcPaging;

namespace CAEProject.Areas.Admin.Controllers
{
    public class ProjectServicesController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 10;

        // GET: Admin/ProjectServices
        public ActionResult Index(int? page)
        {
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var user = db.ProjectServices.OrderBy(x => x.DateTime).AsQueryable();
            return View(user.ToPagedList(UserPage,DefaultPageSize));
        }

        // GET: Admin/ProjectServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProjectServices/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ServiceType,Subject,Consultant,Content,Annex,DateTime,Founder,LastEditDateTime,LastEditor")] ProjectService projectService)
        {
            if (ModelState.IsValid)
            {
                projectService.DateTime = DateTime.Now;
                projectService.Founder = Utility.GetUserTickets().UserCodeName;
                projectService.LastEditDateTime = DateTime.Now;
                db.ProjectServices.Add(projectService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projectService);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ProjectService projectService = db.ProjectServices.Find(id);
            db.ProjectServices.Remove(projectService);
            db.SaveChanges();
            return RedirectToAction("Index", "ProjectServices", new { area = "Admin" });
        }

        //<--------------------回覆Start---------------------->
        // GET: Admin/PsReplies/Create
        public ActionResult PsReplies(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Tab頁面切換
            TempData["tab"] = TempData["tab"] == null ? 0 : TempData["tab"];

            //給Delete返回用Id
            TempData["id"] = id;

            ViewBag.PsData = db.ProjectServices.Where(w => w.Id == id).ToList();
            ViewBag.PsReplies = db.PsReplies.Where(w => w.PsId == id).OrderByDescending(o => o.DateTime).ThenByDescending(t => t.Id).ToList();
            return View();
        }

        // POST: Admin/PsReplies/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PsReplies([Bind(Include = "Id,PsId,ServiceStatus,Reply,DateTime")] PsReply psReply, int id)
        {
            if (ModelState.IsValid)
            {
                psReply.PsId = id;
                psReply.DateTime = DateTime.Now;
                db.PsReplies.Add(psReply);
                db.SaveChanges();
                TempData["tab"] = 2;//成功時傳值至Get
                return RedirectToAction("PsReplies", "ProjectServices", new { area = "Admin", id = id });
            }
            return View(psReply);
        }
        //<--------------------回覆End----------------------->

        //<--------------------回覆刪除Start---------------------->
        // Admin/ProjectServices/PsReplieDel/5
        public ActionResult PsReplieDel(int id)
        {
            PsReply psReply = db.PsReplies.Find(id);
            db.PsReplies.Remove(psReply);
            db.SaveChanges();
            TempData["tab"] = 2;
            int pid = Convert.ToInt32(TempData["id"]);
            return RedirectToAction("PsReplies", "ProjectServices", new { area = "Admin", id = pid });
        }
        //<--------------------回覆刪除End----------------------->


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
