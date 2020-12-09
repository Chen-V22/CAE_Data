using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class MbLevel //會員等級Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "名稱/說明")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Display(Name = "金額")]
        public int Price { get; set; }

        [Display(Name = "點數")]
        public int Points { get; set; }

        [Display(Name = "額外點數")]
        public int Welfare { get; set; }

        [Display(Name = "新增者")]
        public string AddUser { get; set; }

        [Display(Name = "建立日期")]
        public DateTime DateTime { get; set; }

        [Display(Name = "修改者")]
        public string EditUser { get; set; }

        [Display(Name = "最終修改日期")]
        public DateTime LastEditDateTime { get; set; }
    }
}