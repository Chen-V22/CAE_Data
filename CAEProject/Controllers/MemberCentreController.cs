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
    public class MemberCentreController : Controller
    {
        private const int DefaultPageSize = 4;
        private Model1 db = new Model1();

        // GET: MemberCentre/MemberCenter
        public ActionResult MemberCenter(int? page)
        {
            if (Session["member"] != null)
            {
                Member member = (Member)Session["member"];
                var userPage = page - 1 ?? 0;
                var user = db.MbPaids.Where(x => x.MbId == member.Id).OrderByDescending(x => x.PaidDateTime).AsQueryable();
                return View(user.ToPagedList(userPage, DefaultPageSize));
            }
            else
            {
                TempData["memberError"] = "你尚未登入";
                return RedirectToAction("Login", "Members");
            }

        }

        // GET: MemberCentre/Details/5
        public ActionResult MemberPoint(int? page)
        {
            if (Session["member"] != null)
            {
                Member member = (Member)Session["member"];
                var userPage = page - 1 ?? 0;
                var user = db.MbPointses.Where(x => x.MbId == member.Id).OrderByDescending(x => x.UseDateTime).AsQueryable();
                return View(user.ToPagedList(userPage, DefaultPageSize));
            }
            else
            {
                TempData["memberError"] = "你尚未登入";
                return RedirectToAction("Login", "Members");
            }
        }

        // GET: MemberCentre/Create
        public ActionResult MemberEdit()
        {
            if (Session["member"] != null)
            {
                Member ber = (Member)Session["member"];
                Member member = db.Members.Find(ber.Id);
                MbPaidViewModel mbPaidViewModel = new MbPaidViewModel();
                if (member != null)
                {
                    ViewBag.MemberLevel = member.MemberLevel; //會員類別
                    mbPaidViewModel.Password = member.Password;
                    mbPaidViewModel.Account = member.Account;
                    mbPaidViewModel.CompanyName = member.CompanyName;
                    mbPaidViewModel.CompanyNumber = member.CompanyNumber;
                    mbPaidViewModel.Principal = member.Principal;
                    mbPaidViewModel.PrincipalJobTitle = member.PrincipalJobTitle;
                    mbPaidViewModel.CompanyPhone = member.CompanyPhone;
                    mbPaidViewModel.CompanyUrl = member.CompanyUrl;
                    mbPaidViewModel.ContactPersonJobTitle = member.ContactPersonJobTitle;
                    mbPaidViewModel.EmployeeCount = member.EmployeeCount.ToString();
                    mbPaidViewModel.ContactPersonEmail = member.ContactPersonEmail;
                    mbPaidViewModel.CompanyType = member.CompanyType;
                    mbPaidViewModel.Industry = member.Industry;
                    mbPaidViewModel.Training = member.Training;
                    mbPaidViewModel.CompanyIntroduction = member.CompanyIntroduction;
                    mbPaidViewModel.Business = member.Business;
                    mbPaidViewModel.CompanyPhone = member.CompanyPhone;
                    mbPaidViewModel.ContactPerson = member.ContactPerson;
                    mbPaidViewModel.ContactPersonPhone = member.ContactPersonPhone;
                    mbPaidViewModel.Address = member.Address;
                    mbPaidViewModel.Extension = member.Extension.ToString();
                    mbPaidViewModel.Fax = member.Fax;
                    mbPaidViewModel.Demand = member.Demand;
                    mbPaidViewModel.Subscription = member.Subscription;
                    mbPaidViewModel.DateTime = member.DateTime;
                }
                return View(mbPaidViewModel);
            }
            else
            {
                TempData["memberError"] = "你尚未登入";
                return RedirectToAction("Login", "Members");
            }
            
        }

        // POST: MemberCentre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MemberEdit([Bind(Include = "Id,DateTime,Account,Password,CompanyName,CompanyNumber,Principal,PrincipalJobTitle,CompanyPhone,CompanyUrl,ContactPersonJobTitle,EmployeeCount,CompanyType,Industry,Training,CompanyIntroduction,Business,CompanyPhoto,ContactPerson,ContactPersonPhone,ContactPersonEmail,Address,Extension,Fax,Demand,Subscription,EditUser,LastEditDateTime")] MbPaidViewModel mbPaidViewModel, HttpPostedFileBase companyPhone, MemberLevel applicationType,string addPassword)
        {
            if (Session["member"] != null)
            {
                Member mbr = (Member)Session["member"];
                Member member = new Member();
                member.Id = mbr.Id;
                member.MemberLevel = applicationType;
                member.ApplicationStatus = mbr.ApplicationStatus;
                member.Account = mbPaidViewModel.Account;
                if (!String.IsNullOrEmpty(addPassword))
                {
                    member.PasswordSalt = Utility.CreateSalt();
                    member.Password = Utility.GenerateHashWithSalt(mbPaidViewModel.Password, member.PasswordSalt);
                }
                else
                {
                    member.PasswordSalt = mbr.PasswordSalt;
                    member.Password = mbPaidViewModel.Password;
                }
                member.CompanyName = mbPaidViewModel.CompanyName;
                member.CompanyNumber = mbPaidViewModel.CompanyNumber;
                member.Principal = mbPaidViewModel.Principal;
                member.PrincipalJobTitle = mbPaidViewModel.PrincipalJobTitle;
                member.CompanyPhone = mbPaidViewModel.CompanyPhone;
                member.CompanyUrl = mbPaidViewModel.CompanyUrl;
                member.ContactPersonJobTitle = mbPaidViewModel.ContactPersonJobTitle;
                member.EmployeeCount = Convert.ToInt32(mbPaidViewModel.EmployeeCount);
                member.ContactPersonEmail = mbPaidViewModel.ContactPersonEmail;
                member.CompanyType = mbPaidViewModel.CompanyType;
                member.Industry = mbPaidViewModel.Industry;
                member.Training = mbPaidViewModel.Training;
                member.CompanyIntroduction = mbPaidViewModel.CompanyIntroduction;
                member.Business = mbPaidViewModel.Business;
                member.CompanyPhone = mbPaidViewModel.CompanyPhone;
                member.ContactPerson = mbPaidViewModel.ContactPerson;
                member.ContactPersonPhone = mbPaidViewModel.ContactPersonPhone;
                member.Address = mbPaidViewModel.Address;
                member.Extension = Convert.ToInt32(mbPaidViewModel.Extension);
                member.Fax = mbPaidViewModel.Fax;
                member.Demand = mbPaidViewModel.Demand;
                member.Subscription = mbPaidViewModel.Subscription;
                member.DateTime = DateTime.Now;
                member.LastEditDateTime = DateTime.Now;
                member.EditUser = mbPaidViewModel.Account;
                if (companyPhone != null)
                {
                    if (companyPhone.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }
                    member.CompanyPhoto = Utility.SaveUpImage(companyPhone);
                    Utility.GenerateThumbnailImage(member.CompanyPhoto, companyPhone.InputStream, Server.MapPath("~/UpFile/Images"),
                        "s", 138, 99);
                }
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                TempData["memberError"] = "更新成功";
                return RedirectToAction("MemberEdit","MemberCentre");
            }
            return View(mbPaidViewModel);
        }

        // GET: MemberCentre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MbPoints mbPoints = db.MbPointses.Find(id);
            if (mbPoints == null)
            {
                return HttpNotFound();
            }
            ViewBag.MbId = new SelectList(db.Members, "Id", "Account", mbPoints.MbId);
            return View(mbPoints);
        }

        // POST: MemberCentre/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MbId,UseOfHour,UsePoints,Points,UseDateTime")] MbPoints mbPoints)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mbPoints).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MbId = new SelectList(db.Members, "Id", "Account", mbPoints.MbId);
            return View(mbPoints);
        }

        // GET: MemberCentre/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MbPoints mbPoints = db.MbPointses.Find(id);
            if (mbPoints == null)
            {
                return HttpNotFound();
            }
            return View(mbPoints);
        }

        // POST: MemberCentre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MbPoints mbPoints = db.MbPointses.Find(id);
            db.MbPointses.Remove(mbPoints);
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
