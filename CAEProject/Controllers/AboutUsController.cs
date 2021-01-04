using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CAEProject.Controllers
{
    public class AboutUsController : Controller
    {
        // GET: AboutUs/AboutUs
        //關於CAE中心
        public ActionResult AboutUs()
        {
            return View();
        }

        // GET: AboutUs/ConnectionUs
        //聯絡我們
        public ActionResult ConnectionUs()
        {
            return View();
        }

        // GET: AboutUs/ConnectionUs
        //聯絡我們
        public ActionResult Service()
        {
            return View();
        }


    }
}