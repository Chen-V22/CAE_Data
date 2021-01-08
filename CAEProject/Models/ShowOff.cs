using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class ShowOff //案例展示Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "標題")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Display(Name = "資料來源日期")]
        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.DateTime)]
        public DateTime SourceDate { get; set; }

        [Display(Name = "資料來源")]
        [Required(ErrorMessage = "{0}必填")]
        public string Source { get; set; }

        [Display(Name = "狀態")]
        public Status Status { get; set; }

        [Display(Name = "聯絡人")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(20)]
        public string ContactPerson { get; set; }

        [Display(Name = "聯絡人電話")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(10)]
        public string ContactPhone { get; set; }

        [Display(Name = "聯絡人E-mail")]
        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string ContactEmail { get; set; }

        [Display(Name = "公告日期")]
        [Required(ErrorMessage = "{0}必填")]
        public DateTime ShowDateTime { get; set; }

        [Display(Name = "內容")]
        [Required(ErrorMessage = "{0}必填")]
        public string Count { get; set; }

        [Display(Name = "圖片")]
        public string Photo { get; set; }

        [Display(Name = "點閱次數")]
        public int Clicks { get; set; }

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