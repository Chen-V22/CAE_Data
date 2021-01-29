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

namespace CAEProject.Controllers
{
    public class NewslettersController : Controller
    {
        private const int DefaultPageSize = 10;
        private Model1 db = new Model1();

        // GET: News
        public ActionResult Index(int? page)
        {
            string selectInput = Session["selectInput"]?.ToString(); // ==> Session["selectInput"] == null ? null : Session["selectInput"].ToString()
            string year = Session["year"] == null ? null : Session["year"].ToString();
            var userPage = page - 1 ?? 0;
            var user = db.Newsletters.OrderByDescending(x => x.Date).AsQueryable();
            if (!string.IsNullOrEmpty(selectInput))
            {
                user = user.Where(x => x.Title.Contains(selectInput));
            }
            if (!string.IsNullOrEmpty(year))
            {
                int intDate = Convert.ToInt32(year);
                user = user.Where(x => x.Date.Year == intDate);
            }

            return View(user.ToPagedList(userPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(string year,string selectInput)
        {
            Session["selectInput"] = selectInput;
            Session["year"] = year;
            return RedirectToAction("Index", "Newsletters");
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
