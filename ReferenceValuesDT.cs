//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HospitalManagementSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReferenceValuesDT
    {
        public long RefSRNO { get; set; }
        public int CateID { get; set; }
        public long TestID { get; set; }
        public long TestParamID { get; set; }
        public string Gender { get; set; }
        public Nullable<int> MinimumAgeInDays { get; set; }
        public Nullable<int> MaxAgeIndays { get; set; }
        public Nullable<int> MinInYears { get; set; }
        public Nullable<int> MaxInYear { get; set; }
        public string LowerValue { get; set; }
        public string UpperValues { get; set; }
        public string InWords { get; set; }
    
        public virtual GroupDT GroupDT { get; set; }
        public virtual TestParameterDT TestParameterDT { get; set; }
        public virtual TestDT TestDT { get; set; }
    }
}
