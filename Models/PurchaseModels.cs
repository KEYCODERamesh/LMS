using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
  
    [Table("LabPurchaseDT")]
    public class PurchaseModels
    {
        [Key]
        public long PurchId { get; set; }
        public int HID { get; set; }
        public int VendorID { get; set; }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public string ItemName { get; set; }
        public Nullable<decimal> QTY { get; set; }
        public string Units { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> GSTP { get; set; }
        public Nullable<decimal> GSTMAT { get; set; }
        public Nullable<decimal> DisPer { get; set; }
        public Nullable<decimal> Grands { get; set; }
        public string EntryBy { get; set; }
        public DateTime EDate { get; set; }

        public long ITEMID { get; set; }

    }
}