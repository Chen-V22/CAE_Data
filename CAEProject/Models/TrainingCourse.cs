using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class TrainingCourse //教育訓練Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "活動名稱")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Display(Name = "狀態")]
        public Status Status { get; set; }

        [Display(Name = "活動類型")]
        public SeminarStatus SeminarStatus { get; set; }

        [Display(Name = "付費金額")]
        [MaxLength(5)]
        public int Cost { get; set; }

        [Display(Name = "負責人")]
        [Required(ErrorMessage = "{0}必填")]
        public int UserId { get; set; }

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
        [MaxLength(50)]
        public string ContactEmail { get; set; }

        [Display(Name = "活動開始日期")]
        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.DateTime)]
        public DateTime SDate { get; set; }

        [Display(Name = "活動結束日期")]
        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.DateTime)]
        public DateTime EDate { get; set; }

        [Display(Name = "報名開始日期")]
        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.DateTime)]
        public DateTime SignUpSDate { get; set; }

        [Display(Name = "報名結束日期")]
        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.DateTime)]
        public DateTime SignUpEDate { get; set; }

        [Display(Name = "報名地點")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        public string Address { get; set; }

        [Display(Name = "報名總名額")]
        [MaxLength(3)]
        public int Quota { get; set; }

        [Display(Name = "報名候補名額")]
        [MaxLength(2)]
        public int Alternate { get; set; }

        [Display(Name = "限制報名人數")]
        [MaxLength(4)]
        public int Condition { get; set; }
        //=======================<辦理單位S
        [Display(Name = "辦理單位")]
        public Handle Handle { get; set; }

        [Display(Name = "委辦單位")]
        [MaxLength(20)]
        public string Assisting { get; set; }

        [Display(Name = "計畫名稱")]
        [MaxLength(20)]
        public string ProjectName { get; set; }
        //=======================<辦理單位E
        [Display(Name = "內容")]
        [Required(ErrorMessage = "{0}必填")]
        public string Count { get; set; }

        [Display(Name = "說明函上傳")]
        public string File { get; set; }

        [Display(Name = "報名說明事項")]
        [MaxLength(300)]
        public string Description { get; set; }

        [Display(Name = "報名成功訊息")]
        [MaxLength(300)]
        public string Success { get; set; }

        [Display(Name = "活動廣告圖")]
        public string AdImage { get; set; }

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

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}