//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using CAEProject.Models;

//namespace CAEProject.Areas.Admin.Controllers
//{
//    public class MbFreeViewModelsController : Controller
//    {
//        private Model1 db = new Model1();

//        // GET: Admin/MbFreeViewModels
//        public ActionResult Index()
//        {
//            return View(db.MbFreeViewModels.ToList());
//        }

//        // GET: Admin/MbFreeViewModels/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            MbFreeViewModel mbFreeViewModel = db.MbFreeViewModels.Find(id);
//            if (mbFreeViewModel == null)
//            {
//                return HttpNotFound();
//            }
//            return View(mbFreeViewModel);
//        }

       

//        // GET: Admin/MbFreeViewModels/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            MbFreeViewModel mbFreeViewModel = db.MbFreeViewModels.Find(id);
//            if (mbFreeViewModel == null)
//            {
//                return HttpNotFound();
//            }
//            return View(mbFreeViewModel);
//        }

//        // POST: Admin/MbFreeViewModels/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            MbFreeViewModel mbFreeViewModel = db.MbFreeViewModels.Find(id);
//            db.MbFreeViewModels.Remove(mbFreeViewModel);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
