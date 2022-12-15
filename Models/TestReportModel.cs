using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    [Table("TestResultDT")]
    public class TestReportModel
    {
        [Key]
        public long RecordSRNo { get; set; }
        public long RAssignID { get; set; }

        public DateTime TDate { get; set; }
        public DateTime SDate { get; set; }

        public long PatientID { get; set; }
        public int DrID { get; set; }
        public int GRPID { get; set; }
        public int TestID { get; set; }
        public int ParamID { get; set; }


        public int TSrNo { get; set; }
        public string GroupName { get; set; }
        public string TestName { get; set; }
        public string Parm_Name { get; set; }
        public string Sub_Param { get; set; }
        public string Ref_Min { get; set; }
        public string Ref_Max { get; set; }
        public string InputResult { get; set; }
        public string InputRemarks { get; set; }

        public string TestRemarks { get; set; }
        public string LabTechnician { get; set; }
        public string TestedIn { get; set; }
        public string ReportRemarks { get; set; }
        public string TUnit { get; set; }
        public decimal Minref { get; set; }
        public decimal MAXref { get; set; }

        public decimal CMin_ref { get; set; }
        public decimal CMAX_ref { get; set; }
        public decimal CompValues { get; set; }
        public int GSRNO { get; set; }
        public int TeSRNO { get; set; }
        public int PSRNO { get; set; }
        public string  Printsts { get; set; }
    }
}