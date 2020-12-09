using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class CaeSoftware
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "軟體類型")]
        public SoftwareType SoftwareType { get; set; }

        [Display(Name = "軟體類別")]
        public CaeDemand CaeDemand { get; set; }

        [Display(Name = "軟體名稱")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "軟體簡介")]
        [Required(ErrorMessage = "{0}必填")]
        public string Introduction { get; set; }

        [Display(Name = "軟體模式")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        public string SoftwareModel { get; set; }

        [Display(Name = "License數")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(2)]
        public int License { get; set; }

        [Display(Name = "使用核心數")]
        [MaxLength(3)]
        public int Core { get; set; }

        [Display(Name = "每分鐘扣的點數")]
        [MaxLength(3)]
        public int DeductionOfMinute { get; set; }

        [Display(Name = "預估每次使用時數")]
        [MaxLength(3)]
        public int CoinOfEach { get; set; }

        [Display(Name = "當日停止預約時")]
        [MaxLength(3)]
        public int DownTime { get; set; }

        [Display(Name = "開始啟用日期")]
        [Required(ErrorMessage = "{0}必填")]
        public DateTime StartTime { get; set; }

        [Display(Name = "暫停日期")]
        public DateTime? StrStopTime { get; set; }

        [Display(Name = "暫停日期")]
        public DateTime? EndStopTime { get; set; }

        [Display(Name = "軟體使用注意說明")]
        [Required(ErrorMessage = "{0}必填")]
        public string Precautions { get; set; }

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