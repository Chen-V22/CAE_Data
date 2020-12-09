using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{
    public class Role //後臺管理者腳色Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "腳色名稱")]
        [MaxLength(50)]
        public string RoleName { get; set; }

        [Display(Name = "權限")]
        public string Authority { get; set; }
    }
}