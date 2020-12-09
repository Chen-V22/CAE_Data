using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CAEProject.Models;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class Ad //廣告Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "廣告類型")]
        public AdCategory AdCategory { get; set; }

        [Display(Name = "廣告標題")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Display(Name = "廣告圖片")]
        public string Photo { get; set; }

        [Display(Name = "廣告內容")]
        public string Content { get; set; }

        [Display(Name = "申請人")]
        [Required(ErrorMessage = "{0}必填")]
        public string Applican { get; set; }

        [Display(Name = "狀態")]
        public Status AdStatus { get; set; }

        [Display(Name = "廣告連結")]
        public string Url { get; set; }

        [Display(Name = "點閱率")]
        public int? ClickRate { get; set; }

        [Display(Name = "展示開始日期")]
        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        public DateTime SDate { get; set; }

        [Display(Name = "展示結束日期")]
        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        public DateTime EDate { get; set; }

        [Display(Name = "附件上傳")]
        [DataType(DataType.Upload)]
        public string Annex { get; set; }

        [Display(Name = "置頂")]
        public IsTop IsTop { get; set; }

        [Display(Name = "建立日期")]
        public DateTime DateTime { get; set; }

        [Display(Name = "修改日期")]
        public DateTime LastEditDateTime { get; set; }

        [Display(Name = "新增者")]
        public string AddUser { get; set; }

        [Display(Name = "修改者")]
        public string EditUser { get; set; }
    }
}