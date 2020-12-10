using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class RegistrationMinor //報名表不固定欄位(儲存)Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        public int TrainingCourseId { get; set; }

        [Display(Name = "排序")]
        public int Sort { get; set; }

        [Display(Name = "欄位名稱")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Display(Name = "長度")]
        public int Length { get; set; }

        [Display(Name = "必填")]
        public IsRequired IsRequired { get; set; }

        [ForeignKey("TrainingCourseId")]
        [Display(Name = "報名表")]
        public virtual TrainingCourse TrainingCourse { get; set; }
    }
}