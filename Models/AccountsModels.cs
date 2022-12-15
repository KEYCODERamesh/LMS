using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Models
{
    [Table("AccSubHeadsDT")]
    public class AccountsModels
    {
        [Key]
        public long HeadID { get; set; }
        public int AccID { get; set; }
        public string Accounts { get; set; }
        public string AccTypes { get; set; }

        [NotMapped]
        public SelectList AccHeads { get; set; }

       


    }
}