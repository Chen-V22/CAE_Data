using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class MbPoints //會員中心(點數使用紀錄)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        public int MbId { get; set; }

        [Display(Name = "使用時數")]
        public double UseOfHour { get; set; }

        [Display(Name = "使用點數")]
        public int UsePoints { get; set; }

        [Display(Name = "點數")]
        public int Points { get; set; }

        [Display(Name = "使用日期")]
        [DataType(DataType.Date)]
        public DateTime UseDateTime { get; set; }

        [Display(Name = "會員")]
        [ForeignKey("MbId")]
        public virtual Member Member { get; set; }
    }
}