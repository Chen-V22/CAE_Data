using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CAEProject.Models;

namespace CAEProject.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();

        // GET: Home/Index
        public ActionResult Index()
        {
            //TOP廣告
            ViewBag.AdTop = db.Ads.Where(x => x.AdStatus == Status.發行).OrderBy(x => x.IsTop == IsTop.是).ToList();

            //最新快訊
            ViewBag.Newses = db.News.OrderBy(x => x.IsTop).ThenByDescending(x => x.SDate).Take(4).ToList();

            //研討會
            ViewBag.Seminars = db.Seminars.OrderBy(x => x.IsTop).ThenByDescending(x => x.SDate).Take(4).ToList();

            //電子報
            ViewBag.Newsletters = db.Newsletters.OrderByDescending(x => x.Date).ToList();

            //技術新知
            ViewBag.TechnicalKnowledge = db.TechnicalKnowledges.Where(x => x.AdStatus == Status.發行).OrderBy(x => x.IsTop).ThenByDescending(x => x.SDate).ToList();

            //教育訓練
            ViewBag.TrainingCourse = db.TrainingCourses.Where(x => x.Status == Status.發行)
                .OrderByDescending(x => x.SignUpSDate).ToList();

            //活動集錦
            ViewBag.ActivityRecord = db.ActivityRecords.Where(x => x.ActivityStatus == Status.發行)
                .OrderByDescending(x => x.SDate).Take(10).ToList();

            return View();
        }

        // GET: Home/SiteMap
        public ActionResult SiteMap()
        {
            return View();
        }

        // GET: Home/ConnectionUs
        public ActionResult ConnectionUs()
        {
            return View();
        }

        // POST: ConnectionUsViewModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConnectionUs([Bind(Include = "Id,FaqStatus,GenderType,Name,Phone,MobilePhone,Email,Count,CaptchaValue")] ConnectionUsViewModel connectionUsViewModel)
        {
            if (ModelState.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("聯絡人：" + connectionUsViewModel.Name);
                sb.AppendLine("性別：" + connectionUsViewModel.GenderType== GenderType.男.ToString()?"先生":"小姐");
                sb.AppendLine("聯絡人電話："+connectionUsViewModel.Phone);
                sb.AppendLine("聯絡人手機："+connectionUsViewModel.MobilePhone);
                sb.AppendLine("聯絡人信箱："+connectionUsViewModel.Email);
                sb.AppendLine("需求說明："+connectionUsViewModel.Count);

                string smtpAddress = "smtp.gmail.com";
                //設定Port
                int portNumber = 587;
                bool enableSSL = true;
                //填入寄送方email和密碼
                string emailFrom = "m4a1789456@gmail.com";
                string password = "M4a1778899";
                //收信方email 可以用逗號區分多個收件人
                string emailTo = "m4a1789456@gmail.com";
                //主旨
                string subject = "CAE雲端及試作服務加值中心 "+connectionUsViewModel.FaqStatus;
                //內容
                string body = sb.ToString();
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    // 若你的內容是HTML格式，則為True
                    mail.IsBodyHtml = true;
                    //如果需要夾帶檔案
                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));
                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
                TempData["Message"] = "訊息已送出";
                return RedirectToAction("Index");
            }

            return View(connectionUsViewModel);
        }

        public ActionResult TrialService()
        {
            //廣告(新聞區)
            ViewBag.AdNews = db.Ads.Where(x => x.AdStatus == Status.發行).Where(x => x.AdCategory == AdCategory.小圖示廣告).OrderBy(x => Guid.NewGuid()).Take(3).ToList();

            //研討會(新聞區)
            ViewBag.SeminarsNews = db.Seminars.OrderBy(x => x.IsTop).ThenByDescending(x => x.SDate).Take(3).ToList();

            //教育訓練(新聞區)
            ViewBag.TrainingCourseNews = db.TrainingCourses.Where(x => x.Status == Status.發行)
                .OrderByDescending(x => x.SignUpSDate).Take(3).ToList();


            return View();
        }

        [HttpPost]
        public ActionResult DownLoad()
        {
            //我要下載的檔案位置
            string filepath = Server.MapPath("~/傳產加值中心服務量能彙整.docx");
            //取得檔案名稱
            string filename = System.IO.Path.GetFileName(filepath);
            //讀成串流
            Stream iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
            //回傳出檔案
            return File(iStream, "application/msword", filename);
        }
    }
    
    
}