using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    [Table("LabITEMDT")]
    public class LabItems
    {
        [Key]
        public long ITEMID { get; set; }
        public int HID { get; set; }

        [Display(Name = "Item Name !")]
        [Required(ErrorMessage ="Must Enter Item Name !")]
        public string ItemName { get; set; }
        // public string OPQTY { get; set; }

        [Display(Name = "Opening Stock !")]
        [Required(ErrorMessage = "Must Enter Opening Quantity !")]
        public Nullable<decimal> OPQTY { get; set; }
        public DateTime OnDate { get; set; }
        public string Createdby { get; set; }
        public int BrID { get; set; }


      
    }
}