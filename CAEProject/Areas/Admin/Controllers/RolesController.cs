using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CAEProject.Areas.Admin.Filters;
using CAEProject.Models;

namespace CAEProject.Areas.Admin.Controllers
{
    [Premission]
    public class RolesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/Roles
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: Admin/Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Admin/Roles/Create
        public ActionResult Create()
        {
            var premission = db.Premissions.ToList();
            StringBuilder sb = new StringBuilder("[");
            GetPemission(premission.Where(x => x.pid == null).ToList(), sb);
            sb.Append("]");
            ViewBag.data = sb.ToString();
            return View();
        }

        private void GetPemission(ICollection<Premission> list, StringBuilder sb)
        {
            foreach (Premission permission in list)
            {
                sb.Append("{\"id\": \"" + permission.PValue + "\", \"text\": \"" + permission.Name + "\""); //if only got first layer then only use the ones at top and bottom
                if (permission.premissionSon.Count() > 0) //if has another children layer
                {
                    sb.Append(",\"children\":["); //get data is got another children
                    GetPemission(permission.premissionSon, sb); //run the function again in loop to get more data
                    sb.Append("]");
                }
                sb.Append("},"); //at the end of the array there's ',', use trim.end to get rid of the ,
            }
            string temp = sb.ToString();
            sb = new StringBuilder(temp);
        }

        // POST: Admin/Roles/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoleName,Authority")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Admin/Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            var premission = db.Premissions.ToList();
            StringBuilder sb = new StringBuilder("[");
            GetPemission(premission.Where(x => x.pid == null).ToList(), sb);
            sb.Append("]");
            ViewBag.data = sb.ToString();
            return View(role);
        }

        // POST: Admin/Roles/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoleName,Authority")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Admin/Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Admin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
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
