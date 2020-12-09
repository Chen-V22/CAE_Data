using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAEProject.Models;

namespace CAEProject.Areas.Admin.Controllers
{
    public class MembersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/Members
        public ActionResult Index()
        {
            var member = db.Members.ToList();
            return View(member);
        }


        //// GET: Admin/Faqs/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Faq faq = db.Faqs.Find(id);
        //    if (faq == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(faq);
        //}

        //// POST: Admin/Faqs/Edit/5
        //// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        //// 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,FaqStatus,Title,SourceDateTime,Status,ReleaseDateTime,Clicks,Url,Count,AddUser,DateTime,EditUser,LastEditDateTime")] Faq faq)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        faq.EditUser = Utility.GetUserTickets().UserCodeName;
        //        faq.LastEditDateTime = DateTime.Now;
        //        db.Entry(faq).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(faq);
        //}

        // GET: Admin/Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Admin/Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //----------------------------PaidData--------------------------
        // GET: Admin/MbPaidViewModels/Create
        public ActionResult PaidCreate()
        {
            return View();
        }

        // POST: Admin/MbPaidViewModels/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaidCreate([Bind(Include = "Id,DateTime,Account,Password,ApplicationStatus,ApproveDateTime,DisenableDateTime,NoticeDateTime,CompanyName,CompanyNumber,Principal,PrincipalJobTitle,CompanyPhone,CompanyUrl,ContactPersonJobTitle,EmployeeCount,CompanyType,Industry,Training,CompanyIntroduction,Business,CompanyPhoto,ContactPerson,ContactPersonPhone,ContactPersonEmail,Address,Extension,Fax,Demand,Subscription,EditUser,LastEditDateTime")] MbPaidViewModel mbPaidViewModel, MemberLevel memberLevel)
        {
            if (ModelState.IsValid)
            {
                Member member = new Member();
                member.MemberLevel = memberLevel;
                member.Account = mbPaidViewModel.Account;
                member.Password = mbPaidViewModel.Password;
                member.NoticeDateTime = mbPaidViewModel.NoticeDateTime;
                member.CompanyName = mbPaidViewModel.CompanyName;
                member.CompanyNumber = mbPaidViewModel.CompanyNumber;
                member.Principal = mbPaidViewModel.Principal;
                member.PrincipalJobTitle = mbPaidViewModel.PrincipalJobTitle;
                member.CompanyPhone = mbPaidViewModel.CompanyPhone;
                member.CompanyUrl = mbPaidViewModel.CompanyUrl;
                member.ContactPersonJobTitle = mbPaidViewModel.ContactPersonJobTitle;
                member.EmployeeCount = mbPaidViewModel.EmployeeCount;
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
                member.Extension = mbPaidViewModel.Extension;
                member.Fax = mbPaidViewModel.Fax;
                member.Demand = mbPaidViewModel.Demand;
                member.Subscription = mbPaidViewModel.Subscription;
                member.DateTime = DateTime.Now;
                member.LastEditDateTime = DateTime.Now;
                member.EditUser = mbPaidViewModel.Account;

                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mbPaidViewModel);
        }

        // GET: Admin/MbPaidViewModels/Edit/5
        public ActionResult PaidEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.MemberDate = db.Members.Find(id);

