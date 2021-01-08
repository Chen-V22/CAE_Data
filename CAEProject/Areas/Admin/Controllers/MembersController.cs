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

namespace CAEProject.Areas.Admin.Controllers
{
    public class MembersController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 15;

        // GET: Admin/Members
        public ActionResult Index(int? page)
        {
            string mbTitle = Session["mbTitle"]?.ToString();
            ApplicationStatus? applicationStatus = (ApplicationStatus?)Session["applicationStatus"];
            MemberLevel? memberLevel = (MemberLevel?)Session["memberLevel"];
            DateTime? mbStrDateTime = (DateTime?)Session["mbStrDateTime"];
            DateTime? mbEndDateTime = (DateTime?)Session["mbEndDateTime"];
            int userPage = page.HasValue ? page.Value - 1 : 0;
            var data = db.Members.OrderBy(x => x.DateTime).AsQueryable();
            if (!string.IsNullOrEmpty(mbTitle))
            {
                data = data.Where(x => x.CompanyName.Contains(mbTitle) || x.ContactPerson.Contains(mbTitle));
            }
            if (applicationStatus.HasValue)
            {
                data = data.Where(x => x.ApplicationStatus == applicationStatus);
            }
            if (memberLevel.HasValue)
            {
                data = data.Where(x => x.MemberLevel == memberLevel);
            }
            if (mbStrDateTime.HasValue && mbEndDateTime.HasValue)
            {
                mbEndDateTime = mbStrDateTime.Value.AddDays(1);
                data = data.Where(x => x.DateTime >= mbStrDateTime && x.DateTime <= mbEndDateTime);
            }

            return View(data.ToPagedList(userPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(string mbTitle, ApplicationStatus? applicationStatus, MemberLevel? memberLevel, DateTime? mbStrDateTime, DateTime? mbEndDateTime)
        {
            Session["mbTitle"] = mbTitle;
            Session["applicationStatus"] = applicationStatus;
            Session["memberLevel"] = memberLevel;
            Session["mbStrDateTime"] = mbStrDateTime;
            Session["mbEndDateTime"] = mbEndDateTime;
            return RedirectToAction("Index");
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
            ViewBag.Remark = db.MbRemarkses.Where(x => x.MbRemarksId == id).ToList();
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
                
                //啟用時間
                if (member.ApplicationStatus != ApplicationStatus.已啟用)
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
                member.ApplicationStatus = mbPaidViewModel.ApplicationStatus;
                member.Password = !string.IsNullOrEmpty(passwordAdd) ? passwordAdd : mbPaidViewModel.Password;
                member.NoticeDateTime = mbPaidViewModel.NoticeDateTime;
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
                member.Extension = Convert.ToInt32(mbFreeViewModel.Extension);
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

            ViewBag.Remark = db.MbRemarkses.Where(x=>x.MbRemarksId==id).ToList();
            MbFreeViewModel mbFreeViewModel = new MbFreeViewModel();
            mbFreeViewModel.Account = member.Account;
            mbFreeViewModel.Password = member.Password;
            mbFreeViewModel.ApproveDateTime = member.ApproveDateTime;
            mbFreeViewModel.DisenableDateTime = member.DisenableDateTime;
            mbFreeViewModel.ApplicationStatus = member.ApplicationStatus;
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
            mbFreeViewModel.Extension = member.Extension.ToString();
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
        public ActionResult FreeEdit(MbFreeViewModel mbFreeViewModel,int id)
        {
            if (ModelState.IsValid)
            {
                Member member = new Member();
                member.Id = id;

                //啟用時間
                if (member.ApplicationStatus != ApplicationStatus.已啟用)
                {
                    member.ApproveDateTime = mbFreeViewModel.ApplicationStatus == ApplicationStatus.已啟用
                        ? DateTime.Now
                        : mbFreeViewModel.ApproveDateTime;
                }
                else
                {
                    member.ApproveDateTime = mbFreeViewModel.ApproveDateTime;
                }

                //停止時間
                if (member.ApplicationStatus != ApplicationStatus.以停止)
                {
                    member.DisenableDateTime = mbFreeViewModel.ApplicationStatus == ApplicationStatus.以停止
                        ? DateTime.Now
                        : mbFreeViewModel.DisenableDateTime;
                }
                else
                {
                    member.DisenableDateTime = mbFreeViewModel.DisenableDateTime;
                }

                member.MemberLevel = MemberLevel.一般會員;
                member.ApplicationStatus = mbFreeViewModel.ApplicationStatus;
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
                member.Extension = Convert.ToInt32(mbFreeViewModel.Extension);
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
