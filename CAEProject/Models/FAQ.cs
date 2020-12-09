using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class Faq //常見問與答
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "問題類別")]
        public FaqStatus FaqStatus { get; set; }

        [Display(Name = "標題")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Display(Name = "資料來源日期")]
        [Required(ErrorMessage = "{0}必填")]
        public DateTime SourceDateTime { get; set; }

        [Display(Name = "狀態")]
        public Status Status { get; set; }

        [Display(Name = "發布日期")]
        public DateTime ReleaseDateTime { get; set; }

        [Display(Name = "點閱次數")]
        public int Clicks { get; set; }

        [Display(Name = "參考網址")]
        [MaxLength(100)]
        public string Url { get; set; }

        [Display(Name = "內容")]
        [Required(ErrorMessage = "{0}必填")]
        public string Count { get; set; }

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