            if (ViewBag.MemberDate == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: Admin/MbPaidViewModels/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaidEdit(MbPaidViewModel mbPaidViewModel, int id, string passwordAdd)
        {
            if (ModelState.IsValid)
            {
                Member member = new Member();
                member.Id = id;
                member.ApplicationStatus = mbPaidViewModel.ApplicationStatus;
                //啟用時間
                if (member.ApplicationStatus!=ApplicationStatus.已啟用)
                {
                     member.ApproveDateTime = mbPaidViewModel.ApplicationStatus == ApplicationStatus.已啟用
                    ? DateTime.Now
                    : mbPaidViewModel.ApproveDateTime;
                }
                else
                {
                    member.ApproveDateTime = mbPaidViewModel.ApproveDateTime;
                }

                //停止時間
                if (member.ApplicationStatus != ApplicationStatus.以停止)
                {
                    member.DisenableDateTime = mbPaidViewModel.ApplicationStatus == ApplicationStatus.以停止
                        ? DateTime.Now
                        : mbPaidViewModel.DisenableDateTime;
                }
                else
                {
                    member.DisenableDateTime = mbPaidViewModel.DisenableDateTime;
                }

                member.MemberLevel = MemberLevel.鑽石會員;
                member.Account = mbPaidViewModel.Account;
                member.Password = !string.IsNullOrEmpty(passwordAdd) ? passwordAdd : mbPaidViewModel.Password;
                member.NoticeDateTime = mbPaidViewModel.NoticeDateTime;
                member.CompanyName = mbPaidViewModel.CompanyName;
                member.CompanyNumber = mbPaidViewModel.CompanyNumber;
                member.Principal = mbPaidViewModel.Principal;
                member.PrincipalJobTitle = mbPaidViewModel.PrincipalJobTitle;
                member.CompanyPhone = mbPaidViewModel.CompanyPhone;
                member.CompanyUrl = mbPaidViewModel.CompanyUrl;
                member.ContactPersonJobTitle = mbPaidViewModel.ContactPersonJobTitle;
                member.EmployeeCount = mbPaidViewModel.EmployeeCount;
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
                member.Extension = mbPaidViewModel.Extension;
                member.Fax = mbPaidViewModel.Fax;
                member.Demand = mbPaidViewModel.Demand;
                member.Subscription = mbPaidViewModel.Subscription;
                member.DateTime = mbPaidViewModel.DateTime;
                member.LastEditDateTime = DateTime.Now;
                member.EditUser = mbPaidViewModel.Account;

                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mbPaidViewModel);
        }
        //----------------------------------------------------------------

        //----------------------------FreeData--------------------------
        // GET: Admin/MbFreeViewModels/Create
        public ActionResult FreeCreate()
        {
            return View();
        }

        // POST: Admin/MbFreeViewModels/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FreeCreate([Bind(Include = "Id,MemberLevel,DateTime,Account,Password,ApplicationStatus,ApproveDateTime,DisenableDateTime,NoticeDateTime,ContactPerson,ContactPersonPhone,Address,Extension,Fax,CurrentIdentity,CurrentUnit,JobTitle,MobilePhone,IdCard,Email,Phone,BusinessItem,Demand,Subscription,EditUser,LastEditDateTime")] MbFreeViewModel mbFreeViewModel, MemberLevel memberLevel)
        {
            if (ModelState.IsValid)
            {
                Member member = new Member();
                member.MemberLevel = memberLevel;
                member.Account = mbFreeViewModel.Account;
                member.Password = mbFreeViewModel.Password;
                member.NoticeDateTime = mbFreeViewModel.NoticeDateTime;
                member.CurrentIdentity = mbFreeViewModel.CurrentIdentity;
                member.CurrentUnit = mbFreeViewModel.CurrentUnit;
                member.JobTitle = mbFreeViewModel.JobTitle;
                member.MobilePhone = mbFreeViewModel.MobilePhone;
                member.IdCard = mbFreeViewModel.IdCard;
                member.Email = mbFreeViewModel.Email;
                member.Phone = mbFreeViewModel.Phone;
                member.BusinessItem = mbFreeViewModel.BusinessItem;
                member.ContactPerson = mbFreeViewModel.ContactPerson;
                member.ContactPersonPhone = mbFreeViewModel.ContactPersonPhone;
                member.Address = mbFreeViewModel.Address;
                member.Extension = mbFreeViewModel.Extension;
                member.Fax = mbFreeViewModel.Fax;
                member.Demand = mbFreeViewModel.Demand;
                member.Subscription = mbFreeViewModel.Subscription;
                member.DateTime = DateTime.Now;
                member.LastEditDateTime = DateTime.Now;
                member.EditUser = mbFreeViewModel.Account;

                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mbFreeViewModel);
        }

        // GET: Admin/MbFreeViewModels/Edit/5
        public ActionResult FreeEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            MbFreeViewModel mbFreeViewModel = new MbFreeViewModel();
            mbFreeViewModel.Account = member.Account;
            mbFreeViewModel.Password = member.Password;
            mbFreeViewModel.NoticeDateTime = member.NoticeDateTime;
            mbFreeViewModel.CurrentIdentity = member.CurrentIdentity;
            mbFreeViewModel.CurrentUnit = member.CurrentUnit;
            mbFreeViewModel.JobTitle = member.JobTitle;
            mbFreeViewModel.MobilePhone = member.MobilePhone;
            mbFreeViewModel.IdCard = member.IdCard;
            mbFreeViewModel.Email = member.Email;
            mbFreeViewModel.Phone = member.Phone;
            mbFreeViewModel.BusinessItem = member.BusinessItem;
            mbFreeViewModel.ContactPerson = member.ContactPerson;
            mbFreeViewModel.ContactPersonPhone = member.ContactPersonPhone;
            mbFreeViewModel.Address = member.Address;
            mbFreeViewModel.Extension = member.Extension;
            mbFreeViewModel.Fax = member.Fax;
            mbFreeViewModel.Demand = member.Demand;
            mbFreeViewModel.Subscription = member.Subscription;
            mbFreeViewModel.DateTime = member.DateTime;
            return View(mbFreeViewModel);
        }

        // POST: Admin/MbFreeViewModels/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FreeEdit([Bind(Include = "Id,MemberLevel,DateTime,Account,Password,ApplicationStatus,ApproveDateTime,DisenableDateTime,NoticeDateTime,ContactPerson,ContactPersonPhone,Address,Extension,Fax,CurrentIdentity,CurrentUnit,JobTitle,MobilePhone,IdCard,Email,Phone,BusinessItem,Demand,Subscription,EditUser,LastEditDateTime")] MbFreeViewModel mbFreeViewModel)
        {
            if (ModelState.IsValid)
            {
                Member member = new Member();
                member.MemberLevel = mbFreeViewModel.MemberLevel;
                member.Account = mbFreeViewModel.Account;
                member.Password = mbFreeViewModel.Password;
                member.NoticeDateTime = mbFreeViewModel.NoticeDateTime;
                member.CurrentIdentity = mbFreeViewModel.CurrentIdentity;
                member.CurrentUnit = mbFreeViewModel.CurrentUnit;
                member.JobTitle = mbFreeViewModel.JobTitle;
                member.MobilePhone = mbFreeViewModel.MobilePhone;
                member.IdCard = mbFreeViewModel.IdCard;
                member.Email = mbFreeViewModel.Email;
                member.Phone = mbFreeViewModel.Phone;
                member.BusinessItem = mbFreeViewModel.BusinessItem;
                member.ContactPerson = mbFreeViewModel.ContactPerson;
                member.ContactPersonPhone = mbFreeViewModel.ContactPersonPhone;
                member.Address = mbFreeViewModel.Address;
                member.Extension = mbFreeViewModel.Extension;
                member.Fax = mbFreeViewModel.Fax;
                member.Demand = mbFreeViewModel.Demand;
                member.Subscription = mbFreeViewModel.Subscription;
                member.DateTime = DateTime.Now;
                member.LastEditDateTime = DateTime.Now;
                member.EditUser = mbFreeViewModel.Account;

                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mbFreeViewModel);
        }
        //----------------------------------------------------------------

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
