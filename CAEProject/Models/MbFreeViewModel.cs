using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class MbFreeViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        //會員等級
        [Display(Name = "會員等級")]
        public MemberLevel MemberLevel { get; set; }

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

        [Display(Name = "申請狀態")]
        public ApplicationStatus ApplicationStatus { get; set; }

        [Display(Name = "申請通過日期")]
        [DataType(DataType.DateTime)]
        public DateTime? ApproveDateTime { get; set; }

        [Display(Name = "停用日期")]
        [DataType(DataType.DateTime)]
        public DateTime? DisenableDateTime { get; set; }

        [Display(Name = "下次通知日期")]
        [DataType(DataType.DateTime)]
        public DateTime? NoticeDateTime { get; set; }

        [Display(Name = "聯絡人")]
        [MaxLength(50)]
        public string ContactPerson { get; set; }

        [Display(Name = "聯絡人電話")]
        [MaxLength(20)]
        public string ContactPersonPhone { get; set; }

        [Display(Name = "地址")]
        [MaxLength(50)]
        [Required(ErrorMessage = "{0}必填")]
        public string Address { get; set; }

        [Display(Name = "分機")]
        public int Extension { get; set; }

        [Display(Name = "傳真")]
        [MaxLength(15)]
        public string Fax { get; set; }

        /*----------------一般、學生----------------*/
        [Display(Name = "現職機構/學校名稱")]
        [MaxLength(50)]
        public string CurrentIdentity { get; set; }

        [Display(Name = "所屬單位/就讀系所")]
        [MaxLength(50)]
        public string CurrentUnit { get; set; }

        [Display(Name = "職稱/就讀(大學、研究所、博士)")]
        [MaxLength(50)]
        public string JobTitle { get; set; }

        [Display(Name = "行動電話")]
        [MaxLength(10)]
        public string MobilePhone { get; set; }

        [Display(Name = "身分證號")]
        [MaxLength(10)]
        [RegularExpression(@"^[A-Z]{1}[0-9]{9}$")]
        public string IdCard { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "電話")]
        [MaxLength(50)]
        [Required(ErrorMessage = "{0}必填")]
        public string Phone { get; set; }

        [Display(Name = "現職機構營業項目/研究領域或主題")]
        [DataType(DataType.MultilineText)]
        public string BusinessItem { get; set; }
        /*------------------------------------------- */

        [Display(Name = "CAE軟體需求(可複選)")]
        public Demand Demand { get; set; }

        [Display(Name = "是否訂閱電子報")]
        public Subscription Subscription { get; set; }

        [Display(Name = "修改者")]
        public string EditUser { get; set; }

        [Display(Name = "最終修改日期")]
        public DateTime LastEditDateTime { get; set; }
    }
}