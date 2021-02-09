using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class RegistrationForm //報名表Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        //教育訓練編號(Id)
        public int TrainingId { get; set; }

        //會員="帳號"、非會員=null  
        [Display(Name = "會員名稱")]
        public string MemberName { get; set; }

        [Display(Name = "公司名稱")]
        [MaxLength(100)]
        public string Company { get; set; }

        [Display(Name = "統一編號")]
        public string CompanyNumber { get; set; }

        [MaxLength(20)]
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "{0}必填")]
        public string Name { get; set; }

        [MaxLength(50)]
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "{0}必填")]
        public string Email { get; set; }

        [MaxLength(20)]
        [Display(Name = "服務部門")]
        public string Department { get; set; }

        [MaxLength(10)]
        [Display(Name = "職稱")]
        public string JobTitle { get; set; }

        [MaxLength(50)]
        [Display(Name = "手機")]
        [Required(ErrorMessage = "{0}必填")]
        public string Cellphone { get; set; }

        [MaxLength(50)]
        [Display(Name = "電話")]
        [Required(ErrorMessage = "{0}必填")]
        public string Phone { get; set; }

        [MaxLength(50)]
        [Display(Name = "地址")]
        [Required(ErrorMessage = "{0}必填")]
        public string Address { get; set; }

        [MaxLength(100)]
        [Display(Name = "備註")]
        public string Remarks { get; set; }

        [Display(Name = "性別")]
        public GenderType GenderType { get; set; }

        [Display(Name = "發票種類")]
        public UniformInvoice UniformInvoice { get; set; }

        [Display(Name = "追加項目")]
        public string AddData { get; set; }

        [Display(Name = "報名日期")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

        [ForeignKey("TrainingId")]
        [Display(Name = "課程名稱")]
        public virtual TrainingCourse TrainingCourse { get; set; }
    }
}