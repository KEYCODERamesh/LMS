using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class PatRegistrationReceipts
    {
        public string LabName { get; set; }
        public string LabAddress { get; set; }
        public string LabAddress2 { get; set; }
        public string LabCity { get; set; }
        public string LabStateName { get; set; }
        public string LabPinCode { get; set; }
        public string LabMobilenos { get; set; }
        public string LabEMailID { get; set; }

        /*----------Receipts Details---------------*/
        public string RegistrationNo { get; set; }
        public string RegDate { get; set; }
        public string ReferredBy { get; set; }

        public string PatientName { get; set; }
        public string PatAddress { get; set; }
        public string PatMobiles { get; set; }
        public string PatAges { get; set; }
        public string ReceivedBy { get; set; }

        public string InvestigationsDetails { get; set; }
        public string TotalAmounts { get; set; }
        public string Discounts { get; set; }
        public string Payables { get; set; }

    }
}