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
    public class FaqsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Faqs
        public ActionResult Index(int? id)
        {
            ViewBag.data = db.Faqs.Where(x => x.FaqStatus== (id==null?(FaqStatus?)1:(FaqStatus?)id)).OrderByDescending(x => x.DateTime).ToList();
            return View(db.Faqs.ToList());
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
