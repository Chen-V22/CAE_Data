using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAEProject.Models;

namespace CAEProject.Controllers
{
    public class ProjectServicesController : Controller
    {
        private Model1 db = new Model1();

        // GET: ProjectServices/Create
        public ActionResult ProjectService()
        {
            //廣告(新聞區)
            ViewBag.AdNews = db.Ads.Where(x => x.AdStatus == Status.發行).Where(x => x.AdCategory == AdCategory.小圖示廣告).OrderBy(x => Guid.NewGuid()).Take(3).ToList();

            //研討會(新聞區)
            ViewBag.SeminarsNews = db.Seminars.OrderBy(x => x.IsTop).ThenByDescending(x => x.SDate).Take(3).ToList();

            //教育訓練(新聞區)
            ViewBag.TrainingCourseNews = db.TrainingCourses.Where(x => x.Status == Status.發行)
                .OrderByDescending(x => x.SignUpSDate).Take(3).ToList();
            
            if (Session["member"] != null)
            {
                TempData["status"] = true;
                return View();
            }
            else
            {
                TempData["PcError"] = "請先登入會員";
                TempData["status"] = false;
                return View();
            }
            
        }

        // POST: ProjectServices/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProjectService(ProjectService projectService)
        {
            if (ModelState.IsValid)
            {
                projectService.DateTime = DateTime.Now;
                projectService.Founder = Utility.GetUserTickets().UserCodeName;
                projectService.LastEditDateTime = DateTime.Now;
                db.ProjectServices.Add(projectService);
                db.SaveChanges();
                TempData["success"] = "你的需求已送出";
                return RedirectToAction("ProjectService");
            }

            return View(projectService);
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
