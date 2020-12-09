using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class ProjectService //專案服務Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "服務類別")]
        public ServiceType ServiceType { get; set; }

        [Display(Name = "需求主旨")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(30)]
        public string Subject { get; set; }

        [Display(Name = "指定顧問")]
        [MaxLength(30)]
        public string Consultant { get; set; }

        [Display(Name = "需求內容")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "{0}必填")]
        public string Content { get; set; }

        [Display(Name = "附件上傳")]
        public string Annex { get; set; }

        //In Create Get DateTime.Now
        [Display(Name = "建立日期")]
        public DateTime DateTime { get; set; }

        //使用驗證Cookie加入建立人名稱
        [Display(Name = "建立人")]
        public string Founder { get; set; }

        //In Edit Get DateTime.Now
        [Display(Name = "更新日期")]
        public DateTime LastEditDateTime { get; set; }

        //使用驗證Cookie加入更新者名稱
        [Display(Name = "最後修改人")]
        public string LastEditor { get; set; }

        //(導覽屬性)https://dotblogs.com.tw/wasichris/2014/08/23/146339
        public virtual ICollection<PsReply> PsReplies { get; set; }
    }
}