using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    [Table("ReferalDT")]
    public class ReferalDetails
    {
       
        public int RefId { get; set; }
        [Display(Prompt = "Referal Name")]
        [Required(ErrorMessage = "Must Referal Name !")]
        public string Referal_Name { get; set; }

        public string RefType { get; set; }
        //public ReferalType RefTypeenum { get; set; }

        [Key]
        [Required(ErrorMessage = "Must Enter Mobile No.!")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Mobile No SHould be of 10 digit!")]
        public string MobileNos { get; set; }
        public string  Address { get; set; }
        [Required(ErrorMessage = "Must EnterIncentive % !")]
        public decimal ComP { get; set; }
        [Required(ErrorMessage =  "Must Select Current Status !")]
        public bool Ref_Status { get; set; }


       // public Nullable<decimal> Mobileprice { get; set; }
    }
}