using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class Seminar //研討會Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "活動類型")]
        public SeminarStatus SeminarStatus { get; set; }

        [Display(Name = "標題")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Display(Name = "公告日期")]
        [Required(ErrorMessage = "{0}必填")]
        public DateTime ShowDateTime { get; set; }

        [Display(Name = "狀態")]
        public Status Status { get; set; }

        [Display(Name = "主講人")]
        [MaxLength(100)]
        public string Lecturer { get; set; }

        [Display(Name = "置頂")]
        public IsTop IsTop { get; set; }

        [Display(Name = "點閱次數")]
        public int Clicks { get; set; }

        [Display(Name = "參考網址")]
        [MaxLength(100)]
        public string Url { get; set; }

        [Display(Name = "活動地點")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        public string Address { get; set; }

        [Display(Name = "主辦單位")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        public string Organizer { get; set; }

        [Display(Name = "協辦單位")]
        [MaxLength(50)]
        public string Assisting { get; set; }
        
        [Display(Name = "內容")]
        [Required(ErrorMessage = "{0}必填")]
        public string Count { get; set; }

        [Display(Name = "附件上傳")]
        public string File { get; set; }

        [Display(Name = "活動開始日期")]
        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.DateTime)]
        public DateTime SDate { get; set; }

        [Display(Name = "活動結束日期")]
        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.DateTime)]
        public DateTime EDate { get; set; }

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