using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    [Table("VendorDT")]
    public class VendorModel
    {
        [Key]
        public int VID { get; set; }
        public string VendorName { get; set; }
        public string GSTIN { get; set; }
        public string Address { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public Nullable<decimal> OpBal { get; set; }
        public int HID { get; set; }
        public bool SyncSts { get; set; }
    }
}