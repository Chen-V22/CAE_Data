using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class MbPaid //會員中心(繳費(付費)、繳費紀錄)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        public int MbId { get; set; }

        [Display(Name = "繳費金額")]
        public int Paid { get; set; }

        [Display(Name = "本次新增點數")]
        public int AddPoints { get; set; }

        [Display(Name = "繳費日期")]
        public int PaidDateTime { get; set; }

        [Display(Name = "建立日期")]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }

        [Display(Name = "會員")]
        [ForeignKey("MbId")]
        public virtual Member Member { get; set; }
    }
}