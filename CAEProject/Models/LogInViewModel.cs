using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class LogInViewModel //登入Model
    {

        [Display(Name = "使用者代號")]
        [MaxLength(50)]
        [Required(ErrorMessage = "{0}必填")]
        public string UserCodeName { get; set; }

        [Display(Name = "密碼")]
        [StringLength(100, ErrorMessage = "{0}密碼長度必須4個字元以上", MinimumLength = 4)]
        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}