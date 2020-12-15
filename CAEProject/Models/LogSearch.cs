using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class LogSearch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "事件類別")]
        public string EventType { get; set; }

        [Display(Name = "使用者")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Display(Name = "事件日期")]
        public DateTime EventDateTime { get; set; }

        [Display(Name = "事件位置")]
        public string EventUrl { get; set; }

        [Display(Name = "事件內容")]
        public string EventCount { get; set; }
    }
}