using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class ActivityPhoto //活動相片
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        public int ActivityId { get; set; }

        [Display(Name = "相片")]
        public string Photo { get; set; }

        [Display(Name = "相片說明")]
        public string PhotoAnnotation { get; set; }

        [Display(Name = "上傳日期")]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        [ForeignKey("ActivityId")]
        [Display(Name = "相簿名稱")]
        public virtual ActivityRecord ActivityRecord { get; set; }
    }
}