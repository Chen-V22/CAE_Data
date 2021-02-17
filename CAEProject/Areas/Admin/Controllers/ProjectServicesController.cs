using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using CAEProject.Areas.Admin.Filters;
using CAEProject.Models;
using MvcPaging;

namespace CAEProject.Areas.Admin.Controllers
{
    [Premission]
    public class ProjectServicesController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 10;

        // GET: Admin/ProjectServices
        public ActionResult Index(int? page)
        {
            string psKeyword = Session["psKeyword"]?.ToString();
            ServiceType? serviceType = (ServiceType?)Session["serviceType"];
            DateTime? psStrDateTime = (DateTime?)Session["psStrDateTime"];
            DateTime? psEndDateTime = (DateTime?)Session["psEndDateTime"];
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var user = db.ProjectServices.OrderBy(x => x.DateTime).AsQueryable();
            if (!string.IsNullOrEmpty(psKeyword))
            {
                user = user.Where(x => x.Subject.Contains(psKeyword) || x.Content.Contains(psKeyword));
            }

            if (serviceType.HasValue)
            {
                user = user.Where(x => x.ServiceType == serviceType);
            }
            if (psStrDateTime.HasValue && psEndDateTime.HasValue)
            {
                psEndDateTime = psEndDateTime.Value.AddDays(1);
                user = user.Where(x => x.DateTime >= psStrDateTime && x.DateTime <= psEndDateTime);
            }
            return View(user.ToPagedList(UserPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(string psKeyword, ServiceType? serviceType, DateTime? psStrDateTime, DateTime? psEndDateTime)
        {
            Session["psKeyword"] = psKeyword;
            Session["serviceType"] = serviceType;
            Session["psStrDateTime"] = psStrDateTime;
            Session["psEndDateTime"] = psEndDateTime;
            return RedirectToAction("Index");
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
        public ActionResult PsReplies(int? id, int? tab)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "ProjectServices");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Tab頁面切換
            TempData["tab"] = tab ?? 0;

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
                return RedirectToAction("PsReplies", "ProjectServices", new { area = "Admin", id = id, tab = 2 });//成功時傳tab值至Get
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
            int pid = Convert.ToInt32(TempData["id"]);
            return RedirectToAction("PsReplies", "ProjectServices", new { area = "Admin", id = pid, tab = 2 });
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
