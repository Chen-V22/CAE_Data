using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class ConnectionUsViewModel
    {
        [Key]
        public int Id { get; set; }

        public FaqStatus FaqStatus { get; set; }

        public GenderType GenderType { get; set; }

        [Display(Name = "姓名")]
        [MaxLength(20)]
        [Required(ErrorMessage = "{0}必填")]
        public string Name { get; set; }

        [Display(Name = "連絡電話")]
        [MaxLength(10)]
        public string Phone { get; set; }

        
        [Display(Name = "行動電話")]
        [Required(ErrorMessage = "{0}必填")]
        public string MobilePhone { get; set; }

        [Display(Name = "電子信箱")]
        public string Email { get; set; }

        [Display(Name = "需求說明")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "{0}必填")]
        public string Count { get; set; }

        [Display(Name = "驗證碼")]
        public string CaptchaValue { get; set; }
    }
}