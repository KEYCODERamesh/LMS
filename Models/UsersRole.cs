using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HospitalManagementSystem.Models
{
    [Table("UserRoles")]
    public class UsersRole
    {
        [Key]
        public int RID { get; set; }
        public string LoginID { get; set; }
        public string Modules { get; set; }
        public string AddRecord { get; set; }
        public string EditRecord { get; set; }
        public string DeleteRecord { get; set; }
        public string DelPass { get; set; }
        public string EditPass { get; set; }

        [NotMapped]
        public List<SelectListItem> getLoginID { get; set; }
        public SelectList LoginList { get; set; }

    }
}