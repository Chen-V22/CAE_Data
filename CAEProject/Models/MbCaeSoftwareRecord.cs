using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class MbCaeSoftwareRecord //會員中心(預約紀錄)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        public int MbId { get; set; }

        [Display(Name = "軟體名稱")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "軟體類別")]
        [MaxLength(10)]
        public string SoftwareType { get; set; }

        [Display(Name = "軟體狀態")]
        [MaxLength(5)]
        public string Status { get; set; }

        [Display(Name = "備註")]
        [MaxLength(150)]
        public string Remarks { get; set; }

        [Display(Name = "預約日期")]
        [DataType(DataType.Date)]
        public DateTime ReservationDateTime { get; set; }

        [Display(Name = "會員")]
        [ForeignKey("MbId")]
        public virtual Member Member { get; set; }
    }
}