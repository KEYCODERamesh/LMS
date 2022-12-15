using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class ReferenceValues
    {
        public long RefSRNO { get; set; }
        public int CateID { get; set; }
        public long TestID { get; set; }
        public long TestParamID { get; set; }
        public string Gender { get; set; }
        public int MinimumAgeInDays { get; set; }
        public int MaxAgeIndays { get; set; }
        public int MinInYears { get; set; }
        public int MaxInYear { get; set; }
        public string LowerValue { get; set; }
        public string UpperValues { get; set; }
        public string InWords { get; set; }
    }
}