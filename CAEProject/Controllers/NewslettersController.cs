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
    public class NewslettersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Newsletters
        public ActionResult Index()
        {
            return View(db.Newsletters.ToList());
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
