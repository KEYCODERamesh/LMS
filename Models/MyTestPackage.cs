using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    [Table("TestsPackagesDT")]
    public class MyTestPackage
    {
        [Key]
        public int PSRNO { get; set; }
        public int PackageID { get; set; }
        public string PackageName { get; set; }
        public int PanelID { get; set; }
        public int TestID { get; set; }

        public double Fees { get; set; }
        public string Panel_Test_Namelist { get; set; }
        
    }
}