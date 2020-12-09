using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class PsReply //專案服務(回覆)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        public int PsId { get; set; }

        [Display(Name = "狀態")]
        public ServiceStatus ServiceStatus { get; set; }

        [Display(Name = "回覆")]
        [MaxLength(300)]
        [DataType(DataType.MultilineText)]
        public string Reply { get; set; }

        [Display(Name = "回覆日期")]
        public DateTime DateTime { get; set; }

        [Display(Name = "問題")]
        [ForeignKey("PsId")]
        public virtual ProjectService ProjectService { get; set; }
    }
}