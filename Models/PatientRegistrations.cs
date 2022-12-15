using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    [Table("Assigned_test_DT")]
    public class PatientRegistrations
    {
        [Key]
        public int AssignID { get; set; }

        public int PatieintID { get; set; }
        public int DoctorID { get; set; }
        public int TestID { get; set; }
        public string TsetName { get; set; }
        public double Rate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime TDate { get; set; }

        public bool ResultSTs { get; set; }

        public string GrpName { get; set; }

        public int TSRNo { get; set; }
        public int GSRNO { get; set; }
        public int HID { get; set; }
        public bool SyncSts { get; set; }
        public int PanelID { get; set; }
        public int PakageID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //[Display("Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string Gender { get; set; }
        public string AadharNos { get; set; }
        public int AgeYear { get; set; }
        public int Inmonth { get; set; }
        public int InDays { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string AddressDetails { get; set; }
        public string CollectionCenter { get; set; }
        public string SampleColAgents { get; set; }
        public decimal TotRs { get; set; }

        public decimal Discounts { get; set; }
        public decimal Paid { get; set; }
        public decimal Balance { get; set; }
        public string PayMode { get; set; }
    }
}