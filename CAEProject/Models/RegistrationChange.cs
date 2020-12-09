using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class RegistrationChange //報名表不固定欄位(設定)Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        public int TrainingId { get; set; }

        [Display(Name = "排序")]
        [MaxLength(3)]
        public int Sort { get; set; }

        [Display(Name = "欄位名稱")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Display(Name = "長度")]
        public int MaxLength { get; set; }

        [Display(Name = "必填")]
        public IsRequired IsRequired { get; set; }

        [ForeignKey("TrainingId")]
        [Display(Name = "課程名稱")]
        public virtual TrainingCourse TrainingCourse { get; set; }
    }
}