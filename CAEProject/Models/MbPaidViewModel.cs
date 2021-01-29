using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class MbPaidViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "建立日期")]
        public DateTime DateTime { get; set; }

        [Display(Name = "會員帳號")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(15)]
        public string Account { get; set; }

        [Display(Name = "會員密碼")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(100, ErrorMessage = "密碼長度不得小於4個字", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Display(Name = "申請狀態")]
        //public ApplicationStatus ApplicationStatus { get; set; }

        [Display(Name = "申請通過日期")]
        [DataType(DataType.DateTime)]
        public DateTime? ApproveDateTime { get; set; }

        [Display(Name = "停用日期")]
        [DataType(DataType.DateTime)]
        public DateTime? DisenableDateTime { get; set; }

        [Display(Name = "下次通知日期")]
        [DataType(DataType.DateTime)]
        public DateTime? NoticeDateTime { get; set; }

        /*----------------鑽石、白金----------------*/
        [Display(Name = "公司名稱")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        public string CompanyName { get; set; }

        [Display(Name = "統編")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(10)]
        public string CompanyNumber { get; set; }

        [Display(Name = "負責人")]
        [MaxLength(50)]
        public string Principal { get; set; }

        [Display(Name = "負責人職稱")]
        [MaxLength(20)]
        public string PrincipalJobTitle { get; set; }

        [Display(Name = "公司電話")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(20)]
        public string CompanyPhone { get; set; }

        [Display(Name = "公司網址")]
        [MaxLength(100)]
        public string CompanyUrl { get; set; }

        [Display(Name = "聯絡人職稱")]
        [MaxLength(20)]
        public string ContactPersonJobTitle { get; set; }

        [Display(Name = "員工數")]
        public string EmployeeCount { get; set; }

        [Display(Name = "公司屬性")]
        public CompanyType CompanyType { get; set; }

        [Display(Name = "產業類別")]
        public Industry Industry { get; set; }

        [Display(Name = "人才培訓需求")]
        public Training Training { get; set; }

        [Display(Name = "公司簡介")]
        public string CompanyIntroduction { get; set; }

        [Display(Name = "業務項目")]
        public string Business { get; set; }

        [Display(Name = "上傳公司圖片")]
        public string CompanyPhoto { get; set; }
        /*------------------------------------------- */

        [Display(Name = "聯絡人")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        public string ContactPerson { get; set; }

        [Display(Name = "聯絡人電話")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(20)]
        public string ContactPersonPhone { get; set; }

        [Display(Name = "聯絡人E-MAIL")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        public string ContactPersonEmail { get; set; }

        [Display(Name = "地址")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        public string Address { get; set; }

        [Display(Name = "分機")]
        public string Extension { get; set; }

        [Display(Name = "傳真")]
        [MaxLength(15)]
        public string Fax { get; set; }

        [Display(Name = "CAE軟體需求(可複選)")]
        [Required(ErrorMessage = "{0}必填")]
        public Demand Demand { get; set; }

        [Display(Name = "是否訂閱電子報")]
        public Subscription Subscription { get; set; }

        [Display(Name = "修改者")]
        public string EditUser { get; set; }

        [Display(Name = "最終修改日期")]
        public DateTime? LastEditDateTime { get; set; }
    }
}