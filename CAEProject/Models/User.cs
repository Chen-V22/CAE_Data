using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class User //管理者Model
    {

        [Display(Name = "編號")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //使用者暱稱(登入帳號)
        [Display(Name = "使用者代號")]
        [MaxLength(50)]
        [Required(ErrorMessage = "{0}必填")]
        public string UserCodeName { get; set; }

        [Display(Name = "使用者名稱")]
        [MaxLength(50)]
        [Required(ErrorMessage = "{0}必填")]
        public string UserName { get; set; }

        [Display(Name = "電子信箱(E-MAIL)")]
        [MaxLength(100)]
        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "密碼")]
        [StringLength(100, ErrorMessage = "{0}密碼長度必須4個字元以上", MinimumLength = 4)]
        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "密碼鹽")]
        [MaxLength(100)]
        public string PasswordSalt { get; set; }

        [Display(Name = "登入次數")]
        public int NumberOfLogins { get; set; }

        [Display(Name = "腳色名稱")]
        public int RoleId { get; set; }

        [Display(Name = "腳色")]
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        //In Create Get DateTime.Now
        [Display(Name = "建立日期")]
        public DateTime DateTime { get; set; }

        //使用驗證Cookie加入更新者名稱
        [Display(Name = "最後修改人")]
        public string LastEditor { get; set; }

        //In Edit Get DateTime.Now
        [Display(Name = "更新日期")]
        public DateTime LastEditDateTime { get; set; }
    }
}