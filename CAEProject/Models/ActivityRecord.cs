using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class ActivityRecord //活動集錦Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "標題")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Display(Name = "狀態")]
        public Status ActivityStatus { get; set; }

        [Display(Name = "來源日期")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishDateTime { get; set; }

        [Display(Name = "資料來源")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(20)]
        public string Source { get; set; }

        [Display(Name = "點閱次數")]
        public int Clicks { get; set; }

        [Display(Name = "圖片")]
        public string Photo { get; set; }

        [Display(Name = "置頂")]
        public IsTop IsTop { get; set; }

        [Display(Name = "展示開始日期")]
        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SDate { get; set; }

        [Display(Name = "展示結束日期")]
        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EDate { get; set; }

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

        public virtual ICollection<ActivityPhoto> ActivityPhotos { get; set; }
    }
}